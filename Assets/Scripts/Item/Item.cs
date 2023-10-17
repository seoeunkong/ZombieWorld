using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int prefabID;
    public bool activate;

    Rigidbody2D rigid;
    float speed = 20.0f;
    

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        activate = false;
    }

    void Update()
    {
        if(activate)
            MoveToPlayer();
    }


    public void Init(int prefabID)
    {
        this.prefabID = prefabID;
    }

    public void MoveToPlayer()
    {
        Vector3 target = GameManager.instance.player.transform.position;
        Vector3 dir = target - transform.position;
        dir = dir.normalized; //방향

        transform.Translate(dir * Time.deltaTime * speed);
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.GetExp();
            GameManager.instance.itemCount(false); //아이템 총 개수 감소

            if (prefabID == 5)
            {
                GameManager.instance.magActivate = true;
            }

            activate = false;
            gameObject.SetActive(false);
        }

    }
}
