using UnityEngine;

public class BlockResize : MonoBehaviour
{
    [Header("Resize Stats")]
    [SerializeField] bool onXAxsis;
    [SerializeField] Vector3 originPoint;
    private Renderer textureRenderer;
    private GameObject parentObject;

    private void Start()
    {
        textureRenderer = GetComponent<Renderer>();
        parentObject = transform.parent.gameObject;

        textureRenderer.material.mainTextureScale = new Vector2(
            (onXAxsis ? parentObject.transform.localScale.z : parentObject.transform.localScale.x),
            parentObject.transform.localScale.y
        );
    }
}