using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
public class Scheduler : MonoBehaviour
{
    private GameObject[] fila;

    private List<String> filaStr;
    
    public GameObject PlayButton;
    public GameObject PauseButton;
    public GameObject character;
    public GameObject endingScreen;
    
    
    private float cooldown = 0; 
    private float actionCooldown = 0.5f; // cooldown between actions
    public float currentAction = 0;
    public bool activeAction = false;
    public int index = 0;
    public bool isRunning = false;
    //int[] actions = new int[] { 1, 2, 3, 4, 5,6,7,1000 };

    // Start is called before the first frame update
    void Start()
    {
        filaStr = new List<string>();
        Debug.Log("Start");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*a ideia é organizar uma fila e rodar os "comandos", por enquanto a fila é a variável actions.
         No futuro pegaremos a fila de comandos da interface onde a ordem de execução é construída
         */
        
        //-> jogador define acoes a realizar na interface
        //-> fila.cs pega as acoes e essa classe pega de fila.cs
        //-> aqui roda a ação de acordo com o nome, no momento em que começa a rodar chamamos <acao>.cs.run() dentro do update, assim;
        // o update dessa classe é o "roteador" de um pseudo-update em <acao>.cs 
        
        //-> quando <acao>.run() terminar a tarefa essa classe chama scheduler.termino() e passa pra próxima
        // if (isRunning)
        // {
        //     if (!activeAction)
        //     {
        //         SetCooldown(actionCooldown);
        //     }
        //     else if(index == fila.Length)
        //     {
        //         StopQueue();
        //     }
        //     else
        //     {
        //         Cooldown();
        //         if(fila[index] != null)
        //             print("executando ação: " + fila[index].GetComponent<DND_TipoAcao>().tipo + ": "+index);
        //         else
        //         {
        //             print("nada para executar");
        //             Skip();
        //         }
        //     }
        // }
        
        if (isRunning)
        {
            if (!activeAction)
            {
                Inicio();
            }
            if(index == filaStr.Count)
            {
                StopQueue(false);
                return;
            }
            if(activeAction)
            {
                if (filaStr[index] != null)
                {
                    print("executando ação: " + filaStr[index] + ": " + index);
                    if (filaStr[index] == "andar")
                    {
                        character.GetComponent<andar>().Run();
                    }

                    else if (filaStr[index] == "pular")
                        character.GetComponent<pular>().Run();
                    else if (filaStr[index] == "virarD")
                    {
                        character.GetComponent<virar>().Run(1); //direita
                    }
                    else if (filaStr[index] == "virarE")
                    {
                        character.GetComponent<virar>().Run(-1); //esquerda
                    }

                    //sim, eu concordo, essa sequencia de if's é a implementação mais
                    //preguiçosa desse projeto inteiro, mas eu tenho 2 semanas pra entregar o protótipo.
                    
                    //atualização (09/09/21) sobre o comentário acima, eu ainda era ingênuo.*
                    //*veja o animation handler
                }
                else
                {
                    print("nada para executar, ainda");
                    Skip();
                }
            }
        }
    }

    public void Termino()
    {
        index += 1;
        activeAction = false;
    }

    void Inicio()
    {
        cooldown += Time.deltaTime;
        if (cooldown >= actionCooldown)
        {
            activeAction = true;
            cooldown = 0;
        }
    }

    // void SetCooldown(float time)
    // {
    //     currentAction = time;
    //     activeAction = true;
    // }
    //
    // void Cooldown()
    // {
    //     currentAction -= Time.deltaTime;
    //     if (currentAction <= 0)
    //     {
    //         index += 1;
    //         activeAction = false;
    //     }
    // }

    void Skip()
    {
        index += 1;
        activeAction = false;
        cooldown = actionCooldown;
        Debug.Log("skipped");
    }
    
    public void OnRunQueue()
    {
        if (!isRunning)
            RunQueue();
    }

    void RunQueue()
    {
        fila = GameObject.Find("PainelAcoes").GetComponent<Fila>().GetFila();
        if (fila == null)
        {
            Debug.Log("sem acao");
            StopQueue(true);
        }
        PlayButton.SetActive(false);
        PauseButton.SetActive(true);
        index = 0;
        filaStr = new List<string>();
        //get data from queue built on UI
        isRunning = true;
        Debug.Log("começo da execução");
        foreach (var VARIABLE in fila)
        {
            try
            {
                filaStr.Add(VARIABLE.GetComponent<DND_TipoAcao>().tipo);
            }
            catch
            {
                print("pass");
            }
        }
    }

    public void OnStopQueue()
    {
        //evento ativado pelo input, pode ser usado para diferenciar entre uma parada forçada
        //e uma parada normal da execução.
        endingScreen.GetComponent<LevelEndingRouter>().AddTentativa();
        if (isRunning)
            StopQueue(true);
        //esse método pode iniciar o reset 
    }
    
    void StopQueue(bool playerReset)
    {
        if (playerReset)
        {
            character.GetComponent<pular>().Stop();
            character.GetComponent<andar>().Stop();
            endingScreen.GetComponent<LevelEndingRouter>().ResetGameScene();
        }
        index = 0;
        isRunning = false;
        activeAction = false;
        Debug.Log("término da execução");
        PlayButton.SetActive(true);
        PauseButton.SetActive(false);
        fila = null;
        filaStr = new List<string>();
        //se chegou no objetivo mostrar tela de proximo nível
        //se falhou ao chegar no objetivo mostrar tela de tentar novamente
        //GetComponent<Intepretador>().Iterador();
        //esse método pode iniciar o avanço para uma nova fase
        if (playerReset == false)
        {
            endingScreen.GetComponent<LevelEndingRouter>().StartEndingOverlay(character.GetComponent<CharacterController>().matrizReferencia[(int)character.GetComponent<CharacterController>().posicao.x,(int) character.GetComponent<CharacterController>().posicao.y].GetComponent<ActionStatus>().IsFinish);
        }
    }
    
}
