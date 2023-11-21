using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class andar : MonoBehaviour
{
    private bool started = false;
    private Vector3 direcao = new Vector3(0, 0, 0);
    private Vector2 posicao;
    private Vector2 forward;
    private GameObject[,] mapa;
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
    
    private GameObject[,] ReceiveMap()
    {
        return this.gameObject.GetComponent<CharacterController>().matrizReferencia;
    }

    public void Run()
    {
        if (started == false) //inicializando variáveis relevantes
        {
            posicao = ReceiveCurrentPosition();
            forward = ReceiveCurrentForward();
            mapa = ReceiveMap();

            referenciaA = mapa[(int) posicao.x, (int) posicao.y];
            try
            {
                referenciaB = mapa[(int) (posicao.x + forward.x), (int) (posicao.y + forward.y)];
                if ((referenciaA.GetComponent<ActionStatus>().IsJumpable || referenciaB.GetComponent<ActionStatus>().IsJumpable))
                {
                    this.GetComponent<CharacterController>().termino(posicao,forward, 0);
                    return;
                }
                GetComponent<AnimationHandler>().Walk(forward);
                direcao = (referenciaB.transform.position - referenciaA.transform.position).normalized;
                started = true;
            }
            catch
            {
                this.GetComponent<CharacterController>().termino(posicao,forward, 0);
                return;
            }
            
        }
        
        this.gameObject.transform.Translate(direcao*Time.deltaTime, Space.World);
        distancia = new Vector2(this.gameObject.transform.position.x - referenciaB.transform.position.x,
            this.gameObject.transform.position.z - referenciaB.transform.position.z).magnitude;

        if (distancia < 0.05)
        {
            posicao += forward;
            this.gameObject.transform.position = new Vector3(referenciaB.transform.position.x,
                referenciaB.transform.position.y + 1.4f, referenciaB.transform.position.z);
            started = false;
            GetComponent<AnimationHandler>().Stand(forward);
            this.GetComponent<CharacterController>().termino(posicao, forward, 1);
        }
    }

    public void Stop()
    {
        started = false;
    }
}
