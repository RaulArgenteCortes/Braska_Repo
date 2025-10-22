using System.Threading;
using UnityEngine;

public class Geyser : MonoBehaviour
{

    [Header("Variables del geyser")]
    [SerializeField] float minheight = 1.0f;
    [SerializeField] float maxheight = 5.0f;
    [SerializeField] float speed = 2f;
    [SerializeField] float PauseTime = 1f;

    [Header("Curva de aceleración (opcional)")]
    [SerializeField]
    private AnimationCurve speedCurve =
       AnimationCurve.EaseInOut(0, 0, 1, 1);

    [SerializeField] bool growing = true;
    [SerializeField] float pausetimer = 0f;
    [SerializeField] float timer = 0f;
    private Vector3 basescale;
    private Vector3 baseposition;

    private void Start()
    {
        basescale = transform.localScale;
        baseposition = transform.localPosition;
    }
    void Update()
    {
        if (pausetimer > 0)
        {
            pausetimer -= Time.deltaTime;
            return;
        }

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer);

        float curveValue = speedCurve.Evaluate(t);

        float newHeight = growing
            ? Mathf.Lerp(minheight, maxheight, curveValue)
            : Mathf.Lerp(maxheight, minheight, curveValue);

        Vector3 scale = transform.localScale;
        scale.y = newHeight;
        transform.localScale = scale;

        float heightDiff = scale.y - basescale.y;
        transform.position = baseposition + new Vector3(0, heightDiff / 2f, 0);

        if (t >= 1f)
        {
            growing = !growing;
            timer = 0f;
            pausetimer = PauseTime;
        }
    }
   
}
