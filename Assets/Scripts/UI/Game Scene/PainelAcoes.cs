using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


//TODO a ideia aqui é fazer um controlador inteligente da UI do enfileirador,
//como isso demanda uma lógica um pouco complicada vou deixar o array montado no editor por enquanto
//caso o tempo para construir essa solução seja muito expansivo vou deixar a matriz montada assim mesmo
//isso porque não há muito tempo para refinar essa parte por enquanto, quando o jogo estiver mais consolidado
//pode ser avaliado se é viável continuar essa idéia, já que por enquando isto não é o mais crucial

//um jeito que pensei que isto poderia ser implementado é construir a grade previamente no editor e fazer ficar visível somente
//o próximo objeto, assim seria economizado algum tempo na construção da lógica, apesar de ser uma solução ruim.
//talvez assim fique até mais fácil para adicionar um bloco de código entre outros 2

/// <summary>
/// Responsável por colocar as âncoras dos objetos Drag and Drop (DND)
///
/// neste caso, a solução usa algumas âncoras para calcular o espaço do painel onde é construído o pseudocódigo do jogo
///
/// assim, sempre que o jogador adicionar um objeto na última âncora da matriz, surge uma nova na próxima posição.
/// isto facilitia o jogabilidade porque o jogador instintivamente sabe qual a ordem que as ações devem ser colocadas.
///
/// uma limitação deste código é que não há uma função para fazer scroll, isto limita o código a 16 blocos,
/// se vocês quiserem adicionar mapas maiores que o 4x4 original, vai ser necessário implementar um scroll ou abas (é preferível fazer scroll, por conta de facilitar a gameplay, mas fazer com abas é mais simples)
/// e fazer com que este código conforme a adição de novas ancoras ao novo paradigma
/// </summary>
public class PainelAcoes : MonoBehaviour
{
    //ancoras do sistema de drag and drop
    [SerializeField] private GameObject placeholder;
    [SerializeField] private GameObject inBetween;

    //referencias para geração da grade responsiva
    [SerializeField] private GameObject reference_TL;
    [SerializeField] private GameObject reference_TR;
    [SerializeField] private GameObject reference_BL;
    [SerializeField] private GameObject reference_BR;

    //mudando essa variavel altera-se quantos elementos possui cada linha da grade de ações
    private static int elemLinha = 3;
    private static int quantLinhas = 6;

    //devnote: mudar para gobj
    private GameObject[,] arrayMap = new GameObject[elemLinha, quantLinhas];
    private GameObject[] ordemAcao = new GameObject[elemLinha*quantLinhas];
    private GameObject[] PlaceholderList = new GameObject[elemLinha*quantLinhas];

    //declarando variaveis futuras
    public float width;
    public float height;

    void Start()
    {
        //calculando tamanho do retangulo baseado nas referencias
        width = reference_TR.GetComponent<RectTransform>().anchoredPosition.x
                - reference_TL.GetComponent<RectTransform>().anchoredPosition.x;
        //Debug.Log(reference_TL.GetComponent<RectTransform>().rect.y +", "+ reference_TL.GetComponent<RectTransform>().anchoredPosition);
        height = reference_TL.GetComponent<RectTransform>().anchoredPosition.y
                 - reference_BL.GetComponent<RectTransform>().anchoredPosition.y;
        //Instantiate(placeholder, GameObject.Find("PainelAcoes").transform, false)
        //dev test
        Add(0);
    }

    public void ResetPainel()
    {
        foreach (GameObject VARIABLE in PlaceholderList)
        {
            Destroy(VARIABLE);
        }
        this.gameObject.GetComponent<Fila>().ResetFila();
        
        //calculando tamanho do retangulo baseado nas referencias
        width = reference_TR.GetComponent<RectTransform>().anchoredPosition.x
                - reference_TL.GetComponent<RectTransform>().anchoredPosition.x;
        //Debug.Log(reference_TL.GetComponent<RectTransform>().rect.y +", "+ reference_TL.GetComponent<RectTransform>().anchoredPosition);
        height = reference_TL.GetComponent<RectTransform>().anchoredPosition.y
                 - reference_BL.GetComponent<RectTransform>().anchoredPosition.y;
        //Instantiate(placeholder, GameObject.Find("PainelAcoes").transform, false)
        
        arrayMap = new GameObject[elemLinha, quantLinhas];
        ordemAcao = new GameObject[elemLinha*quantLinhas];
        PlaceholderList = new GameObject[elemLinha*quantLinhas];
        
        //dev test
        Add(0);
    }

    public void Add(int posicao) //int posicao no array, se eu quero adicionar o 4o elemento (primeiro da segunda linha), este parâmetro viria como 3
    {                                                                                           //o o o
                                                                                             //x <--
        if (posicao >= elemLinha*quantLinhas)
            return;
        if (PlaceholderList[posicao] != null)
            return;
            
        //obs: espero que vocÊs tenham feito BM, eu estava louco de matemática quando fiz esse código. Não é muito complicado, mas a forma que eu escrevi foi meio maluca.
        
        //dado par ordenado (i, j)
        //cada linha tem 3 objetos, e existem n linhas
        //para achar linha j, usando a posicao da acao, considerando uma ordem que parte de 0 ao infinito
        //pega se o menor inteiro (Math.floor()) da posicao dividida pela quantidade de elementos na linha (3) (posição / 3)
        //para achar a coluna, pega se a posição - linha * 3(elementos na linha)
        float divisao = posicao / elemLinha;
        int linha = Convert.ToInt32(Math.Floor(divisao));
        int coluna = posicao - linha * elemLinha;
        float placeholderTamanho = placeholder.GetComponent<RectTransform>().rect.width;
        Vector2 origem = reference_TL.GetComponent<RectTransform>().anchoredPosition;
        //centralizando em x
        origem.x = origem.x + (width - (placeholderTamanho + placeholderTamanho * 0.2f) * 3)/2;

        Vector2 posicaoAncora = new Vector2(origem.x + coluna * (placeholderTamanho+placeholderTamanho*0.2f) + (placeholderTamanho+placeholderTamanho*0.2f)/2,
                                            origem.y - linha * (placeholderTamanho+placeholderTamanho*0.2f) - (placeholderTamanho+placeholderTamanho*0.2f)/2);

        GameObject addedPh = Instantiate(placeholder, GameObject.Find("PainelAcoes").transform);
        addedPh.GetComponent<RectTransform>().anchoredPosition = posicaoAncora;
        addedPh.GetComponent<DND_DropReceiver>().SetPosition(posicao);
        PlaceholderList[posicao] = addedPh;

        // print(linha);
        // if (arrayMap[posicao - linha * elemLinha, linha] == null)
        // {
        //     arrayMap[posicao - linha*elemLinha, linha] = acao;
        //     if(arrayMap[(posicao - linha * elemLinha), linha] == null)
        // }
        // else
        // {
        //     
        // }
        // if (ordemAcao[posicao] == null)
        // {
        //     ordemAcao[posicao] = acao;
        //     if (ordemAcao[posicao + 1] == null)
        //     {
        //         //instanciar um objeto placeholder na próxima posição
        //     }
        //         
        // }

        // print(arrayMap[2,3].transform.name);
    }

    public int GetTamanho()
    {
        //retorna o tamanho que o array deve ter para suportar a UI com esses parâmetros de coluna vs linhas
        return (elemLinha * quantLinhas);
    }
    
}
