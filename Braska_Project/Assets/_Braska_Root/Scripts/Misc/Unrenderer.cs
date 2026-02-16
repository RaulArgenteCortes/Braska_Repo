using UnityEngine;

public class Unrenderer : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }
}
