using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    bool isLive = true;

    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target; //속도, 목표, 생존여부를 위한 변수
    WaitForFixedUpdate wait;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;
    Collider2D coll;

    Drop drop;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();
        drop = GetComponent<Drop>();
    }


    void FixedUpdate()
    {
        if (!GameManager.instance.isLive) return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        //플레이어 추적 로직 
        Vector2 dirVec = target.position - rigid.position; //타겟 위치 - 몬스터 위치
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive) return;

        if (!isLive) return;

        spriter.flipX = target.position.x < rigid.position.x;
    }
    

    void Dead()
    {
        drop.Create();
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();

        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 3;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    //초기 속성을 적용하는 함수 
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")||!isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            //Live, Hit action
            anim.SetTrigger("Hit");

        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);

            GameManager.instance.kill++;
        }

    }


    IEnumerator KnockBack()
    {
        yield return wait; //다음 하나의 물리 프레임 딜레이 

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos; // 플레이어 기준 반대 방향 
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

    }

    


}