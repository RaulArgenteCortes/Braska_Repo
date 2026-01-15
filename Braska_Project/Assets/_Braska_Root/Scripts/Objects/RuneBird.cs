using Unity.VisualScripting;
using UnityEngine;

public class RuneBird : MonoBehaviour
{
    [Header("a")]
    public bool onPointA;
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;

    private void Start()
    {
        onPointA = true;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            onPointA ? pointA.transform.position : pointB.transform.position,
            5 * Time.fixedDeltaTime
        );
    }
}
