using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class Olfatear : MonoBehaviour
{
    #region "Variables"
    [Header("Variables Excavar")]
    [SerializeField] GameObject Waypoint;
    [SerializeField] GameObject orbe;
    [SerializeField] GameObject excavar;
    [SerializeField] bool luzEncendida = false;
    [SerializeField] TextoExcavar textoexcavar;

    [Header("Vairables Orbe")]
    [SerializeField] Transform perro;
    [SerializeField] float velocidaddeSeguimiento = 3f;
    [SerializeField] float distanciaDetras = 1f;
    [SerializeField] float alturaOrbe = 1.5f;
    [SerializeField] bool OrbeActivo = false;
    [SerializeField] Vector3 velocidadSuavizada = Vector3.zero;

    #endregion
    #region "Voids"
    public void OnRastrearExcavar()
    {
        Waypoint.SetActive(true);
        excavar.SetActive(true);
        luzEncendida = true;
        if (textoexcavar.PerroEnTrigger && luzEncendida)
        {
            orbe.gameObject.SetActive(true);
            OrbeActivo = true;

            Collider col = orbe.GetComponent<Collider>();
            if (col) col.enabled = false;

            Vector3 puntoAparicion = excavar.transform.position + Vector3.up * 0.2f;

            float distanciaPerro = Vector3.Distance(perro.position, puntoAparicion);
            if (distanciaPerro < 0.8f)
            {
                puntoAparicion += perro.right * 0.8f;
            }

            orbe.transform.position = puntoAparicion;

            Vector3 destinoElevado = puntoAparicion + Vector3.up * 0.8f;
            orbe.transform.position = Vector3.Lerp(orbe.transform.position, destinoElevado, 0.5f);

           
        }
    }

   
    private void Update()
    {
        if (OrbeActivo  && perro != null)
        {
            Vector3 destino = perro.position - perro.forward * distanciaDetras + Vector3.up * alturaOrbe;

            // Movimiento fluido detrás del perro
            orbe.transform.position = Vector3.SmoothDamp(
                orbe.transform.position,
                destino,
                ref velocidadSuavizada,
                1f / velocidaddeSeguimiento
            );

            // Rotación mágica
            orbe.transform.Rotate(Vector3.up * 60f * Time.deltaTime, Space.World);
        }
    }
}
#endregion