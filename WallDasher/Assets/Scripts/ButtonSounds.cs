using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlayButtonHover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlayButtonClick();
    }
}
