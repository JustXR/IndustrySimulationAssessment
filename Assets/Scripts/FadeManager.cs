using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2.0f;

    private void Start()
    {
        // Ensure the image is fully transparent at the start
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }
}
