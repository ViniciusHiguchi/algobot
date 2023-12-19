using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// essa função faz a transformação física no espaço do jogo para a ação de pular, usando a referência de onde a personagem começou e qual é o bloco para onde ela deve ir
/// para representar o pulo é encontrada uma parábola com raizes nas coordenadas da referencia A e B, e ajustado o ponto máximo para um valor predeterminado 
/// usando uma homotetia vertical: https://youtu.be/C4eqsZ7rr-8?t=244 isto para evitar que o personagem dê um pulo muito alto.
///
/// é chamada uma vez por FixedUpdate do scheduler
/// </summary>
public class pular : MonoBehaviour
{
    private bool started = false;
    private Vector3 direcao = new Vector3(0, 0, 0);
    private Vector2 posicao;
    private Vector2 forward;
    private GameObject[,] mapa;
    private float distancia;
    private float yDisplacement;
    private float alturaMax = 2;
    private float coeficienteHomotetia = 1;
    private float jumpTiming = 0.8f;
    private float elapsedTime = 0;

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
    
    private float ReceiveYDisplacementConstant()
    {
        return this.gameObject.GetComponent<CharacterController>().yDisplacementConstant;
    }
    
    private float NormalizeFunctionMax() //acha um coeficiente que aplica uma homotetia no gráfico da parábola
    {                                    //para que o ponto máximo seja exatamente em um valor y0 predefinido
        float coefHomotetia = 0;
        
        if (forward.x != 0) //x
        {
            float middlePoint = referenciaA.transform.position.x +
                                (referenciaB.transform.position.x - referenciaA.transform.position.x)/2;
            float maxY = (middlePoint - referenciaA.transform.position.x) * (middlePoint - referenciaB.transform.position.x)*-1;
            coefHomotetia = maxY / alturaMax;
        }
        
        if (forward.y != 0) //z
        {
            float middlePoint = referenciaA.transform.position.z +
                                (referenciaB.transform.position.z - referenciaA.transform.position.z)/2;
            float maxY = (middlePoint - referenciaA.transform.position.z) * (middlePoint - referenciaB.transform.position.z)*-1;
            coefHomotetia = maxY / alturaMax;
        }
        
        return coefHomotetia;
    }

    public void Run()
    {
        if (started == false) //inicializando variáveis relevantes
        {
            posicao = ReceiveCurrentPosition();
            forward = ReceiveCurrentForward();
            mapa = ReceiveMap();
            yDisplacement = ReceiveYDisplacementConstant();
            
            coeficienteHomotetia = 1;
            elapsedTime = 0;

            referenciaA = mapa[(int) posicao.x, (int) posicao.y];
            try
            {
                referenciaB = mapa[(int) (posicao.x + forward.x), (int) (posicao.y + forward.y)];
                if ((!referenciaA.GetComponent<ActionStatus>().IsJumpable && !referenciaB.GetComponent<ActionStatus>().IsJumpable))
                {
                    this.GetComponent<CharacterController>().termino(posicao,forward, 0);
                    return;
                }
                coeficienteHomotetia = NormalizeFunctionMax();
                GetComponent<AnimationHandler>().Jump(forward);
                direcao = (referenciaB.transform.position - referenciaA.transform.position).normalized;
                started = true;
            }
            catch
            {
                this.GetComponent<CharacterController>().termino(posicao,forward, 0);
                return;
            }
        }
        
        elapsedTime += Time.deltaTime;
        float incerteza = 0;
        
        if (forward.x != 0 && elapsedTime >= jumpTiming) //x
        {
            // this.gameObject.transform.Translate(direcao*Time.deltaTime, Space.World);
            // distancia = new Vector2(this.gameObject.transform.position.x - referenciaB.transform.position.x,
            //     this.gameObject.transform.position.z - referenciaB.transform.position.z).magnitude;
            float xPos = this.gameObject.transform.position.x + direcao.x*Time.deltaTime;
            float yPos = (((xPos - referenciaA.transform.position.x)*(xPos - referenciaB.transform.position.x)*-1)/coeficienteHomotetia) + yDisplacement + referenciaA.transform.position.y;
            incerteza = xPos * 4 + (referenciaA.transform.position.z + referenciaB.transform.position.z);
            this.gameObject.transform.position = new Vector3(xPos, yPos, this.gameObject.transform.position.z);
            
            distancia = new Vector2(this.gameObject.transform.position.x - referenciaB.transform.position.x,
                (this.gameObject.transform.position.y - yDisplacement) - referenciaB.transform.position.y).magnitude;
        }

        if (forward.y != 0 && elapsedTime >= jumpTiming) //z
        {
            // this.gameObject.transform.Translate(direcao*Time.deltaTime, Space.World);
            // distancia = new Vector2(this.gameObject.transform.position.x - referenciaB.transform.position.x,
            //     this.gameObject.transform.position.z - referenciaB.transform.position.z).magnitude; 
            float zPos = this.gameObject.transform.position.z + direcao.z*Time.deltaTime;
            
            //para achar a parábola é gerada uma função de segundo grau com as raízes em A e B da seguinte forma: (z - raizA) * (z - raizB)
            //isso garante que as raízes serão sempre raizA e RaizB, e depois é aplicada as funções e constantes para normalizar a curva
            float yPos = (((zPos - referenciaA.transform.position.z)*(zPos - referenciaB.transform.position.z)*-1)/coeficienteHomotetia) + yDisplacement + referenciaA.transform.position.y;
            incerteza = zPos * 4 + (referenciaA.transform.position.z + referenciaB.transform.position.z);
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, yPos, zPos);
            
            distancia = new Vector2(this.gameObject.transform.position.z - referenciaB.transform.position.z,
                (this.gameObject.transform.position.y - yDisplacement) - referenciaB.transform.position.y).magnitude;
        }
        
        distancia = new Vector3(this.gameObject.transform.position.x - referenciaB.transform.position.x,
            (this.gameObject.transform.position.y - yDisplacement) - referenciaB.transform.position.y,
            this.gameObject.transform.position.z - referenciaB.transform.position.z).magnitude;

        //print(distancia + " +-" + incerteza * Time.deltaTime);
        if (distancia <= incerteza*Time.deltaTime+0.2)
        {
            posicao += forward;
            this.gameObject.transform.position = new Vector3(referenciaB.transform.position.x, referenciaB.transform.position.y+yDisplacement, referenciaB.transform.position.z);
            started = false;
            GetComponent<AnimationHandler>().Land(forward);
            this.GetComponent<CharacterController>().termino(posicao,forward, 1);
        }
    }
    
    public void Stop()
    {
        started = false;
    }
}
