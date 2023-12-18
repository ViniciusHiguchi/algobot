using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Contém os dados do respectivo botão de seleção de nível e controla seus atributos.
/// </summary>
public class SelectLevelButton : MonoBehaviour
{

    public int level;
    public int difficulty;

    public string levelString;
    
    public Color color;


    public GameObject levelEficienciaTmp;
    public GameObject levelTmp; 
    
    /// <summary>
    /// Define os atributos do botão de nível, incluindo número, dificuldade, cor, nome do arquivo e exibição de informações.
    /// </summary>
    /// <param name="lvl">Número do nível.</param>
    /// <param name="dif">Dificuldade do nível.</param>
    /// <param name="clr">Cor do botão.</param>
    /// <param name="file">Nome do arquivo do nível.</param>
    public void SetAttributes(int lvl, int dif, Color clr, string file)
    {
        print("build");
        level = lvl;
        difficulty = dif;
        levelString = file;
        gameObject.GetComponent<Image>().color = clr;
        levelTmp.gameObject.GetComponent<TextMeshProUGUI>().text = "" + level;

        if(!PlayerPrefs.HasKey(file))
        {
            levelEficienciaTmp.SetActive(false);
        }
        else
        {
            levelEficienciaTmp.GetComponent<TextMeshProUGUI>().text = (int)(PlayerPrefs.GetFloat(file)*100) + "%";
        }
    }
    
    /// <summary>
    /// Carrega o cenário de jogo associando o nível.
    /// </summary>
    public void Load()
    {
        GetComponent<LoadGameScene>().GameScene(levelString);
    }
}
