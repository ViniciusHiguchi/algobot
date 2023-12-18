using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// este código recebe uma métrica bem arbitrária de código efetivo e não efetivo, decidido pelo scheduler que roda o código que vem do painelAções da UI
/// a partir de quantas ações foram feitas x quantas ações fora mefetivas (progridem no jogo) ele calcula e atualiza a UI que mostra a eficiência da solução.
///
/// quando entreguei o projeto houve uma confusão tremenda sobre como essa métrica é calculada.
///
///     Se vocês quiserem manter a métrica:
/// 
///     Pela repercussão que eu presenciei disto aqui, eu recomendo que seja feito um bom estudo
///     nesse sentido em artigos de pedagogia; TICs (Tecnologias da Informação e da Comunicação);
///     gamificação e jogos sérios, sobre motivação intrínseca e extrínseca. Assim vocês podem
///     modelar meios mais justos, transparentes e integrados no jogo de como dar feedback ao
///     jogador (1).
///         Hoje em dia, depois de algumas epopéias em matérias de liceniatura, eu não utilizaria
///     um sistema de pontuação tão explícito e arbitrário para calcular o desempenho do jogador.
///     O objetivo final desse sistema é manter o jogador no estado de flow, tentando fazer com
///     que a dificuldade se ajuste ao jogador.
///         Pontuação em jogos sérios voltados pra um público alvo tão jovem, pode ser bem
///     detrimental para o autoconceito que os estudantes tem sobre suas habilidades. Reforçando
///     e incentivando quem já tem facilidade (que se o jogo for interessante esse aluno vai jogar
///     de qualquer forma), e fazendo com que quem tem mais dificuldade com pensamento computacional,
///     eventualmente se frustre com o baixo desempenho e tenha uma tendencia a possivelmente abandonar
///     o jogo.
///
///     As afirmações desse segundo parágrafo tratam-se de uma hipótese, num paper de IC ou PDPD
///     é preciso de evidência para suportar isso. Mas de qualquer forma, trata-se de um jogo e não
///     uma avaliação, então repensar este sistema é importante.
///
///     (1) - Eu recomendo que se vocês quiserem fazer essa pesquisa, falem com o professor André,
///     para que vocês possam ter um direcionamento qualificado sobre o que e onde pesquisar nesse sentido.
/// </summary>
public class Eficiencia : MonoBehaviour
{
    private Slider eficiencia;
    private float total=0;
    private float efetivo=0;
    
    // Start is called before the first frame update
    void Start()
    {
        eficiencia = this.gameObject.GetComponent<Slider>();
        eficiencia.value = 1;
    }

    public void ResetEficiencia()
    {
        eficiencia.value = 1;
        total=0;
        efetivo=0;
    }

    public void CalculoEficiencia(float statusAcao) //status ação assume valor 1 ou 0, sendo que 1 conta como uma ação efetiva e 0 como não efetiva
    {
        total += 1;
        efetivo += statusAcao;
        print("efetivo: "+ efetivo);
        print("total: "+ total);
        print("ef: "+ (efetivo/total));
        UpdateEficiencia((efetivo/total));
    }

    void UpdateEficiencia(float valor)
    {
        eficiencia.value = valor;
    }

    public float GetEficiencia()
    {
        return eficiencia.value;
    }
}
