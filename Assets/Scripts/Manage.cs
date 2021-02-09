using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manage : MonoBehaviour
{
    public static Manage instance;
    public float m_generateTerm = 1.5f;
    public float[] levels;
    public int[] bulletCount;
    public Text scoreText;
    public Text FinalText;
    public CanvasGroup GameOverPanel;
    int index = 0;
    float m_cuttentTime;
    bool m_gameover = false;
    float result1 = 0;
    int result = 0;
    public int reward = 0;

    void Awake()
    {
        if (instance)
        {
            Debug.Log("다수의 인스턴스 실행중");

        }
        instance = this;

    }
    // Use this for initialization
    void Start()
    {
        m_cuttentTime = 0;
        m_gameover = false;
        StartCoroutine("CreateBullet");
    }

    // Update is called once per frame
    void Update()
    {
        if(m_gameover)
        {
            return;
        }
        m_cuttentTime += Time.deltaTime;
        result1 = (Mathf.Round(m_cuttentTime * 100)) / 100*20 + reward * 200;
        result = (int)result1;
        scoreText.text = "Score :" + result;
        if (m_cuttentTime > levels[index] && (index < levels.Length - 1))
        {
            index++;
        }
    }

    IEnumerator CreateBullet()
    {
        while (!m_gameover)
        {
            yield return new WaitForSeconds(m_generateTerm);
            for (int i = 0; i < bulletCount[index]; i++)
            {
                BulletManager.instance.CreateBullet();

            }
        }
    }

    public void SetGameOver()
    {
        m_gameover = true;
        GameOverPanel.alpha = 1;
        GameOverPanel.interactable = true;
        GameOverPanel.blocksRaycasts = true;
        FinalText.text = "Final Score :" + result;
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void SetReward()
    {
        reward++;
    }

    private void GoMain()
    {
        SceneManager.LoadScene("startScene");
    }

    public void ResetBtn()
    {
        Invoke("GoMain", .1f);
    }

    private void GoGame()
    {
        SceneManager.LoadScene("gameScene");
    }

    public void StartGame()
    {
        Invoke("GoGame", .1f);
    }

    public void Finish()
    {
        Application.Quit();
    }
}
