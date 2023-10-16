using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

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
        //적절한 숫자로 나누어 시간에 맞춰 레벨업 
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f);


        if (timer > spawnData[level].spawnTime) //레벨을 활용해 소환 타이밍을 변경
        {

            Spawn();
            timer = 0;
        }


    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; //자식 오브젝트에서만 선택되도록 랜덤 시작은 1부터
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }

}


//직렬화(Serialization):개체를 저장 혹은 전송하기 위해 변환 -> 인스팩터 창에 보임
[System.Serializable]
public class SpawnData //소환 데이터를 담당하는 클래스 선언
{

    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;


}
