using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{

    #region "Variables"
    [SerializeField] private Transform PuntoA;
    [SerializeField] private Transform PuntoB;
    [SerializeField] private float velocidad = 2f;

    [SerializeField] bool enMovimiento = false;
    [SerializeField] bool HaciaB = true;
    #endregion
    #region "Void Update"
    private void Update()
    {
        if (!enMovimiento) return;

        Transform destino = HaciaB ? PuntoB : PuntoA;

        transform.position = Vector3.MoveTowards(transform.position, destino.position, velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position, destino.position) < 0.05f)
        {
            if (HaciaB)
            {
                HaciaB = false;
            }
            else
            {
                enMovimiento = false;
            }
        }
    }
    #endregion
    #region "Activar y desactivar movimiento plataforma"
    public void ActivarMovimiento()
    {
      
        enMovimiento = true;
        HaciaB = true;
    }
    public void DetenerMovimiento()
    {
        enMovimiento = false;
    }
    #endregion
    #region "Collision pj y plataforma"
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
    #endregion
}

