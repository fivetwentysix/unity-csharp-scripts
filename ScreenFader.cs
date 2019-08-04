using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Usage:

Create a UI RawImage object on the screen, attach this script to that rawImage.
Link the RawImage object to the rawImage property on this component.

In play mode press the `'` key to trigger.
 */
public class ScreenFader : MonoBehaviour
{
    public RawImage rawImage;
    public byte alpha;
    public float fadeTime = 2.0f;
    public bool fadeIn = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // If the user presses `'`, fade in or out.
        if (Input.GetKeyDown(KeyCode.Quote))
        {
            if (fadeIn)
            {
                StartCoroutine(this.StartFadeOut());
                this.fadeIn = false;
            }
            else
            {
                StartCoroutine(this.StartFadeIn());
                this.fadeIn = true;
            }
        }
    }

    private YieldInstruction fadeInstruction = new YieldInstruction();
    IEnumerator StartFadeOut()
    {
        float elapsedTime = 0.0f;
        Color c = rawImage.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = 0.0f + Mathf.Clamp01(elapsedTime / fadeTime);
            rawImage.color = c;
        }
    }

    IEnumerator StartFadeIn()
    {
        float elapsedTime = 0.0f;
        Color c = rawImage.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            rawImage.color = c;
        }
    }
}
