using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorAnimator : MonoBehaviour
{
    public Sprite mouthSpriteDefault;
    public SpriteRenderer mouthSpriteRenderer;
    public Animator mouthAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartAnimMouth()
    {
        mouthAnimator.enabled = true;
    }

    public void EndAnimMouth()
    {
        mouthAnimator.enabled = false;
        mouthSpriteRenderer.sprite = mouthSpriteDefault;
    }
}
