using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contém os dados do respectivo botão
/// </summary>
public class SelectDifficultyButton : MonoBehaviour
{
    public int difficulty;

    public GameObject painelNiveis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SelectDifficulty()
    {
        painelNiveis.GetComponent<PainelNiveisDisplay>().DisplayDifficulty(difficulty, gameObject.GetComponent<Image>().color);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
