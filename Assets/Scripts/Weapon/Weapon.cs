using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //무기 생성 및 관리 담당


    public int id; //무기ID
    public int prefabid; //프리펩ID
    public float damage;
    public int count; //개수
    public float speed;

    Player player;
    float timer;

    void Awake()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
        //if (!GameManager.instance.isLive) return;

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            case 1:
                timer += Time.deltaTime;

                //speed는 연사속도. 연사속도가 적을수록 많이 발사 
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;

            default:
                break;


        }

        //test code
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }

    }



    public void Init(ItemData data)
    {
        //Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabid = index;
                break;
            }
        }

        switch (id)
        {
            case 0:
            case 2:
                speed = 150;
                Batch(id);
                break;

            case 1:
                speed = 0.3f;
                break;

            default:
                break;
        }

        //특정 함수 호출을 모든 자식에게 방송하는 함수
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);


    }


    void Batch(int id) //생성된 무기를 배치하는 함수
    {
        switch (id)
        {
            case 0: 
                for (int index = 0; index < count; index++)
                {
                    Transform bullet;

                    //기존 오브젝트 풀링을 먼저 활용하고 모자른 것은 풀링에서 가져오기
                    if (index < transform.childCount)
                    {
                        bullet = transform.GetChild(index);
                    }
                    else
                    {
                        bullet = GameManager.instance.pool.Get(prefabid).transform;
                        bullet.parent = transform; //Weapon 0 오브젝트를 부모로 설정
                    }

                    //배치하기 전에 초기화
                    bullet.localPosition = Vector3.zero;
                    bullet.localRotation = Quaternion.identity;


                    Vector3 rotVec = Vector3.forward * 360 * index / count;
                    bullet.Rotate(rotVec);

                    bullet.Translate(bullet.up * 1.5f, Space.World);

                    bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); //근접무기는 무조건 관통하기 때문에 의미없. 따라서 무한으로 관통한다는 의미에서 -1을 넣어줌
                }

                break;

            case 2:

                for (int index = 0; index < count; index++)
                {
                    Transform bullet;

                    if (index < transform.childCount)
                        continue;


                    bullet = GameManager.instance.pool.Get(prefabid).transform;
                    bullet.parent = transform; //Weapon 2 오브젝트를 부모로 설정

                    Vector3 rotVec = Vector3.forward * 360 * index / count;
                    bullet.Rotate(rotVec);

                    bullet.GetComponent<Bullet>().Init(damage, -2, Vector3.forward); //무한으로 관통한다는 의미에서 -2을 넣어줌
                }
                break;

        }
        
    }


    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0 || id == 2)
            Batch(id);

        //특정 함수 호출을 모든 자식에게 방송하는 함수
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);

    }


    void Fire()
    {
        //가장 가까이 몬스터가 없으면 그냥 넘어감
        if (!player.scanner.nearestTarget)
            return;

        Vector3 target = player.scanner.nearestTarget.position;
        Vector3 dir = target - transform.position;
        dir = dir.normalized; //방향

        Transform bullet = GameManager.instance.pool.Get(prefabid).transform;
        bullet.position = transform.position; //기존 생성 로직 그대로 활용하면서 위치를 플레이어 위치로 지정
        //지정된 축을 중심으로 목표를 향해 회전하는 함수 
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
       
    }



}
