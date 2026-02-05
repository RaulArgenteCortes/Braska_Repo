using UnityEngine;

public class CreditScroll : MonoBehaviour
{
    
        public float speed = 60f;
    

 
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
