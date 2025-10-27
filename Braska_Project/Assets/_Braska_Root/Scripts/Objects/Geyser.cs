using System.Threading;
using UnityEngine;

public class Geyser : MonoBehaviour
{

    [Header("Variables del geyser")]
    [SerializeField] float minheight = 1.0f;
    [SerializeField] float maxheight = 5.0f;
    [SerializeField] float PauseTime = 1f;

    [Header("Curva de aceleraci�n (opcional)")]
    [SerializeField] private AnimationCurve speedCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [SerializeField] bool growing = true;
    [SerializeField] float pausetimer = 0f;
    [SerializeField] float timer = 0f;

    private Vector3 basescale;
    private Vector3 baseposition;
    private Vector3 lastPosition;
    private Rigidbody rb;

    private Rigidbody playerRb;
    private bool playerOnTop = false;


    private void Start()
    {
        basescale = transform.localScale;
        baseposition = transform.localPosition;
        lastPosition = transform.position + Vector3.up * basescale.y / 2f;

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

       
    }
    private void FixedUpdate()
    {
        if (pausetimer > 0)
        {
            pausetimer -= Time.fixedDeltaTime;
            return;
        }

        timer += Time.fixedDeltaTime;
        float t = Mathf.Clamp01(timer);
        float curveValue = speedCurve.Evaluate(t);

        float newHeight = growing
            ? Mathf.Lerp(minheight, maxheight, curveValue)
            : Mathf.Lerp(maxheight, minheight, curveValue);

        float deltaHeight = newHeight - basescale.y;

        Vector3 scale = basescale;
        scale.y = newHeight;
        transform.localScale = scale;

        Vector3 targetPos = transform.position + new Vector3(0, deltaHeight / 2f, 0);
        rb.MovePosition(targetPos);

        Vector3 topPosition = targetPos + Vector3.up * newHeight / 2f;
        Vector3 moveDelta = topPosition - lastPosition;

        if (playerOnTop && playerRb != null)
        {
            playerRb.position += moveDelta;
        }
        lastPosition = topPosition;
        basescale.y = newHeight;


        if (t >= 1f)
        {
            growing = !growing;
            timer = 0f;
            pausetimer = PauseTime;
        }

    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRb = collision.rigidbody;
            playerOnTop = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnTop = false;
            playerRb = null;
        }
    }


}
