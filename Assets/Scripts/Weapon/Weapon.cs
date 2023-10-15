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

    void Start()
    {
        Init();
    }


    void Update()
    {
        //if (!GameManager.instance.isLive) return;

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
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



    public void Init()
    {
      
        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;

            default:
                speed = 0.3f;
                break;
        }
       
    }


    void Batch() //생성된 무기를 배치하는 함수
    {
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
    }


    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();
       
    }






}
