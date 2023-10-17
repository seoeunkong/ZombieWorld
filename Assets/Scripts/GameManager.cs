using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;


    [Header("# Game Control")]
    public bool isLive; //시간 정지 여부 
    public float gameTime;
    public float maxGameTime;


    [Header("# Player Info")]
    public int level; //레벨
    public float health;
    public float maxHealth = 100;
    public int kill; //킬 수 
    public int exp; //경험치
    //각 레벨의 필요경험치를 보관할 배열 변수 선언 및 초기화 
    public int[] nextExp;

    [Header("# Item")]
    public int itemCnt; //아이템 개수
    public bool magActivate;

    float cRate = 5f; //coin 비율
    float mRate = 2f; // mag 비율


    void Awake()
    {
        instance = this; //자기자신으로 초기화

    }

    public void GameStart()
    {
        health = maxHealth;
        uiLevelUp.Select(0);
        Resume();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f); //애니메이션 보기 전에 stop을 할 수 있기 때문에 딜레이함
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }


    void Update()
    {
        if (!isLive) return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }

        //자석 아이템 사용 
        if (magActivate)
        {
            pool.Move(4);
        }
    }



    public int GetRanItm()
    {
        //총 개수에 따른 coin 개수 구하기 
        float a = mRate / (cRate + mRate);
        float rate = itemCnt - (itemCnt * a);

        int tmp = Random.Range(0, itemCnt);

        if (tmp <= rate)
        {
             return 4; // prefabID로 반환
        }
        else return 5;
        
    }


    public void GetExp()
    {
        if (!isLive) return;

        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)]) //min 함수를 사용하여 최고 경험치를 그대로 사용하도록 변경 
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    //아이템 총 개수 체크 
    public void itemCount(bool active)
    {
        if (active) itemCnt++;
        else itemCnt--;
    }


    public void Stop()
    {
        isLive = false;
        //유니티의 시간 속도(배율)
        Time.timeScale = 0;
    }


    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }

}
