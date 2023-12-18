using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// usado para que o componente de UI saiba quais são as ações colcoadas na fila
/// </summary>
public class DND_TipoAcao : MonoBehaviour
{
    //'andar', 'virarD' (direita), 'virarE' (esquerda), 'pular'
    public string tipo;
}