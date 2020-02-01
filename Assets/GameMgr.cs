using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    public Transform ItemObjsParent;
    public ItemObj[] itemObjs;
    public Text text_infected;
    public Text text_healthy;
    public Text text_dead;
    public Text text_infectedRate;
    public Text text_healthyRate;
    public Text text_deadRate;
    public float infected_count = 0;
    public float dead_count = 0;
    public float healthy_count = 0;
    public float infected_rate = 1;
    public float dead_rate = 2;
    public float healthy_rate = 3;

    private SpeedTextDisplayer speedTextDisplayer;
    public StatusDisplayer potStatusDisplayer;
    public StatusDisplayer gameStatusDisplayer;

    // Start is called before the first frame update
    void Start()
    {
        speedTextDisplayer = FindObjectOfType<SpeedTextDisplayer>();

        SetInfectedRate(3);
        SetDeadRate(1);
        SetHealthyRate(2);

        DisplaySpeechText("hello, how are you? I am good!");
        DisplayPotStatus("Pot Ready!");
        DisplayGameStatus("Game Start!");
    }

    // Update is called once per frame
    void Update()
    {
        // test
        infected_count += Time.deltaTime * infected_rate;
        dead_count += Time.deltaTime * dead_rate;
        healthy_count += Time.deltaTime * healthy_rate;

        SetTextInfected((int)infected_count);
        SetTextHealthy((int)healthy_count);
        SetTextDead((int)dead_count);
    }

    public void OnItemClicked(Item item)
    {
        Assert.IsTrue(item.id.Length > 0, "Item needs an id");

        ItemObj targetItemObj = null;
        foreach(ItemObj itemObj in itemObjs)
        {
            if(itemObj.GetComponent<Item>().id == item.id)
            {
                targetItemObj = itemObj;
                break;
            }
        }
        Assert.IsTrue(targetItemObj != null, "Invalid target Item Obj: " + item.id);

        ItemObj obj = Instantiate<ItemObj>(targetItemObj);
        float minX = -3.0f;
        float maxX = 3.0f;
        float minY = 6.2f;
        obj.transform.parent = ItemObjsParent;
        obj.transform.position = new Vector3(Random.Range(minX, maxX), minY);
    }

    public void SetInfectedCount(float count)
    {
        infected_count = count;
        SetTextInfected((int)infected_count);
    }

    public void SetDeadCount(float count)
    {
        dead_count = count;
        SetTextHealthy((int)dead_count);
    }

    public void SetHealthyCount(float count)
    {
        healthy_count = count;
        SetTextDead((int)healthy_count);
    }

    public void SetTextInfected(int ppl)
    {
        text_infected.text = "Infected: " + ppl;
    }

    public void SetTextHealthy(int ppl)
    {
        text_healthy.text = "Healthy: " + ppl;
    }

    public void SetTextDead(int ppl)
    {
        text_dead.text = "Dead: " + ppl;
    }

    public void SetInfectedRate(float rate)
    {
        infected_rate = rate;
        SetTextInfectedRate((int)infected_rate);
    }

    public void SetHealthyRate(float rate)
    {
        healthy_rate = rate;
        SetTextHealthyRate((int)healthy_rate);
    }

    public void SetDeadRate(float rate)
    {
        dead_rate = rate;
        SetTextDeadRate((int)dead_rate);
    }

    public void SetTextInfectedRate(int rate)
    {
        text_infectedRate.text = (rate > 0 ? "↑" : "↓") + ": " + rate;
    }

    public void SetTextHealthyRate(int rate)
    {
       text_healthyRate.text = (rate > 0 ? "↑" : "↓") + ": " + rate;
    }

    public void SetTextDeadRate(int rate)
    {
        text_deadRate.text = (rate > 0 ? "↑" : "↓") + ": " + rate;
    }

    public void DisplaySpeechText(string speechText)
    {
        speedTextDisplayer.Display(speechText);
    }

    public void DisplayPotStatus(string status)
    {
        potStatusDisplayer.Display(status);
    }

    public void DisplayGameStatus(string status)
    {
        gameStatusDisplayer.Display(status);
    }
}
