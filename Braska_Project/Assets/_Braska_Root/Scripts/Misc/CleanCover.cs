using UnityEngine;

public class CleanCover : MonoBehaviour
{
    [Header("Layer Stats")]
    [SerializeField] GameObject layerCheckOne;
    [SerializeField] GameObject layerCheckTwo;
    [SerializeField] float layerRadius;
    [SerializeField] LayerMask layerToCheck;
    private bool overlap;

    private void Start()
    {
        overlap = 
            Physics.CheckSphere(layerCheckOne.transform.position, layerRadius, layerToCheck)
            && Physics.CheckSphere(layerCheckTwo.transform.position, layerRadius, layerToCheck);

        if (overlap)
        {
            gameObject.SetActive(false);
        }
    }
}
