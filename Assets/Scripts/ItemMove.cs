using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour {

    Vector2 m_LeftBottom;
    Vector2 m_RightTop;

    void Start()
    {
        m_LeftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        m_RightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector2 pos = GetRandomPosition();
        Vector3 temp = new Vector3(pos.x, 11, 0);
        transform.position = temp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -0.05f, 0);

        if (transform.position.y < -15.0f)
        {
            Destroy(gameObject);
        }
    }

    Vector2 GetRandomPosition()
    {
        int caseNum = Random.Range(0, 2);
        Vector2 pos = Vector2.zero;
        switch (caseNum)
        {
            case 0:     //상단
                pos.x = Random.Range(m_LeftBottom.x, m_RightTop.x);
                pos.y = m_RightTop.y;
                break;
            case 1:     //하단
                pos.x = Random.Range(m_LeftBottom.x, m_RightTop.x);
                pos.y = m_LeftBottom.y;
                break;

        }

        return pos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "spaceship")
        {
            Destroy(gameObject);
        }
    }

}
