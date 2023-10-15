using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager pool;

    [Header("# Game Object")]
    public Player player;

    void Awake()
    {
        instance = this; //자기자신으로 초기화
    }

}
