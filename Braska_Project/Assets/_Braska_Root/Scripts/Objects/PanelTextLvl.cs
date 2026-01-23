    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;           

    public class PanelTextLvl : MonoBehaviour
{
    [Header("UI Settings")]
    public TMP_Text textUI;
    public TMP_Text[] texts;
    public float fadeDuration = 1f;
    public float fadeOutDuration = 0.3f;
    public float delayBeforeFade = 0.5f;

    float fadeSpeedIn;
    float fadeSpeedOut;

    // Flag para saber si el fade in ya comenzó
    bool fadeInStarted = false;

    void Awake()
    {
        if (textUI != null)
        {
            Color c = textUI.color;
            c.a = 0f;
            textUI.color = c;
        }

        fadeSpeedIn = 1f / fadeDuration;
        fadeSpeedOut = 1f / fadeOutDuration;
    }

    public void FadeIn()
    {
        if (textUI == null) return;

        // Cancelar cualquier fade out o fade in existente
        CancelInvoke(nameof(UpdateFadeOut));
        CancelInvoke(nameof(UpdateFadeIn));

        fadeInStarted = false; // Resetear flag
        Invoke(nameof(StartFadeIn), delayBeforeFade);
    }

    void StartFadeIn()
    {
        // Marcar que el fade in ya empezó
        fadeInStarted = true;
        InvokeRepeating(nameof(UpdateFadeIn), 0f, 0.01f);
    }

    void UpdateFadeIn()
    {
        if (textUI == null) return;

        Color c = textUI.color;
        c.a += fadeSpeedIn * 0.01f;
        if (c.a > 1f) c.a = 1f;
        textUI.color = c;

        if (c.a >= 1f)
            CancelInvoke(nameof(UpdateFadeIn));
    }

    public void FadeOut()
    {
        if (textUI == null) return;

        // Cancelar fade in si aún no empezó
        if (!fadeInStarted)
        {
            CancelInvoke(nameof(StartFadeIn));
        }

        // Cancelar cualquier fade in o fade out activo
        CancelInvoke(nameof(UpdateFadeIn));
        CancelInvoke(nameof(UpdateFadeOut));

        InvokeRepeating(nameof(UpdateFadeOut), 0f, 0.01f);
    }

    void UpdateFadeOut()
    {
        if (textUI == null) return;

        Color c = textUI.color;
        c.a -= fadeSpeedOut * 0.01f;
        if (c.a < 0f) c.a = 0f;
        textUI.color = c;

        if (c.a <= 0f)
            CancelInvoke(nameof(UpdateFadeOut));
    }
}