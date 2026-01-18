using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlackManager : MonoBehaviour
{
    public static FadeToBlackManager Instance;

    public Image blackScreen;
    public float timeToFade = 0.3f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        FadeToBlack(false, 3f);
    }

    public void FadeToBlack(bool toBlack, float time)
    {
        StartCoroutine(Fade(toBlack, time));

    }

    // Fade the screen to black or from black
    IEnumerator Fade(bool toBlack, float time)
    {
        if (toBlack)
        {
            float elapsedTime = 0f;
            Color color = blackScreen.color;
            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Clamp01(elapsedTime / time);
                blackScreen.color = color;
                yield return null;
            }
            color.a = 1f;
            blackScreen.color = color;
        }
        else
        {
            float elapsedTime = 0f;
            Color color = blackScreen.color;
            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                color.a = 1f - Mathf.Clamp01(elapsedTime / time);
                blackScreen.color = color;
                yield return null;
            }
            color.a = 0f;
            blackScreen.color = color;
        }

    }
}