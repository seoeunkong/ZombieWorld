using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    //public SpawnData[] spawnData;

    int level;
    float timer;

    void Awake()
    {
        //spawnPoint에 자기자신과 자식 오브젝트들 포함됨
        spawnPoint = GetComponentsInChildren<Transform>();

    }

    void Update()
    {
        //if (!GameManager.instance.isLive) return;

        timer += Time.deltaTime;
        //level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);  //적절한 숫자로 나누어 시간에 맞춰 레벨이 올라가도록 작성

        if (timer > 0.2f) //spawnData[level].spawnTime) //레벨을 활용해 소환 타이밍을 변경
        {
            Spawn();
            timer = 0;
        }


    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(Random.Range(0,4));
        //GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; //자식 오브젝트에서만 선택되도록 랜덤 시작은 1부터
        //enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }

}
