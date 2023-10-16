using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; //데미지
    public int per; //관통

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }


    // 데미지, 관통, 방향 
    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        //관통이 -1(무한)보다 큰 것에 대해서는 속도 적용
        //per == -1 인 경우는 근접 무기 
        if (per > -1)
        {
            //총알이 날아가는 속도 증가시키기 
            rigid.velocity = dir * 15;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1)
            return;

        per--;

        //-1이 되면 비활성화 
        if (per == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
