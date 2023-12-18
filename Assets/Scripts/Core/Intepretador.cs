using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
/// <summary>
/// esta classe tem a função de interagir com o mapa 'físico' mostrado na tela do jogo
///
/// ela também guarda referências importantes sobre os objetos do mapa (matrizReferencia)
/// </summary>
public class Intepretador : MonoBehaviour
{
    //as funcionalidades dessa classe podem ser testadas ativando a função Scheduler.StopQueue(), ativando com a 
    //barra de espaco ou com a execução de Scheduler.OnRunQueue() pressinando 'c'
    
    //é necessário verificar se essas binds ainda estão funcionando

    public static string currentMap;

    public GameObject lvlEnding;
    
    //declarando gameobjects 'tiles' utilizadas no mapa
    public GameObject prototypeTile;
    
    //o modelo de como a matriz será iterada foi baseado na matriz disponível no artigo
    //Design de Jogos Eletronicos de Quebra-Cabeças usando Geraçao Procedural
    //publicado na sbgames: https://www.sbgames.org/sbgames2019/files/papers/ArtesDesignShort/198445.pdf
    //mock data para testes no desenvolvimento. o mapa será pego dos StreamingAssets, e decidido através do sistema
    //que avalia o quão bem o jogador foi no mapa anterior. Como discutido anteriormente (Anteriormente aonde vini do passado??? Talvez eu esteja falando do meu projeto de PDPD).
   
    //esse diagrama é para facilitar a construção de níveis para teste, e ter uma referência visual da onde está cada coisa
    // int[,] arrayMap = new int[4, 4] {
    //     { 1000, 0100 ,0000 ,0000 }, //--> z 
    //     { 0000, 0010, 0010, 0000 },
    //     { 0000, 0000, 0100, 0010 },
    //     { 0000, 0000, 0000, 0001 } }; //canto mais distante da camera
    //    |
    //    v
    //    x
    
    private int[,] arrayMap = new int[4, 4];
    
    private int[] inicioReferencia = new int[2] {0,0};
    
    //usada no método Instanciar() há uma descrição mais detalhada da utilidade lá.
    private GameObject[,] matrizReferencia = new GameObject[4,4];
    
    /* arrayCount é usado no método Instanciar(), serve para acessar e organizar a matriz arrayReferencia
       serve também para calcular as coordenadas onde os blocos irão aparecer */
    private int[] arrayCount = new int[2] {0,0};

    //inicia a construção do mapa e o controlador do que acontece quando o código termina de ser executado
    private void Awake()
    {
        print(LoadGameScene.levelToLoad);
        Iterador(LoadGameScene.levelToLoad);
        lvlEnding.GetComponent<LevelEndingRouter>().SetLevelControlVariables();
    }

    //faz o reset do mapa para construir outro
    public void ResetMapa()
    {
        foreach (GameObject VARIABLE in matrizReferencia)
        {
            Destroy(VARIABLE);
        }
    }

    //itera por cada objeto da respresentação em str do mapa, e chama a função que instancia o bloco na posição devida
    public void Iterador(string mapFile)
    {
        int [,] mapData = GetComponent<LoadLevel>().LoadLevelFromFile(mapFile); 
        arrayCount = new int[2] {0,0};
        foreach (var tile in mapData)
        {
            print(tile);
            Instanciar(tile);
        }

        currentMap = mapFile;
    }

    void Instanciar(int tile)
    {
        //este vector3 define a onde o bloco vai parar no mapa do jogo
        Vector3 vectorTile = new Vector3(1.5f * arrayCount[0],0, 1.5f * arrayCount[1]);
        
        /* matrizReferencia guarda a referencia para cada gameobject da cena do jogo, é importante
        para acessar propriedades especificas de cada tile, como: se é possível utilizar uma ação de pular
        para ir para o próximo, ou se é possível fazer algo na tile em que o personagem está em cima,
        como o acender do lightbot, se isto fosse implementado. Também é uma boa idéia utilizar como referencia de posição
        para saber qual vetor x,y,z a personagem tem como destino, já que é essencial que o jogo represente precisamente
        a posição da personagem */
        
        // 0001 -> início
        // 0010 -> Bloco regular
        // 0100 -> Bloco deslocado 1 unidade para cima
        // 1000 -> Bloco final
        // com 4 'bits' vocês podem representar 16 blocos diferentes,
        // como não está realmente em binário da pra ter até 1000 se for representado em decimal (0, 1, 2, 3, ...)
        
        //colocar o if que dita qual bloco vai ser instanciado aqui
        
        //cada bloco dentro do if faz todo o trabalho de instanciar os blocos,
        //colocar as propriedades que eles tem no actionStatus, e colocar os blocos no lugar certo do espaço do jogo
        //sempre salvando a referência na matriz
        //assim, a partir de um acesso na variável matrizReferencia[x,y]
        //pode ser acessada a referência da tile da linha x e coluna y
        if (tile == 0001)
        {
            matrizReferencia[arrayCount[0], arrayCount[1]] =
                Instantiate(prototypeTile, vectorTile, Quaternion.Euler(0, 0, 0));
            matrizReferencia[arrayCount[0], arrayCount[1]].name = "" + tile;
            inicioReferencia[0] = arrayCount[0];
            inicioReferencia[1] = arrayCount[1];
            matrizReferencia[arrayCount[0], arrayCount[1]].transform.localScale = new Vector3(1.5f, 1, 1.5f);
            matrizReferencia[arrayCount[0], arrayCount[1]].GetComponent<Renderer>().material.color = Color.cyan;
        }
        if (tile == 0010)
        {
            matrizReferencia[arrayCount[0], arrayCount[1]] =
                Instantiate(prototypeTile, vectorTile, Quaternion.Euler(0, 0, 0));
            matrizReferencia[arrayCount[0], arrayCount[1]].transform.localScale = new Vector3(1.5f, 1, 1.5f);
            matrizReferencia[arrayCount[0], arrayCount[1]].name = tile+" "+arrayCount[0]+", "+arrayCount[1];
            matrizReferencia[arrayCount[0], arrayCount[1]].GetComponent<ActionStatus>().IsWalkable = true;
        }
        if (tile == 0100)
        {
            matrizReferencia[arrayCount[0], arrayCount[1]] =
                Instantiate(prototypeTile, vectorTile + new Vector3(0,1,0), Quaternion.Euler(0, 0, 0));
            matrizReferencia[arrayCount[0], arrayCount[1]].name = tile+" "+arrayCount[0]+", "+arrayCount[1];
            matrizReferencia[arrayCount[0], arrayCount[1]].GetComponent<ActionStatus>().IsJumpable = true;
        }
        if (tile == 1000)
        {
            matrizReferencia[arrayCount[0], arrayCount[1]] =
                Instantiate(prototypeTile, vectorTile, Quaternion.Euler(0, 0, 0));
            matrizReferencia[arrayCount[0], arrayCount[1]].name = tile+" "+arrayCount[0]+", "+arrayCount[1];
            matrizReferencia[arrayCount[0], arrayCount[1]].GetComponent<ActionStatus>().IsFinish = true;
            matrizReferencia[arrayCount[0], arrayCount[1]].transform.localScale = new Vector3(1.5f, 1, 1.5f);
            matrizReferencia[arrayCount[0], arrayCount[1]].GetComponent<Renderer>().material.color = Color.green;
        }

        
        //controle da variável de referencia de linhas e colunas na matriz.
        //é útil realizar o controle dessa variável aqui, pois ele itera existindo ou não* um comando para instanciar acima
        //assim evitamos problemas de referência, e como todos os movimento se baseiam nisso
        //é importante que a matrizReferencia guarde exatamente o que existe ou não existe naquela posição
        
        //* blocos 0000 não serão instanciados e terão referencias nulas na matriz
        if (arrayCount[1] < 3)
            arrayCount[1] += 1;
        else
        {
            arrayCount[0] += 1;
            arrayCount[1] = 0;
        }
    }

    public GameObject[,] GetMatriz()
    {
        return matrizReferencia;
    }

    public int[] GetInicio()
    {
        return inicioReferencia;
    }
}
