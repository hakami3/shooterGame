using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {

    public GameObject coinPrefab;
    float span = 5.0f;
    float delta = 0;
    Vector2 m_LeftBottom, m_RightTop;

    void Start()
    {

    }

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            Instantiate(coinPrefab, GetRandomPosition(), Quaternion.identity);
        }
    }

    Vector2 GetRandomPosition()
    {
        int caseNum = Random.Range(0, 2);
        Vector2 pos = Vector2.zero;
        int px = Random.Range(-150, 150);
        int py = Random.Range(-110, 110);
        float xx = px * 0.1f;
        float yy = py * 0.1f;
        pos.x = xx;
        pos.y = yy;
        return pos;
    }
}
