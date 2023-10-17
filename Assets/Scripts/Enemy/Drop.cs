using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    int prefabID;

    public void Create()
    {
        prefabID = GameManager.instance.GetRanItm();

        GameObject item = GameManager.instance.pool.Get(prefabID);

        Item itm = item.GetComponent<Item>();
        itm.Init(prefabID);
        item.transform.position = transform.position;
    }

}
