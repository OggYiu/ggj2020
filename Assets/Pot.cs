﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public GameObject[] GFXs;
    public Sprite[] waterSprites;
    public BoxCollider2D inputTrigger;
    public float BOILING_RATE = 1;
    public float maxBoilingPoint = 0;
    public float boilingPoint = 0;
    public List<Item> items;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer waterSpriteRenderer;
    public AudioSource SFX_click;
    public AudioSource SFX_addItem;
    public AudioSource SFX_boil;
    public AudioSource SFX_ready;
    public SpriteRenderer spriteRenderer_Ready;
    //public Animator GFX_Ready;

    private float SPRITE_CHANGE_INTERVAL = 0.1f;
    private float spriteChangeCountdown = 0;
    private int lastSpriteIndex = 0;

    private bool readyGfxShown = false;

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
            if (boilingPoint >= maxBoilingPoint)
            {
                boilingPoint = maxBoilingPoint;

                if(!readyGfxShown)
                {
                    GameObject gfx = Instantiate(GFXs[0]);
                    gfx.transform.position = new Vector3(0, 0, 0);
                    gfx.transform.localScale = new Vector3(3, 3, 3);
                    Destroy(gfx, 2.0f);
                    readyGfxShown = true;

                    SFX_ready.Play();
                }
                //spriteRenderer_Ready.enabled = true;
                //GFX_Ready.enabled = true;
            }
            UpdateBoilingPotColor();
        }

        if(spriteChangeCountdown > 0)
        {
            spriteChangeCountdown -= Time.deltaTime;
            if(spriteChangeCountdown <= 0)
            {
                int spriteIndex = 0;

                do
                {
                    spriteIndex = Random.Range(1, waterSprites.Length - 1);
                }
                while (lastSpriteIndex == spriteIndex);

                waterSpriteRenderer.sprite = waterSprites[spriteIndex];
                lastSpriteIndex = spriteIndex;
                spriteChangeCountdown = SPRITE_CHANGE_INTERVAL;
            }
        }
    }
    void OnMouseDown()
    {
        if (isReady())
        {
            List<string> list = new List<string>();

            for (int i = 0; i < items.Count; ++i)
            {
                list.Add(items[i].id);
                Destroy(items[i].gameObject, 0.1f);
            }
            items.Clear();

            ResetBoilingPoint();
            spriteRenderer.color = Color.white;

            GameMgr gameMgr = FindObjectOfType<GameMgr>();
            gameMgr.CurrentVirus.CURE(list.ToArray());

            lastSpriteIndex = 0;
            waterSpriteRenderer.sprite = waterSprites[0];

            SFX_click.Play();

            readyGfxShown = false;

            GameObject gfx = Instantiate(GFXs[1]);
            gfx.transform.position = new Vector3(0, 0, 0);
            gfx.transform.localScale = new Vector3(3, 3, 3);
            Destroy(gfx, 2.0f);
            //spriteRenderer_Ready.enabled = false;
            //GFX_Ready.enabled = false;
        }
    }

    public bool isReady()
    {
        return boilingPoint >= maxBoilingPoint;
    }

    public void ResetBoilingPoint()
    {
        boilingPoint = 0;
        spriteChangeCountdown = 0;
        SFX_boil.Stop();
    }

    public void UpdateBoilingPotColor()
    {
        float factor = boilingPoint / maxBoilingPoint;
        Color color = new Color(factor, 1.0f - factor, 1.0f - factor);

        //spriteRenderer.color = new Color(factor, 1.0f - factor, 1.0f - factor);

        foreach (Item item in items)
        {
            float scaleFactor = 1.0f - factor / 2.0f;

            if (item.transform.localScale.x > scaleFactor)
            {
                item.SetColor(color);

                item.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            }
        }
    }

    public void UpdateBoilingPointCount()
    {
        maxBoilingPoint = items.Count * BOILING_RATE;
        if(boilingPoint >= maxBoilingPoint)
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

        if (items.Count == 0)
        {
            spriteChangeCountdown = SPRITE_CHANGE_INTERVAL;
            SFX_boil.Play();
        }

        readyGfxShown = false;
        items.Add(item);

        UpdateBoilingPointCount();

        SFX_addItem.Play();
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
