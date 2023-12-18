using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Implementa a funcionalidade de receber objetos arrastados (drag and drop) em um componente UI.
/// </summary>
public class DND_DropReceiver : MonoBehaviour, IDropHandler
{
    // Posição na fila de arrasto.
    private int queuePosition;

    // Objeto ancorado ao componente de recepção de gotas.
    private GameObject anchoredGameObj;

    // Componente RectTransform do objeto de recepção de gotas.
    private RectTransform rectTransform;

    // Componente CanvasGroup para controlar a opacidade e a capacidade de bloquear cliques do objeto de recepção de gotas.
    private CanvasGroup canvasGroup;

    /// <summary>
    /// Inicialização chamada ao instanciar o objeto.
    /// </summary>
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    /// <summary>
    /// Chamado quando um objeto é solto no componente de recepção de comandos.
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        
        // Verifica se o ponteiro arrastado não é nulo e se não há um objeto ancorado neste componente.
        if (eventData.pointerDrag != null && anchoredGameObj == null)
        {
            // Cria uma duplicata do objeto arrastado e a ancora ao componente de recepção de comandos.
            anchoredGameObj = Instantiate(eventData.pointerDrag, this.gameObject.transform, true);
            anchoredGameObj.GetComponent<RectTransform>().position = rectTransform.position;
            anchoredGameObj.GetComponent<RectTransform>().rotation = rectTransform.rotation;
            anchoredGameObj.GetComponent<RectTransform>().localScale = rectTransform.localScale;
            anchoredGameObj.GetComponent<DND_DraggableObj>().Anchor(this.gameObject);
            
            // Adiciona à fila de ações uma posição após a posição atual na fila.
            GameObject.Find("PainelAcoes").GetComponent<PainelAcoes>().Add(queuePosition + 1);
            
            // Adiciona à fila de arrasto o objeto ancorado e a posição atual na fila.
            GameObject.Find("PainelAcoes").GetComponent<Fila>().Add(anchoredGameObj,queuePosition);
        }
    }

    /// <summary>
    /// Define a posição na fila de arrasto.
    /// </summary>
    public void SetPosition(int posicao)
    {
        queuePosition = posicao;
    }

    /// <summary>
    /// Obtém a posição na fila de arrasto.
    /// </summary>
    public int GetPosition()
    {
        return (queuePosition);
    }
}
