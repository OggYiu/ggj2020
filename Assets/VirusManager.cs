using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusManager : MonoBehaviour
{
    // Next update in second
    private int nextUpdate = 1;

    public long population = 7700000000;
    public long infected_population = 0;
    public long killed_population = 0;

    private GameMgr gameMgr;
    private Virus current_virus;

    // Start is called before the first frame update
    void Start()
    {
        gameMgr = FindObjectOfType<GameMgr>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the next update is reached
        if (Time.time >= nextUpdate)
        {
            Debug.Log(Time.time + ">=" + nextUpdate);
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            // Call your fonction
            UpdateEverySecond();
        }
    }

    void UpdateEverySecond()
    {
        if (current_virus == null)
        {
            current_virus = gameMgr.CurrentVirus;
            infected_population = current_virus.init_infection;
        }
        else
        {
            if (population > 0)
            {
                long orig_infected_popultation = infected_population;
                infected_population = (long)((float)infected_population * current_virus.infection_rate);
                if (infected_population >= population)
                {
                    infected_population = population;
                }

                long orig_killed_population = killed_population;
                killed_population = (long)((float)infected_population * current_virus.dead_rate);
                infected_population = infected_population - killed_population + orig_killed_population;

                population -= killed_population;
                if (population - killed_population <= 0)
                {
                    population = 0;
                }

                gameMgr.SetTextInfected((long)infected_population);
                gameMgr.SetTextHealthy((long)population);
                gameMgr.SetTextDead((long)killed_population);

                Debug.Log("Population: " + population);
            }
            else
            {
                Debug.Log("Population: " + population);
                Debug.Log("GAME OVER");
            }
        }
    }
}
