using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public BoxCollider2D inputTrigger;
    public float maxBoilingPoint = 0;
    public float boilingPoint = 0;
    public List<Item> items;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //Physics.Raycast(ray, hit, 5.0f);
        //if (Input.GetMouseButtonDown(0) && hit.collider.name == "YourGameObjectName")
        //{
        //}

        if(items.Count > 0)
        {
            boilingPoint += Time.deltaTime;
            if (boilingPoint > maxBoilingPoint)
            {
                boilingPoint = maxBoilingPoint;
            }
            UpdateBoilingPotColor();
        }
    }
    void OnMouseDown()
    {
        if (isReady())
        {
            //testing
            /*
            string[] items = { "Honey", "Peppermint" };
            CurrentVirus.CURE(items);
            */

            List<string> list = new List<string>();

            for (int i = 0; i < items.Count; ++i)
            {
                list.Add(items[i].name);
                Destroy(items[i].gameObject, 0.1f);
            }
            items.Clear();

            ResetBoilingPoint();
            spriteRenderer.color = Color.white;

            GameMgr gameMgr = FindObjectOfType<GameMgr>();
            gameMgr.CurrentVirus.CURE(list.ToArray());

            Debug.Log("CURE: " + list.ToArray());
        }
    }

    public bool isReady()
    {
        return boilingPoint >= maxBoilingPoint;
    }

    public void ResetBoilingPoint()
    {
        boilingPoint = 0;
    }

    public void UpdateBoilingPotColor()
    {
        float factor = boilingPoint / maxBoilingPoint;
        spriteRenderer.color = new Color(factor, 1.0f - factor, 1.0f - factor);

        foreach(Item item in items)
        {
            item.SetColor(spriteRenderer.color);
        }
    }

    public void UpdateBoilingPointCount()
    {
        maxBoilingPoint = items.Count * 2;
        if(boilingPoint > maxBoilingPoint)
        {
            boilingPoint = maxBoilingPoint;
        }
        UpdateBoilingPotColor();
    }

    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if(item == null)
        {
            return;
        }

        items.Add(item);

        UpdateBoilingPointCount();
    }

    //When the Primitive exits the collision, it will change Color
    private void OnTriggerExit2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if (item == null)
        {
            return;
        }

        items.Remove(item);

        if(items.Count == 0)
        {
            ResetBoilingPoint();
            item.SetColor(Color.white);
        }
        else
        {
            UpdateBoilingPointCount();
        }
    }
}
