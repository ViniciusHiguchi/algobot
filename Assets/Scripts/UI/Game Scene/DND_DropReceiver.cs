using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DND_DropReceiver : MonoBehaviour, IDropHandler
{
    private int queuePosition;
    private GameObject anchoredGameObj;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void OnDrop(PointerEventData eventData)
    {

        Debug.Log("it's like magic");
        if (eventData.pointerDrag != null && anchoredGameObj == null)
        {
            anchoredGameObj = Instantiate(eventData.pointerDrag, this.gameObject.transform, true);
            anchoredGameObj.GetComponent<RectTransform>().position = rectTransform.position;
            anchoredGameObj.GetComponent<RectTransform>().rotation = rectTransform.rotation;
            anchoredGameObj.GetComponent<RectTransform>().localScale = rectTransform.localScale;
            anchoredGameObj.GetComponent<DND_DraggableObj>().Anchor(this.gameObject);
            GameObject.Find("PainelAcoes").GetComponent<PainelAcoes>().Add(queuePosition + 1);
            GameObject.Find("PainelAcoes").GetComponent<Fila>().Add(anchoredGameObj,queuePosition);
        }
    }

    public void SetPosition(int posicao)
    {
        queuePosition = posicao;
    }

    public int GetPosition()
    {
        return (queuePosition);
    }

    public void erase()
    {
        
    }
}
