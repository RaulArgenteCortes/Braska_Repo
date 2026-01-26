using TMPro;
using UnityEngine;

public class TriggerPanel : MonoBehaviour
{
    public GameObject panelAactivar;
    public GameObject menuPausa;
    public GameObject menuoptions;


    public TextMeshProUGUI textoDialogo;
    [TextArea(2, 5)]
    public string[] dialogosIniciales;
    [TextArea(2, 5)]
    public string[] dialogosRepetidos;

    private bool playerInside = false;
    private int indiceDialogo = 0;
    private bool dialogoTerminado = false;
    private bool usandoDialogoRepetido = false;
    private bool dialogoEnCurso = false;
    private bool repetidosMostrados = false;


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
        string[] dialogosActuales = usandoDialogoRepetido ? dialogosRepetidos : dialogosIniciales;

        if (indiceDialogo + 1 <= dialogosActuales.Length)
        {
            indiceDialogo++;
        }

        if (indiceDialogo < dialogosActuales.Length)
        {
            textoDialogo.text = dialogosActuales[indiceDialogo];
            dialogoEnCurso = true;
        }
        else
        {
            panelAactivar.SetActive(false);
            dialogoTerminado = true;
            dialogoEnCurso = false;

            if (usandoDialogoRepetido)
            {
                repetidosMostrados = true;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (playerInside) return;

        playerInside = true;
        panelAactivar.SetActive(true);

        usandoDialogoRepetido = dialogoTerminado && !repetidosMostrados;
        dialogoEnCurso = true;

        string[] dialogosActuales = usandoDialogoRepetido ? dialogosRepetidos : dialogosIniciales;
        textoDialogo.text = dialogosActuales[indiceDialogo];
    }


    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;
        panelAactivar.SetActive(false);
        if (dialogoTerminado)
        {
            indiceDialogo = 0;
        }
        dialogoEnCurso = false;
    }


}
