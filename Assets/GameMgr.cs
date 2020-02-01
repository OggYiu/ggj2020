using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameMgr : MonoBehaviour
{
    public ItemObj[] itemObjs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnItemClicked(Item item)
    {
        Assert.IsTrue(item.id.Length > 0, "Item needs an id");
        Debug.Log("OnItemClicked: " + item.id);

        ItemObj targetItemObj = null;
        foreach(ItemObj itemObj in itemObjs)
        {
            if(itemObj.GetComponent<Item>().id == item.id)
            {
                targetItemObj = itemObj;
                break;
            }
        }

        if(targetItemObj != null)
        {

        }
    }
}
