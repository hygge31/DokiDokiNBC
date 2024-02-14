using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager
{
    public Action OnPlayerSetup;
    public Action<int, int> OnHealing;
    public Action<int, int> OnExtraHealing;
    public Action<int, int> OnGetDamaged;
    public Action<Item_SO> OnApplyPerkStat;
    public Action<Item_SO> OnGetOrChangeActiveItem;

    // ### HPs
    public int Hp { get; private set; }
    public int MaxHp { get; private set; }
    public int ExtraHp {  get; private set; }

    // ### Stats
    public float Atk {  get; private set; }
    public float FireRate {  get; private set; }
    public float Crit {  get; private set; }
    public float MoveSpeed {  get; private set; }
    

    public Item_SO ActiveItem { get; private set; }

    public void PlayerSetup()
    {
        // To Do - 플레이어 실제 스탯을 받아서 적용 시키기.

        //CharacterStatsHandler stat = UnityEngine.Object.FindObjectOfType<CharacterStatsHandler>();

        //Hp = stat.CurrentStates.maxHealth - 2;
        //MaxHp = stat.CurrentStates.maxHealth;

        //MoveSpeed = stat.CurrentStates.speed;

        // Temp
        Hp = 3;
        MoveSpeed = 3;

        OnPlayerSetup?.Invoke();
    }

    public void Healing()
    {
        int prevHp = Hp;
        Hp++;
        OnHealing?.Invoke(prevHp, Hp);
    }

    public void ExtraHealing()
    {
        ExtraHp++;
        OnExtraHealing?.Invoke(1,1);
    }

    public void ApplyPerkStat(Item_SO item)
    {
        Atk += item.atk;
        FireRate += item.fireRate;
        Crit += item.crit;
        MoveSpeed += item.moveSpeed;        

        OnApplyPerkStat?.Invoke(item);
    }

    public void GetActiveItem(Item_SO item)
    {
        ActiveItem = item;
        OnGetOrChangeActiveItem?.Invoke(item);
    }

    public void GetDamaged()
    {
        int prevHp = Hp;
        int prevExtraHp = ExtraHp;

        if (ExtraHp > 0)
        {
            ExtraHp--;
            OnGetDamaged?.Invoke(prevExtraHp, ExtraHp);
        }
        else
        {
            Hp--;
            OnGetDamaged?.Invoke(prevHp, Hp);
        }        

        if(Hp <= 0)
        {
            //To Do - GameOver
            Debug.Log("캐릭터 사망");
        }
    }
}
