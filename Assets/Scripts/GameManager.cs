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

    void Awake()
    {
        instance = this; //자기자신으로 초기화
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
    }

}
