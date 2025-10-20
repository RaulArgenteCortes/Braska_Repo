using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class Olfatear : MonoBehaviour
{
    #region "Variables"
    [SerializeField] GameObject Waypoint;
    [SerializeField] GameObject orbe;
    [SerializeField] GameObject excavar;
    [SerializeField] bool luzEncendida = false;
    [SerializeField] bool PerroEnTrigger = false;
    [SerializeField] TextoExcavar textoexcavar;
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
        }
    }
    #endregion

}


