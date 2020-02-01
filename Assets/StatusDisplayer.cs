using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusDisplayer : MonoBehaviour
{
    public Text text_status;
    public float COUNT_DOWN = 2;
    private float countDown = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown > 0 && text_status.text.Length > 0)
        {
            countDown -= Time.deltaTime;

            if (countDown <= 0)
            {
                countDown = 0;
                text_status.text = "";
            }
        }
    }

    public void Display(string status)
    {
        text_status.text = status;
        countDown = COUNT_DOWN;
    }
}
