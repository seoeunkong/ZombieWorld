using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; //데미지
    public int per; //관통

    Rigidbody2D rigid;

   
    // 데미지, 관통, 방향 
    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;


    }
}
