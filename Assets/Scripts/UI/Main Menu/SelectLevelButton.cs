using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Contém os dados do respectivo botão, e controla atributos.
/// </summary>
public class SelectLevelButton : MonoBehaviour
{

    public int level;
    public int difficulty;

    public string levelString;
    
    public Color color;


    public GameObject levelEficienciaTmp;
    public GameObject levelTmp; 


    // Start is called before the first frame update
    void Start()
    {
        
    }
//build new shit
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

    public void Load()
    {
        GetComponent<LoadGameScene>().GameScene(levelString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
