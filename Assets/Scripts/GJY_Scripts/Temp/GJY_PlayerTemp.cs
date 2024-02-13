using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJY_PlayerTemp : MonoBehaviour
{
    public float speed;

    private Rigidbody2D _rigid;

    private bool isMovable = true;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isMovable = !isMovable;
            // Temp To Do - 상점 UI 출력
        }
    }

    private void Move()
    {
        if (!isMovable)
            return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movePos = new Vector2(moveX, moveY);
        Vector2 nextPos = (Vector2)transform.position + movePos.normalized * speed * Time.fixedDeltaTime;

        _rigid.MovePosition(nextPos);
    }
}
