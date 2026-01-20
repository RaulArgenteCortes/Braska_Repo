using UnityEngine;

public class BlockResize : MonoBehaviour
{
    [Header("Resize Stats")]
    [SerializeField] float blockSize;
    [SerializeField] bool onXAxsis;
    private Renderer textureRenderer;
    private GameObject parentObject;

    private void Start()
    {
        textureRenderer = GetComponent<Renderer>();
        parentObject = transform.parent.gameObject;

        textureRenderer.material.mainTextureScale = new Vector2(
            (onXAxsis ? parentObject.transform.localScale.z : parentObject.transform.localScale.x) / blockSize,
            parentObject.transform.localScale.y / blockSize
        );

        ChangeOffSet();
    }

    private void ChangeOffSet()
    {
        if (onXAxsis)
        {
            textureRenderer.material.mainTextureOffset = new Vector2(
                transform.position.x % 2 == 0 ? 0 : 0.5f,
                transform.position.z % 2 == 0 ? 0 : 0.5f
            );
        }
    }
}
