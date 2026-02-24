using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject mapa;
    public GameObject eficiencia;
    public float yDisplacementConstant = 1.4f; //constante pra mover o personagem y unidades pra cima, para que a personagem apareça de pé no bloco
    
    public GameObject[,] matrizReferencia;
    private int[] inicioReferencia;
    
    
    public Vector2 forwardDirection; //y deste vetor é o eixo z do editor, x o eixo x, ambos na mesma direção, essa variável
    //guarda a direção que a personagem está olhando
    public Vector2 posicao; //essa variável guarda a presente posição da personagem na matriz do jogo, iniciada na posição de início do mapa
    
    
    // Start is called before the first frame update
    /// <summary>
    /// inicializa o personagem no primeiro frame. Acho que daria só para chamar o ResetChar aqui
    /// </summary>
    void Start()
    {
        forwardDirection = new Vector2(0, -1);
        matrizReferencia = mapa.GetComponent<Intepretador>().GetMatriz();
        inicioReferencia = mapa.GetComponent<Intepretador>().GetInicio();
        this.gameObject.transform.position = new Vector3(
            matrizReferencia[inicioReferencia[0], inicioReferencia[1]].transform.position.x,
            matrizReferencia[inicioReferencia[0], inicioReferencia[1]].transform.position.y + yDisplacementConstant,
            matrizReferencia[inicioReferencia[0], inicioReferencia[1]].transform.position.z);
        posicao = new Vector2(inicioReferencia[0], inicioReferencia[1]);
        
        this.gameObject.GetComponent<AnimationHandler>().Stand(forwardDirection);
    }

    /// <summary>
    /// faz o reset do personagem para uma nova tentativa ou novo mapa
    /// </summary>
    public void ResetChar()
    {
        forwardDirection = new Vector2(-1, 0); 
        matrizReferencia = mapa.GetComponent<Intepretador>().GetMatriz();
        inicioReferencia = mapa.GetComponent<Intepretador>().GetInicio();
        this.gameObject.transform.position = new Vector3(
            matrizReferencia[inicioReferencia[0], inicioReferencia[1]].transform.position.x,
            matrizReferencia[inicioReferencia[0], inicioReferencia[1]].transform.position.y + yDisplacementConstant,
            matrizReferencia[inicioReferencia[0], inicioReferencia[1]].transform.position.z);
        posicao = new Vector2(inicioReferencia[0], inicioReferencia[1]);
        
        this.gameObject.GetComponent<AnimationHandler>().Stand(forwardDirection);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// chamado ao término de cada ação, atualiza as referências de posição do personagem, direção, e se chegou ao bloco final
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="forward"></param>
    /// <param name="status"></param>
    public void termino(Vector2 pos, Vector2 forward, float status)
    {
        if (status == 1)
        {
            posicao = pos;
            forwardDirection = forward;
            eficiencia.GetComponent<Eficiencia>().CalculoEficiencia(status);
            mapa.GetComponent<Scheduler>().Termino();
        }
        else
        {
            eficiencia.GetComponent<Eficiencia>().CalculoEficiencia(status);
            mapa.GetComponent<Scheduler>().Termino();
        }
    }
}
