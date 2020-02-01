using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedTextDisplayer : MonoBehaviour
{
    public Text text_speech;
    public string currentSpeechText;
    public float displayRate = 10.0f;
    private float rateAccum = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSpeechText.Length > 0)
        {
            rateAccum += Time.deltaTime * displayRate;
            int wordsToDisplay = (int)(rateAccum);
            if(wordsToDisplay > currentSpeechText.Length)
            {
                wordsToDisplay = currentSpeechText.Length;
            }
            text_speech.text = currentSpeechText.Substring(0, wordsToDisplay);
        }
    }

    public void Display(string speechText)
    {
        currentSpeechText = speechText;
        rateAccum = 0;
    }
}
