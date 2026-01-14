using Unity.VisualScripting;
using UnityEngine;

public class RuneTriggerBird : MonoBehaviour
{
    [Header("a")]
    [SerializeField] GameObject birdPoint;
    [SerializeField] GameObject bird;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bark") && ObjectManager.instance.runeCanTrigger && bird.transform.position == birdPoint.transform.position)
        {
            bird.GetComponent<RuneBird>().onPointA = !bird.GetComponent<RuneBird>().onPointA;

            ObjectManager.instance.RunePrepareMove();
        }
    }
}
