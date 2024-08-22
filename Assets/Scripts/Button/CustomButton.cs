using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        //커서 진입
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        //사운드 재생
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        //커서 진입
    }
}
