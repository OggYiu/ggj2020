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

    private float orig_dead_rate;
    private float orig_cure_rate;
    private float orig_infection_rate;

    // Start is called before the first frame update
    void Start()
    {
        orig_dead_rate = dead_rate;
        orig_cure_rate = cure_rate;
        orig_infection_rate = infection_rate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CURE(string[] items)
    {
        int cure_count = cures.Length;
        int correct_count = 0;
        List<string> correctedCure = new List<string>(cures);

        foreach (string item in items)
        {
            foreach (string cure in correctedCure)
            {
                if (cure == item)
                {
                    correct_count++;
                    correctedCure.Remove(cure);
                    break;
                }
            }
        }

        Debug.Log("correct_count: " + correct_count);
        Debug.Log("cure_count: " + cure_count);

        if (correct_count == cure_count)
        {
            infection_rate = 1f;
            cure_rate = 1f;
            return true;
        }
        else if(correct_count > 0)
        {
            infection_rate = ((float)(orig_infection_rate - 1) * (1f - ((float)correct_count / (float)cure_count))) + 1f;

            dead_rate = (float)orig_dead_rate * (1f - ((float)correct_count / (float)cure_count));
            //cure_rate = cure_rate * 2;

            Debug.Log("infection_rate: " + infection_rate);
            Debug.Log("dead_rate: " + dead_rate);
            Debug.Log("cure_rate: " + cure_rate);
        }
        else if (correct_count == 0)
        {
            dead_rate = orig_dead_rate;
            cure_rate = orig_cure_rate;
            infection_rate = orig_infection_rate;
        }

        return false;
    }
}
