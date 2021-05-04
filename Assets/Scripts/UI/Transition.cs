using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    private void Start()
    {
        FadeOut(2f, null);
    }

    public void FadeIn(float duration, Action onFadeIn, float delay = 0f)
    {
        StartCoroutine(FadeInCoroutine(duration, onFadeIn, delay));
    }

    public void FadeOut(float duration, Action onFadeOut, float delay = 0f)
    {
        StartCoroutine(FadeOutCoroutine(duration, onFadeOut, delay));
    }

    IEnumerator FadeOutCoroutine(float duration, Action onFadeOut, float delay)
    {
        if(delay > 0f)
            yield return new WaitForSeconds(delay);

        Color color = fadeImage.color;

        for(float alpha = 1f; alpha >= 0f; alpha -= Time.deltaTime / duration)
        {
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, 0f);
        fadeImage.enabled = false;
        onFadeOut?.Invoke();
    }

    IEnumerator FadeInCoroutine(float duration, Action onFadeIn, float delay)
    {
        if(delay > 0f)
            yield return new WaitForSeconds(delay);

        fadeImage.enabled = true;
        Color color = fadeImage.color;

        for(float alpha = 0f; alpha <= 1f; alpha += Time.deltaTime / duration)
        {
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, 1f);
        onFadeIn?.Invoke();
    }
}
