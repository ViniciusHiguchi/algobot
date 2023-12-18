using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Implementa a funcionalidade de arrastar e soltar (drag and drop) para objetos na interface do usuário.
/// </summary>
public class DND_DraggableObj : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    // Referência ao componente Canvas.
    [SerializeField] private Canvas canvas;

    // Componente RectTransform do objeto arrastável.
    private RectTransform rectTransform;

    // Componente CanvasGroup para controlar a opacidade e a capacidade de bloquear cliques do objeto arrastável.
    private CanvasGroup canvasGroup;

    // Objeto duplicado que é criado durante o arraste.
    private GameObject gameObjDuplicate;

    // Objeto marcador de posição ancorado.
    private GameObject anchoredPlaceholder;

    // Flag para indicar se o objeto está ancorado.
    private bool isAnchored;

    /// <summary>
    /// Inicialização chamada ao acordar o objeto.
    /// </summary>
    private void Awake()
    {
        isAnchored = false;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    /*TODO -(10/05)-implement dragging feature--;  -10/05-drop receiver implement--   task added at (07/05/21)
      TODO drag and drop queue handler   task added at (07/05/21)

     */
    
    /// <summary>
    /// Chamado no início do arraste.
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin Drag");
        if (isAnchored == false)
        {
            // Cria uma duplicata do objeto para ser arrastada.
            gameObjDuplicate = GameObject.Instantiate(this.gameObject);
            gameObjDuplicate.transform.SetParent(GameObject.Find("PainelAcoes").transform);
            gameObjDuplicate.GetComponent<RectTransform>().position = rectTransform.position;
            gameObjDuplicate.GetComponent<RectTransform>().rotation = rectTransform.rotation;
            gameObjDuplicate.GetComponent<RectTransform>().localScale = rectTransform.localScale;
        }
        else
        {
            // Adiciona um marcador de posição nulo à fila de arrasto.
            GameObject.Find("PainelAcoes").GetComponent<Fila>().Add(null,anchoredPlaceholder.GetComponent<DND_DropReceiver>().GetPosition());
        }
        
        // Ajusta a opacidade e bloqueia cliques no objeto arrastável original.
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// Chamado durante o arraste.
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("on drag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    
    /// <summary>
    /// Chamado no final do arraste.
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("on end drag");
        
        // Restaura a opacidade e a capacidade de bloquear cliques no objeto arrastável.
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        
        // Define o nome da duplicata para o nome original do objeto arrastável.
        if(isAnchored == false)
            gameObjDuplicate.name = this.gameObject.name;
        
        // Destrói o objeto arrastável original.
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Ancora o objeto a um marcador de posição.
    /// </summary>
    public void Anchor(GameObject ph)
    {
        Debug.Log("anchor");
        
        // Restaura a opacidade e a capacidade de bloquear cliques no objeto arrastável.
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        
        // Marca o objeto como ancorado e armazena o marcador de posição ancorado.
        isAnchored = true;
        anchoredPlaceholder = ph;
    }
    
    /// <summary>
    /// Chamado quando o ponteiro é pressionado.
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointer down");
    }
}
