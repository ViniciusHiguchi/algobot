using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// faz a alteração do vetor que representa o forward (direção que está olhando) do personagem, com base em se o personagem virou para a esquerda ou para a direita
/// </summary>
public class virar : MonoBehaviour
{
    // private bool started = false;
    private Vector3 direcao = new Vector3(0, 0, 0);
    private Vector2 posicao;
    private Vector2 forward;
    // private GameObject[,] mapa;
    private float distancia;

    private GameObject referenciaA;
    private GameObject referenciaB;

    private Vector2 ReceiveCurrentPosition()
    {
        return this.gameObject.GetComponent<CharacterController>().posicao;
    }
    
    private Vector2 ReceiveCurrentForward()
    {
        return this.gameObject.GetComponent<CharacterController>().forwardDirection;
    }
    
    // private GameObject[,] ReceiveMap()
    // {
    //     return this.gameObject.GetComponent<CharacterController>().matrizReferencia;
    // }

    public void Run(float direcaoRotacao) //1 é para direita -1 esquerda
    {
        posicao = ReceiveCurrentPosition();
        forward = ReceiveCurrentForward();
        forward = swap(direcaoRotacao);
        //SetEscalaX(forward);
        GetComponent<AnimationHandler>().Stand(forward);
        this.GetComponent<CharacterController>().termino(posicao,forward, 1);
    }

    private Vector2 swap(float direcaoRotacao)
    {
        if (forward.x == 0)
        {
            forward = new Vector2(direcaoRotacao * 1 * forward.y, 0);
        }
        else
        {
            forward = new Vector2(0,direcaoRotacao * -1 * forward.x);
        }

        return forward;
    }
}
