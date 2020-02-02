using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedTextDisplayer : MonoBehaviour
{
    public Text text_speech;
    public string currentSpeechText;
    public float DISPLAY_RATE = 10.0f;
    public float DISAPPEAR_AFTER = 2.0f;
    private float rateAccum = 0f;
    private float disappearAccum = 0f;
    public AnchorAnimator anchorAnimator;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSpeechText.Length > 0 && disappearAccum <= 0)
        {
            rateAccum += Time.deltaTime * DISPLAY_RATE;
            int wordsToDisplay = (int)(rateAccum);
            if(wordsToDisplay > currentSpeechText.Length)
            {
                wordsToDisplay = currentSpeechText.Length;
                disappearAccum = DISAPPEAR_AFTER;

                if (anchorAnimator != null) { anchorAnimator.EndAnimMouth(); }
            }
            text_speech.text = currentSpeechText.Substring(0, wordsToDisplay);
        }

        if(disappearAccum > 0)
        {
            disappearAccum -= Time.deltaTime;
            if(disappearAccum <= 0)
            {
                currentSpeechText = "";
                text_speech.text = "";
            }
        }
    }

    public void Display(string speechText)
    {
        anchorAnimator.StartAnimMouth();
        currentSpeechText = speechText;
        rateAccum = 0;
        disappearAccum = 0;
    }
}
