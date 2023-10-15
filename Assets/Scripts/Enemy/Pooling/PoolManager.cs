using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs; //프리펩들을 저장할 배열 변수 선언
    List<GameObject>[] pools; //오브젝트 풀들을 저장할 배열 변수 선언



    void Awake()
    {
        //리스트 배열 초기화할 때 크기는 프리펩 배열 길이 활용 
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }

    }

    public GameObject Get(int index)
    {
        GameObject select = null;


        //선택한 풀에서 놀고 있는(비활성화) 오브젝트 접근
        foreach (GameObject item in pools[index])
        {

            if (!item.activeSelf)
            {
                //발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }


        //못 찾았으면?
        if (!select) 
        {
            //새롭게 생성하고 select 변수에 할당
            select = Instantiate(prefabs[index], transform); 
            pools[index].Add(select);
        }



        return select;
    }
}
