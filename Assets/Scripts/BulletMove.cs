using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {
    public float m_speed = 3f;
    public Vector2 m_direction;
    public bool m_isUsed = false;

    bool onScreen = false; // 화면 들어온지 
    bool onScreenOut = false;
    Rigidbody2D m_rigid;


    // Use this for initialization

    void Awake()
    {
        m_rigid = GetComponent<Rigidbody2D>();

    }

    public void SetDirection(Vector2 value)
    {
        m_direction = value;
    }


    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (!onScreen && BulletManager.instance.IsInScreen(transform.position))        // 화면 밖에서 안으로 들어오면
        {
            onScreen = true;
        }
        else if (onScreen && !BulletManager.instance.IsInScreen(transform.position))     //화면 안에서 밖으로 나가면
        {
            onScreenOut = true;
            m_isUsed = false;
        }
    }
    void FixedUpdate()
    {
            if(!onScreenOut)
            {
                m_rigid.velocity = m_direction * m_speed;
            }
    }
    public void SetPosition(Vector2 value)
    {
        transform.position = value;
        onScreen = false;
        onScreenOut = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "spaceship")
        {
            Destroy(gameObject);
        }
    }
}
