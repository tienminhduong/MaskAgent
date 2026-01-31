using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Transition : Singleton<Transition>
{
    [SerializeField] private Image fadeImage;
    private float fadeDuration = 1.5f;

    private Coroutine fadeCoroutine;

    protected override void Awake()
    {
        base.Awake();
        if (fadeImage == null)
            fadeImage = GetComponent<Image>();
    }
    private void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        StartFade(1f, 0f); // đen -> trong suốt
    }

    public void FadeOut()
    {
        StartFade(0f, 1f); // trong suốt -> đen
    }

    private void StartFade(float from, float to)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeCoroutine(from, to));
    }

    private IEnumerator FadeCoroutine(float from, float to)
    {
        if (fadeImage == null)
            fadeImage = GetComponent<Image>();

        float time = 0f;
        Color color = fadeImage.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            float alpha = Mathf.Lerp(from, to, time / fadeDuration);
            color.a = alpha;
            fadeImage.color = color;

            yield return null;
        }

        color.a = to;
        fadeImage.color = color;

        fadeCoroutine = null;
    }
}
