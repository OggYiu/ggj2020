using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    public string virus_name = "";
    public long init_infection = 100;
    public float infection_rate = 2.5f;
    public float dead_rate = 0.1f;
    public float cure_rate = 0.1f;
    public string[] cures;
    public string[] symptoms;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CURE(string[] items)
    {
        int cure_count = cures.Length;
        int correct_count = 0;
        foreach (string item in items)
        {
            foreach (string cure in cures)
            {
                if (cure == item)
                {
                    correct_count++;
                }
            }
        }

        if (correct_count == cure_count)
        {
            infection_rate = 1;
            cure_rate = 0.5f;
            return true;
        }
        else if(correct_count > 0)
        {
            infection_rate = infection_rate * (correct_count / cure_count);
            dead_rate = dead_rate * (correct_count / cure_count);

            Debug.Log("infection_rate: " + infection_rate);
            Debug.Log("dead_rate: " + dead_rate);
        }

        return false;
    }
}
