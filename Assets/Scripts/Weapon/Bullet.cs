using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; //데미지
    public int per; //관통

    Rigidbody2D rigid;

    // 이동 관련 변수
    float move_speed;
    float move_x_rate;
    float move_y_rate;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        move_speed = 5.0f;
        move_x_rate = Random.Range(-1.0f, 1.0f);
        move_y_rate = Random.Range(-1.0f, 1.0f);

        while (Mathf.Abs(move_x_rate) < 0.3f)
        {
            move_x_rate = Random.Range(-1.0f, 1.0f);
        }

        while (Mathf.Abs(move_y_rate) < 0.3f)
        {
            move_y_rate = Random.Range(-1.0f, 1.0f);
        }
    }

    void Update()
    {
        if(per < -1) //삽 
        {
            MoveInside();
        }
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
        if (!collision.CompareTag("Enemy") || per <= -1)
            return;

        per--;

        //-1이 되면 비활성화 
        if (per == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    //카메라 안에서 움직이기 
    void MoveInside()
    {
        transform.Translate(Vector3.right * Time.deltaTime * move_speed * move_x_rate, Space.World);
        transform.Translate(Vector3.up * Time.deltaTime * move_speed * move_y_rate, Space.World);

        // 카메라를 벗어나지 않도록 범위 제한
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        if (position.x < 0f)
        {
            position.x = 0f;
            move_x_rate = Random.Range(0.3f, 1.0f);
        }
        if (position.y < 0f)
        {
            position.y = 0f;
            move_y_rate = Random.Range(0.3f, 1.0f);
        }
        if (position.x > 1f)
        {
            position.x = 1f;
            move_x_rate = Random.Range(-1.0f, -0.3f);
        }
        if (position.y > 1f)
        {
            position.y = 1f;
            move_y_rate = Random.Range(-1.0f, -0.3f);
        }
        transform.position = Camera.main.ViewportToWorldPoint(position);

        transform.Rotate(Vector3.back * 200.0f * Time.deltaTime);
    }
}
