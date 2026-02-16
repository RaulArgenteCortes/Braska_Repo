using UnityEngine;

public class FootPrints : MonoBehaviour
{

    [SerializeField] float lifeTime = 3f;
    [SerializeField] float fadeTime = 1.5f;

    Renderer rend;
    Color startColor;

    bool fading = false;
    float fadeTimer = 0f;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        Invoke(nameof(StartFade), lifeTime);
    }

    void StartFade()
    {
        fading = true;
    }

    void Update()
    {
        if (!fading) return;

        fadeTimer += Time.deltaTime;
        float alpha = Mathf.Lerp(startColor.a, 0f, fadeTimer / fadeTime);
        rend.material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

        if (fadeTimer >= fadeTime)
        {
            Destroy(gameObject);
        }
    }
}

