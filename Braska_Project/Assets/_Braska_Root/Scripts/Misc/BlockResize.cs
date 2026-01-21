using Unity.VisualScripting;
using UnityEngine;

public class BlockResize : MonoBehaviour
{
    [Header("Resize Stats")]
    [SerializeField] bool onXAxsis;
    [SerializeField] bool doubleSize;
    [SerializeField] Vector2 pain;
    private Renderer textureRenderer;
    private GameObject parentObject;

    private void Start()
    {
        textureRenderer = GetComponent<Renderer>();
        parentObject = transform.parent.gameObject;

        textureRenderer.material.mainTextureScale = new Vector2(
            (onXAxsis ? parentObject.transform.localScale.z : parentObject.transform.localScale.x) / (doubleSize ? 2 : 1),
            parentObject.transform.localScale.y / (doubleSize ? 2 : 1)
        );

        if (doubleSize)
            SizeAdjust();
    }

    private void SizeAdjust()
    {
        // Hace que el material no se altere por la posicion y escala del objeto padre.
        if (onXAxsis)
        {
            textureRenderer.material.mainTextureOffset = new Vector2(
                parentObject.transform.position.z / (transform.localPosition.x < 0 ? -2 : 2)
                    + parentObject.transform.localScale.z / -4,
                parentObject.transform.position.y / 2
                    + parentObject.transform.localScale.y / -4
            );
        }
        else
        {
            textureRenderer.material.mainTextureOffset = new Vector2(
                parentObject.transform.position.x / (transform.localPosition.z > 0 ? -2 : 2)
                    + parentObject.transform.localScale.x / -4,
                parentObject.transform.position.y / 2
                    + parentObject.transform.localScale.y / -4
            );
        }
    }

    private void Update()
    {
        textureRenderer.material.mainTextureScale = new Vector2(
            (onXAxsis ? parentObject.transform.localScale.z : parentObject.transform.localScale.x) / (doubleSize ? 2 : 1),
            parentObject.transform.localScale.y / (doubleSize ? 2 : 1)
        );

        SizeAdjust();
    }
}