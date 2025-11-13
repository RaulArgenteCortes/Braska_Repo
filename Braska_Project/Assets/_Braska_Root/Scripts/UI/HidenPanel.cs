using UnityEngine;

public class HidePanel : MonoBehaviour
{
    public GameObject panel;   // Asigna tu panel aquí en el Inspector

    void Start()
    {
        // Llamar a "Hide" después de 30 segundos
        Invoke("Hide", 30f);
    }

    void Hide()
    {
        panel.SetActive(false);
    }
}
