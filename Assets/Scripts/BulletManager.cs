using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;
    public GameObject BulletPrefab;
    public int maxBullet = 50;
    public Transform m_Player;
    List<BulletMove> BulletList;
    
    Vector2 m_LeftBottom;
    Vector2 m_RightTop;

    void Awake()
    {
        if (instance)
        {
            Debug.Log("다중 인스턴스 실행중.");
        }
        instance = this;
    }
    // Use this for initialization
    void Start()
    {
        m_LeftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        m_RightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        BulletList = new List<BulletMove> ();
        for(int i=0; i<maxBullet; i++)
        {
            GameObject temp = (GameObject)Instantiate(BulletPrefab, new Vector3(0, m_RightTop.y + 2, 0),Quaternion.identity);
            BulletList.Add(temp.GetComponent<BulletMove>());
        }
    }

    public bool IsInScreen(Vector2 target)
    {
        if (target.x > m_LeftBottom.x && target.x < m_RightTop.x && target.y > m_LeftBottom.y && target.y < m_RightTop.y)
        {
            return true;
        }
        else
        {
            return false;
        }
        // Update is called once per frame
        

    }
    public void CreateBullet()
    {
        if(!m_Player)
        {
            return;
        }
        Vector2 pos = GetRandomPosition();  //랜덤하게 받아와서
        Vector2 direction = (Vector2)m_Player.position - pos;       //안쪽으로 들어오는 벡터

        BulletMove seletedBullet = BulletList.Find(o => o.m_isUsed == false);   //현재 미사용중인 총알을 찾아서

        if(!seletedBullet)      //최대 총알수 초과
        {
            Debug.Log("최대 총알수를 늘려주세요.");
        }
        else    //방향과 속도 설정
        {
            seletedBullet.SetDirection(direction.normalized);
            seletedBullet.SetPosition(pos);
            seletedBullet.m_isUsed = true;
        }
    }

    Vector2 GetRandomPosition()
    {
        int caseNum = Random.Range(0, 4);
        Vector2 pos = Vector2.zero;
            switch (caseNum)
        {
            case 0:     //좌측
                pos.x = m_LeftBottom.x - 1;
                pos.y = Random.Range(m_LeftBottom.y, m_RightTop.y);
                break;
            case 1:     //우측
                pos.x = m_RightTop.x +1;
                pos.y = Random.Range(m_LeftBottom.y, m_RightTop.y);
                break;
            case 2:     //상단
                pos.x = Random.Range(m_LeftBottom.x, m_RightTop.x);
                pos.y = m_RightTop.y + 1;
                break;
            case 3:     //하단
                pos.x = Random.Range(m_LeftBottom.x, m_RightTop.x);
                pos.y = m_LeftBottom.y - 1;
                break;

        }

        return pos;
    }
}
