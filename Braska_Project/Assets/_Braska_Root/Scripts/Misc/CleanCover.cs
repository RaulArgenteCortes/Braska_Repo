using UnityEngine;

public class CleanCover : MonoBehaviour
{
    [Header("Layer Stats")]
    [SerializeField] GameObject layerCheck;
    [SerializeField] float layerRadius;
    [SerializeField] LayerMask layerGround;
    private bool overlap;

    private void Start()
    {
        overlap = Physics.CheckSphere(layerCheck.transform.position, layerRadius, layerGround);

        if (overlap)
        {
            gameObject.SetActive(false);
        }
    }
}
