using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class PlayerStatManager
{
    public Action OnDead;
    public Action OnPlayerSetup;
    public Action OnWeaponChange;
    public Action OnUpgradeFromShop;
    public Action<string, int> OnPerkSetup;
    public Action<int, int> OnHealing;
    public Action<int, int> OnGetDamaged;
    public Action<Item_SO> OnApplyPerkStat;

    public int upgrade_Atk { get; set; }
    public int upgrade_FireRate { get; set; }
    public int upgrade_MoveSpeed { get; set; }

    // ### Stats
    public int Hp { get; private set; }

    public float Atk { get; private set; }
    public float FireRate { get; private set; }
    public float MoveSpeed { get; private set; }
    public int AddBullet { get; private set; }
    public int PierceCount { get; private set; }

    // ### Applied Stats    
    public float A_Atk { get; private set; }
    public float A_FireRate { get; private set; }
    public float A_MoveSpeed { get; private set; }
    public int A_AddBullet { get; private set; }
    public int A_PierceCount { get; private set; }

    // ### Weapon Stats    
    public float W_Atk { get; private set; }
    public float W_FireRate { get; private set; }
    public float W_BulletSpeed { get; private set; }
    public int W_AddBullet { get; private set; }
    public int W_PierceCount { get; private set; }
    public float W_Duration { get; private set; }

    public bool IsDead { get; private set; } = false;

    private Dictionary<string, int> _perkDict = new Dictionary<string, int>();

    public void Init()
    {
        Hp = 10;
        MoveSpeed = 5;
        Atk = 1;
        FireRate = 1;
        AddBullet = 0;
        PierceCount = 0;

        Managers.Attack.OnWeaponSetup += WeaponStatApply;
    }

    public void PlayerSetup()
    {
        // To Do - 플레이어 퍽을 받아서 적용 시키기.        

        string[] keys = Enum.GetNames(typeof(Define.Perks));
        for (int i = 0; i < keys.Length; i++)
        {
            if (_perkDict.TryGetValue(keys[i], out int value))
                OnPerkSetup?.Invoke(keys[i], value);
        }

        OnPlayerSetup?.Invoke();
        Managers.Attack.OnChangeWeapon += WeaponStatApply;
    }

    public void Healing()
    {
        int prevHp = Hp;
        Hp++;
        OnHealing?.Invoke(prevHp, Hp);
    }

    private void WeaponStatApply(Item_SO weapon)
    {
        W_FireRate = weapon.fireRate;
        W_Atk = weapon.atk;
        W_BulletSpeed = weapon.bulletSpeed;
        W_AddBullet = weapon.addBullet;
        W_PierceCount = weapon.pierceCount;
        W_Duration = weapon.duration;

        AppliedStatCalculator();
    }

    public void ApplyPerkStat(Item_SO item)
    {
        if (_perkDict.ContainsKey(item.name))
            _perkDict[item.name]++;
        else
            _perkDict.Add(item.name, 1);

        Atk += item.atk;
        FireRate += item.fireRate;
        AddBullet += item.addBullet;
        PierceCount += item.pierceCount;
        A_MoveSpeed = MoveSpeed * item.moveSpeed;

        AppliedStatCalculator();

        OnApplyPerkStat?.Invoke(item);
    }

    private void AppliedStatCalculator()
    {
        A_FireRate = FireRate * W_FireRate;
        A_Atk = Atk * W_Atk;
        A_AddBullet = AddBullet + W_AddBullet;
        A_PierceCount = PierceCount + W_PierceCount;
        A_MoveSpeed = MoveSpeed * (_perkDict.ContainsKey("Item_Injector") == true ? 2 : 1);

        OnWeaponChange?.Invoke();
    }

    public void Upgrade(string input)
    {
        switch (input)
        {
            case "1":
                Atk += 0.5f;
                upgrade_Atk++;
                break;
            case "2":
                FireRate -= 0.1f;
                upgrade_FireRate++;
                break;
            case "3":
                MoveSpeed += 0.5f;
                upgrade_MoveSpeed++;
                break;
        }

        AppliedStatCalculator();

        OnUpgradeFromShop?.Invoke();
    }

    public void GetDamaged()
    {
        if (IsDead)
            return;

        int prevHp = Hp;

        Hp--;
        OnGetDamaged?.Invoke(prevHp, Hp);

        if (Hp <= 0)
        {
            //To Do - GameOver
            IsDead = true;
            OnDead?.Invoke();
            Init();
            Debug.Log("캐릭터 사망");
        }
    }

    public void Clear()
    {
        IsDead = false;
        OnDead = null;
        OnPlayerSetup = null;
        OnPerkSetup = null;
        OnHealing = null;
        OnGetDamaged = null;
        OnApplyPerkStat = null;
        OnUpgradeFromShop = null;
        OnWeaponChange = null;
    }
}
