using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    public static PlayerAttackController Instance;

    private ProjectileManager _projectileManager;

    public Transform projectileSpawnPosition;

    public IAttack equippedItem; // 현재 장착된 공격 아이템

    private Vector2 attackDirection; // 공격 방향

    private void Awake()
    {
        Instance = this; 
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        // 마우스 왼쪽 버튼이 눌렸을 때 공격
        if (context.phase == InputActionPhase.Performed && equippedItem != null)
        {
            equippedItem.PerformAttack(attackDirection);
            Debug.Log("공격입력");
        }
    }

    public void EquipItem(IAttack item)
    {
        equippedItem = item;
    }

    // 공격 방향을 설정합니다.
    public void SetAttackDirection(Vector2 direction)
    {
        attackDirection = direction;
        Debug.Log("공격방향설정");        
    }

    //public void CreateProjectile(AttackSO AttackData)
    //{
    //    _projectileManager.ShootBullet(
    //            projectileSpawnPosition.position,
    //            AttackData
    //            );
    //}
}
