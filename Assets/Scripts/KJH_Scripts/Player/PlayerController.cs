using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private Vector2 curMovementInput;

    private Rigidbody2D _rigidbody;
    private Animator headAnimator;
    private Animator bodyAnimator;

    public static PlayerController instance;
    private CharacterStatsHandler _stats;

    private void Awake()
    {
        instance = this;
        _rigidbody = GetComponent<Rigidbody2D>();
        _stats = GetComponent<CharacterStatsHandler>();

        // 자식 오브젝트에서 Animator 컴포넌트를 찾아서 가져옴
        headAnimator = GameObject.FindGameObjectWithTag("Head").GetComponent<Animator>();
        bodyAnimator = GameObject.FindGameObjectWithTag("Body").GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = transform.up * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= _stats.CurrentStates.moveSpeed;

        _rigidbody.velocity = dir;

        // 이동 입력에 따라 애니메이션 트리거 설정
        if (curMovementInput.magnitude > 0)
        {
            headAnimator.SetBool("IsMoving", true);
            bodyAnimator.SetBool("IsMoving", true);
            SetAnimationDirection(curMovementInput);
        }
        else
        {
            headAnimator.SetBool("IsMoving", false);
            bodyAnimator.SetBool("IsMoving", false);
        }
    }

    // 이동 방향에 따라 애니메이션 방향 설정
    private void SetAnimationDirection(Vector2 direction)
    {
        // 상하 이동
        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            //상방향
            if (direction.y > 0)
            {
                headAnimator.SetFloat("Direction", 0); 
                bodyAnimator.SetFloat("Direction", 0); 
            }
            //하방향
            else
            {
                headAnimator.SetFloat("Direction", 1); 
                bodyAnimator.SetFloat("Direction", 1); 
            }
        }
        // 좌우 이동
        else
        {
            //우방향
            if (direction.x > 0)
            {
                headAnimator.SetFloat("Direction", 2); 
                bodyAnimator.SetFloat("Direction", 2); 
            }
            //좌방향
            else
            {
                headAnimator.SetFloat("Direction", 3); 
                bodyAnimator.SetFloat("Direction", 3); 
            }
        }
    }

    // InputSystem을 사용하여 이동 입력을 처리
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Waiting)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }
}