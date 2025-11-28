using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectorFeedback : MonoBehaviour
{
    public GameObject arms; // objeto que contiene las patas

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (arms != null)
            arms.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (arms != null)
            arms.SetActive(false);
    }

}
