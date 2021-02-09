using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float m_speed = 15f;
    Vector2 move;
    Rigidbody2D m_rigid;
    float screenTop, screenBottom, screenLeft, screenRight;

    void Awake()
    {
        m_rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        screenTop = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        screenBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
    }
    void Update()
    {
        CheckInput();
        CheckOutOfScreen();
    }
    void FixedUpdate()
    {
        m_rigid.velocity = move * m_speed;
    }
    void CheckInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        move = new Vector2(xInput, yInput).normalized;
    }

    void CheckOutOfScreen()
    {
        float nextX = Mathf.Clamp(transform.position.x, screenLeft, screenRight);
        float nextY = Mathf.Clamp(transform.position.y, screenBottom, screenTop);

        transform.position = new Vector3(nextX, nextY, transform.position.z);
    }
}


