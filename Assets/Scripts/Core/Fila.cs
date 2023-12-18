using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fila : MonoBehaviour
{
    private GameObject[] fila;
    private void Awake()
    { 
        fila = new GameObject[GetComponent<PainelAcoes>().GetTamanho()];
    }

    public void ResetFila()
    {
        fila = new GameObject[GetComponent<PainelAcoes>().GetTamanho()];
    }

    //esta função é chamada quando o jogador arrasta um bloco para o placeholder, e adiciona uma referência do gameobject na lsita de comandos a serem executados
    public void Add(GameObject acao, int posicao)
    {
        fila[posicao] = acao;
    }

    public GameObject[] GetFila()
    {
        return (fila);
    }
}
