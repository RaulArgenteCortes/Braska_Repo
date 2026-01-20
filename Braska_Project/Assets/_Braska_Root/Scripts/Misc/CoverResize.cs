using UnityEngine;

public class CoverResize : MonoBehaviour
{
    [Header("Resize Stats")]
    private Renderer textureRenderer;

    void Start()
    {
        textureRenderer = GetComponent<Renderer>();

        Resize();
    }

    private void Resize()
    {
        textureRenderer.material.mainTextureScale = new Vector2(
            0.5f,
            0.5f
        );

        textureRenderer.material.mainTextureOffset = new Vector2(
            transform.position.x % 2 == 0 ? 0 : 0.5f,
            transform.position.z % 2 == 0 ? 0 : 0.5f
        );
    }
}
