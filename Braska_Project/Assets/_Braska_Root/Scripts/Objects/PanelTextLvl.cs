    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;           

    public class PanelTextLvl : MonoBehaviour
{
    [Header("UI Settings")]
    public TMP_Text[] texts;
    public Image image;
    public float fadeDuration = 1f;
    public float fadeOutDuration = 0.3f;
    public float delayBeforeFade = 0.5f;

    float fadeSpeedIn;
    float fadeSpeedOut;

    // Flag para saber si el fade in ya comenzó
    bool fadeInStarted = false;

    void Awake()
    {
        fadeSpeedIn = 1f / fadeDuration;
        fadeSpeedOut = 1f / fadeOutDuration;

        SetAlpha(0f);
    }

    public void FadeIn()
    {
        CancelInvoke();
        fadeInStarted = false;
        Invoke(nameof(StartFadeIn), delayBeforeFade);
    }

    void StartFadeIn()
    {
        fadeInStarted = true;
        InvokeRepeating(nameof(UpdateFadeIn), 0f, 0.01f);
    }
    void SetAlpha(float alpha)
    {
        foreach (TMP_Text t in texts)
        {
            if (t == null) continue;
            Color c = t.color;
            c.a = alpha;
            t.color = c;
        }
        if (image != null)
        {
            Color c = image.color;
            c.a = alpha;
            image.color = c;
        }

    }
    void UpdateFadeIn()
    {
        float currentAlpha = GetCurrentAlpha();
        float newAlpha = Mathf.Clamp01(currentAlpha + fadeSpeedIn * 0.01f);

        SetAlpha(newAlpha);

        if (newAlpha >= 1f)
            CancelInvoke(nameof(UpdateFadeIn));
    }

    public void FadeOut()
    {
        if (!fadeInStarted)
            CancelInvoke(nameof(StartFadeIn));

        CancelInvoke(nameof(UpdateFadeIn));
        InvokeRepeating(nameof(UpdateFadeOut), 0f, 0.01f);
    }

    void UpdateFadeOut()
    {
        float currentAlpha = GetCurrentAlpha();
        float newAlpha = Mathf.Clamp01(currentAlpha - fadeSpeedOut * 0.01f);

        SetAlpha(newAlpha);

        if (newAlpha <= 0f)
            CancelInvoke(nameof(UpdateFadeOut));
    }
    float GetCurrentAlpha()
    {
        foreach (TMP_Text t in texts)
        {
            if (t != null)
                return t.color.a;
        }
        if (image != null)
            return image.color.a;

        return 0f;
    }
}