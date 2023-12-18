using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contém os dados do respectivo botão de seleção de dificuldade.
/// </summary>
public class SelectDifficultyButton : MonoBehaviour
{
    // Dificuldade associada a este botão.
    public int difficulty;

    // Referência ao painel de exibição de níveis.
    public GameObject painelNiveis;

    /// <summary>
    /// Função chamada quando o botão de seleção de dificuldade é clicado.
    /// Chama a função DisplayDifficulty no PainelNiveisDisplay associado, passando a dificuldade e a cor do botão.
    /// </summary>
    public void SelectDifficulty()
    {
        painelNiveis.GetComponent<PainelNiveisDisplay>().DisplayDifficulty(difficulty, gameObject.GetComponent<Image>().color);
    }
}
