using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    float mRate = 0.3f; // mag 비율

    private float rate;
    private int length;

    int prefabID;

    void Awake()
    {
        length = GameManager.instance.itemCnt;
        Init();

    }

    void Init()
    {
        //mag 개수 
        rate = length * mRate;
    }

    int GetRandomRate()
    {
        int tmp = Random.Range(0, length);

        if (tmp <= rate - 1) // length는 0을 포함하지 않기 때문에 0부터 세기 위해 정답 개수 - 1
        {
            return 5; // prefabID로 반환
        }
        else return 4;
    }

    public void Create()
    {
        prefabID = GetRandomRate();

        GameObject item = GameManager.instance.pool.Get(prefabID);

        Item itm = item.GetComponent<Item>();
        itm.Init(prefabID);
        item.transform.position = transform.position;
    }

}
