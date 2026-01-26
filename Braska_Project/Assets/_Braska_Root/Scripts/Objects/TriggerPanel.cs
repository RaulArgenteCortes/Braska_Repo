using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerPanel : MonoBehaviour
{
    public GameObject panelAactivar;
    public GameObject menuPausa;
    public GameObject menuoptions;


    public float velocidadFade = 4f; // segundos para fade
    public float velocidadLetra = 0.05f;

    public TextMeshProUGUI textoDialogo;
    [TextArea(2, 5)]
    public string[] dialogosIniciales;

    private bool playerInside = false;
    private int indiceDialogo = 0;
    private bool dialogoTerminado = false;
    private bool dialogoEnCurso = false;

    private string textoActual = "";
    private int letraActual = 0;

    public Graphic[] hijosPanel;

    private void Start()
    {
        
        foreach (var g in hijosPanel)
        {
            Color c = g.color;
            c.a = 0f;
            g.color = c;
        }
        panelAactivar.SetActive(true);
    }
    private void Update()
    {
        if (!playerInside) return;

        if (menuPausa.activeSelf || menuoptions.activeSelf)
        {
            panelAactivar.SetActive(false);
            return;
        }

        if (dialogoEnCurso && !panelAactivar.activeSelf)
        {
            panelAactivar.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            AvanzarDialogo();
        }
    }
    void AvanzarDialogo()
    {
        {
            if (dialogoTerminado) return;

            CancelInvoke(nameof(MostrarLetra));

            if (indiceDialogo < dialogosIniciales.Length)
            {
                textoActual = dialogosIniciales[indiceDialogo];
                letraActual = 0;
                textoDialogo.text = "";
                indiceDialogo++;
                dialogoEnCurso = true;

                // Mostrar texto letra por letra
                InvokeRepeating(nameof(MostrarLetra), 0f, velocidadLetra);
            }
            else
            {
                dialogoTerminado = true;
                dialogoEnCurso = false;
                InvokeRepeating(nameof(FadeOutHijos), 0f, 0.02f);
            }
        }
    }
    void MostrarLetra()
    {
        if (letraActual < textoActual.Length)
        {
            textoDialogo.text += textoActual[letraActual];
            letraActual++;
        }
        else
        {
            CancelInvoke(nameof(MostrarLetra));
        }
    }

    void FadeInHijos()
    {
        bool terminado = true;
        foreach (var g in hijosPanel)
        {
            Color c = g.color;
            c.a += 0.02f / velocidadFade;
            if (c.a > 1f) c.a = 1f;
            g.color = c;

            if (c.a < 1f) terminado = false;
        }

        if (terminado)
            CancelInvoke(nameof(FadeInHijos));
    }


    void FadeOutHijos()
    {
        bool terminado = true;
        foreach (var g in hijosPanel)
        {
            Color c = g.color;
            c.a -= 0.02f / velocidadFade;
            if (c.a < 0f) c.a = 0f;
            g.color = c;

            if (c.a > 0f) terminado = false;
        }

        if (terminado)
            CancelInvoke(nameof(FadeOutHijos));
    }



    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || playerInside) return;

        playerInside = true;

        if (dialogoTerminado) return;

        // Fade-in de todos los hijos al entrar
        CancelInvoke(nameof(FadeOutHijos));
        InvokeRepeating(nameof(FadeInHijos), 0f, 0.02f);

        dialogoEnCurso = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;
        dialogoEnCurso = false;
        CancelInvoke(nameof(MostrarLetra));
        CancelInvoke(nameof(FadeInHijos));
        InvokeRepeating(nameof(FadeOutHijos), 0f, 0.02f);
    }

}
