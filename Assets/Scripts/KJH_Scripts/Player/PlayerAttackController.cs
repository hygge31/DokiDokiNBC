using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    public static PlayerAttackController Instance;

    [Header("Projectiles")]
    [HideInInspector]
    public Vector2 attackDirection; // 공격 방향
    [HideInInspector]
    public Quaternion rotation; // 투사체 방향
    private Animator animator;
    private PlayerStatManager playerStatManager;

    [Header("Attack")]
    private float timeSinceLastAttack = 0f; // 발사 후 시간
    protected bool IsAttacking { get; set; }

    private void Awake()
    {
        Instance = this;
        animator = GetComponentInChildren<Animator>();
        playerStatManager = Managers.Player;
    }

    private void FixedUpdate()
    {
        HandleAttackDelay();
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        // 마우스 왼쪽 버튼이 눌렸을 때 공격

        if (context.phase == InputActionPhase.Performed)
        {
            IsAttacking = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            IsAttacking = false;
        }
    }

    // 공격 방향을 설정합니다.
    public void SetAttackDirection()
    {

        //애니메이터의 파라미터 값을 받음
        float direction = animator.GetFloat("Direction");

        //기본 스프라이트가 아래를 바라보므로
        attackDirection = Vector2.down;
        if (direction == 0f)
        {
            attackDirection = Vector2.up;
            rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (direction == 1f)
        {
            attackDirection = Vector2.down;
            rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (direction == 2f)
        {
            attackDirection = Vector2.right;
            rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction == 3f)
        {
            attackDirection = Vector2.left;
            rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void HandleAttackDelay()
    {

        if (timeSinceLastAttack <= playerStatManager.FireRate)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        if (IsAttacking && timeSinceLastAttack > playerStatManager.FireRate)
        {
            timeSinceLastAttack = 0;
            SetAttackDirection();
            Managers.Attack.UseWeapon(transform.position, attackDirection);
        }
    }
}
