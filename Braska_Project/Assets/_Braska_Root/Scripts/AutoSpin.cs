using UnityEngine;

public class AutoSpin : MonoBehaviour
{
    private void Update()
    {
        transform.eulerAngles += new Vector3(
            0,
            -30,
            0
        ) * Time.deltaTime;
    }
}
