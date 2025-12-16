using UnityEngine;

public class CleanCover : MonoBehaviour
{
    [Header("Layer Stats")]
    [SerializeField] bool overlap;
    [SerializeField] LayerMask ground;
    [SerializeField] GameObject layerCheck;

    private void Start()
    {
        overlap = Physics.CheckSphere(transform.position, 0.01f, ground);

        if (overlap)
        {
            gameObject.SetActive(false);
        }
    }
}
