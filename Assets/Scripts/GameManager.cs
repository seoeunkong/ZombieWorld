using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager pool;

    [Header("# Game Object")]
    public Player player;

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
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Item")]
    public int itemCnt = 0; //아이템 개수
    public bool magActivate;


    void Awake()
    {
        instance = this; //자기자신으로 초기화
    }

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        //if (!isLive) return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            //GameVictory();
        }

        //자석 아이템 사용 
        if (magActivate)
        {
            pool.Move(4);
        }
    }

    public void GetExp()
    {
        //if (!isLive) return;

        exp++;

        if (exp == nextExp[level]) //min 함수를 사용하여 최고 경험치를 그대로 사용하도록 변경 
        {
            level++;
            exp = 0;
            //uiLevelUp.Show();
        }
    }

    //아이템 총 개수 체크 
    public void itemCount(bool active)
    {
        if (active) itemCnt++;
        else itemCnt--;
    }

}
