using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfectadoLadrido : MonoBehaviour
{
    #region"Variables"
    [Header("Plataformas")]
    [SerializeField] private PlatformMove[] plataformas;

    #endregion
    #region "Voids"
    public void OnBarked()
    {
    

        foreach (PlatformMove p in plataformas)
        {
                p.ActivarMovimiento();
        }
  
    }
    #endregion
}
