using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadeEffect : MonoBehaviour
{

    [SerializeField] private Image fadeImage;

    public void ScreenFade(float targetAlpha, float duration, System.Action option = null)
    {
        StartCoroutine(FadeCoroutine(targetAlpha, duration,option));
    }


    private IEnumerator FadeCoroutine(float targetAlpha, float duration, System.Action option)
    {
        float time = 0;
        Color currentColor = fadeImage.color;

        float startAlpha = currentColor.a;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);

            fadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }

        if(option != null)
        {
            option.Invoke(); // if option method is not null, then invoke it
        }

    }

}
