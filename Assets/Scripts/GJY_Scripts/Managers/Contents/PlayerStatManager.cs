using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager
{
    public Action OnPlayerSetup;
    public Action OnHealing;
    public Action OnExtraHealing;
    public Action OnGetDamaged;
    public Action<Item_SO> OnApplyPerkStat;
    public Action<Item_SO> OnGetOrChangeActiveItem;

    // ### HPs
    private int _hp;
    private int _maxHp;
    private int _extraHp;

    // ### Stats
    private float _atk;
    private float _fireRate;
    private float _crit;
    private float _moveSpeed;

    public int Hp => _hp;
    public int MaxHp => _maxHp;
    public int ExtraHp => _extraHp;

    public float Atk => _atk;
    public float FireRate => _fireRate;
    public float Crit => _crit;
    public float MoveSpeed => _moveSpeed;

    public Item_SO ActiveItem { get; private set; }

    public void PlayerSetup()
    {
        // To Do - 플레이어 실제 스탯을 받아서 적용 시키기.
        OnPlayerSetup?.Invoke();
    }

    public void Healing()
    {        
        _hp++;
        OnHealing?.Invoke();
    }

    public void ExtraHealing()
    {
        _extraHp++;
        OnExtraHealing?.Invoke();
    }

    public void ApplyPerkStat(Item_SO item)
    {
        _atk += item.atk;
        _fireRate += item.fireRate;
        _crit += item.crit;
        _moveSpeed += item.moveSpeed;        

        OnApplyPerkStat?.Invoke(item);
    }

    public void GetActiveItem(Item_SO item)
    {
        ActiveItem = item;
        OnGetOrChangeActiveItem?.Invoke(item);
    }

    public void GetDamaged()
    {
        if(_extraHp > 0)
            _extraHp--;
        else
            _hp--;

        OnGetDamaged?.Invoke();

        if(_hp <= 0)
        {
            //To Do - GameOver
        }
    }
}
