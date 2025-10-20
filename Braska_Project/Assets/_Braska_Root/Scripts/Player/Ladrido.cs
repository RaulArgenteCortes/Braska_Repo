using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ladrido : MonoBehaviour
{
    #region "Variables"
    [Header("Configuración de ladrido")]
    [SerializeField] float Barkradius;
    [SerializeField] float Barkcooldown;
    [SerializeField] LayerMask affectedmask;

    [Header("Acciónes")]
    [SerializeField] bool canBark = true;
    [SerializeField] InputAction Barkaction;
    public event System.Action OnBarkEvent;
    #endregion
    #region "voids" 
    void Update()
    {
        if (Barkaction != null && Barkaction.WasPerformedThisFrame())
        {
            Bark();
        }
    }
    public void Bark()
    {
        if (!canBark) return;

        canBark = false; 

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Barkradius, affectedmask);
        foreach (Collider hit in hitColliders)
        {
            AfectadoLadrido barkable = hit.GetComponent<AfectadoLadrido>();
            if (barkable != null)
                barkable.OnBarked();
        }

        OnBarkEvent?.Invoke();

        Invoke(nameof(ResetBark), Barkcooldown);
    }

    private void ResetBark()
    {
        canBark = true;
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0.5f, 0.1f, 0.3f);
        Gizmos.DrawSphere(transform.position, Barkradius);
    }

    public void SetCanBark(bool value)
    {
        canBark = value;
    }
    #endregion
}
