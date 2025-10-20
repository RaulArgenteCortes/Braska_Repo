using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextoExcavar : MonoBehaviour
{
    #region "Variables"
    [SerializeField] private TMP_Text textoTMP;
    public bool PerroEnTrigger = false;
    #endregion
    #region "Voids Trigggers"
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textoTMP.gameObject.SetActive(true);
            PerroEnTrigger = true;
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textoTMP.gameObject.SetActive(false);
            PerroEnTrigger = false;
        }
       
    }
    #endregion
}
