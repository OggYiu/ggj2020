﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameMgr : MonoBehaviour
{
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
    }
}
