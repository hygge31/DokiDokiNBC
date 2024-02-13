using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttackData", menuName = "KJH_SO/Attacks/DefaultAttackData", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public string bulletNameTag;
    public float fireRate;
    public float damage;
    public LayerMask targetLayer;

    [Header("Knock Back Info")]
    public bool isOnKnockback;
    public float knockbackPower;
    public float knockbackTime;
}
