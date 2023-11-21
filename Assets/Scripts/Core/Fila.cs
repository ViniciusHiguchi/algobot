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

    public void Add(GameObject acao, int posicao)
    {
        fila[posicao] = acao;
    }

    public GameObject[] GetFila()
    {
        return (fila);
    }
}
