using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DND_DraggableObj : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GameObject gameObjDuplicate;
    private GameObject anchoredPlaceholder;

    private bool isAnchored;

    private void Awake()
    {
        isAnchored = false;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    /*TODO -(10/05)-implement dragging feature--;  -10/05-drop receiver implement--   task added at (07/05/21)
      TODO drag and drop queue handler   task added at (07/05/21)

     */
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin Drag");
        if (isAnchored == false)
        {
            gameObjDuplicate = GameObject.Instantiate(this.gameObject);
            gameObjDuplicate.transform.SetParent(GameObject.Find("PainelAcoes").transform);
            gameObjDuplicate.GetComponent<RectTransform>().position = rectTransform.position;
            gameObjDuplicate.GetComponent<RectTransform>().rotation = rectTransform.rotation;
            gameObjDuplicate.GetComponent<RectTransform>().localScale = rectTransform.localScale;
        }
        else
        {
            GameObject.Find("PainelAcoes").GetComponent<Fila>().Add(null,anchoredPlaceholder.GetComponent<DND_DropReceiver>().GetPosition());
        }
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("on drag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("on end drag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if(isAnchored == false)
            gameObjDuplicate.name = this.gameObject.name;
        Destroy(this.gameObject);
    }

    public void Anchor(GameObject ph)
    {
        Debug.Log("anchor");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        isAnchored = true;
        anchoredPlaceholder = ph;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointer down");
    }
}
