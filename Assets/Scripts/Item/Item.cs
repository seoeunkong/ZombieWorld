using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int prefabID;

    Rigidbody2D rigid;
    

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
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

        rigid.AddForce(dir * 50.0f, ForceMode2D.Impulse);
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.GetExp();
            GameManager.instance.itemCount(false); //아이템 총 개수 감소

            if(prefabID == 5)
            {
                GameManager.instance.magActivate = true;
            }

            gameObject.SetActive(false);
        }
    }
}
