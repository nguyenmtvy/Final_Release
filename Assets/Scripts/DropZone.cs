using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    [SerializeField] Boat boat;
    private bool inDropZone = false;
    private bool isArrived = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
    private void Update()
    {
        if (boat.getArrived())
        {
            isArrived = true;
        }
        else
        {
            isArrived = false;
        }
    }

    public bool isInDropZone()
    {
        return inDropZone;
    }

    public void OnDrop(PointerEventData eventData)
    {                
        if (isArrived) { 
            Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
            if (d != null)
            {
                inDropZone = true;
            }
        }
    }
}
