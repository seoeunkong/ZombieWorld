using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Items[] items;


    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Items>(true);

        
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next() //랜덤 활성화 함수 
    {
        //1. 모든 아이템 비활성화
        foreach (Items item in items)
        {
            item.gameObject.SetActive(false);
        }

        //2. 그 중에서 랜덤 3개 아이템 활성화
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            //모두 같지 않을 때 탈출 
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for (int index = 0; index < ran.Length; index++)
        {
            Items ranItem = items[ran[index]];

            //3. 만렙 아이템의 경우는 소비 아이템(음료)으로 대체
            if (ranItem.level == ranItem.data.damages.Length)
            {
                //아이템이 최대 레벨이면 소비 아이템이 대신 활성화
                items[5].gameObject.SetActive(true);
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }


        }
    }


}
