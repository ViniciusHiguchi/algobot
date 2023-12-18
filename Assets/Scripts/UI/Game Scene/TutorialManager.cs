using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Me desculpem, mas todo o tutorial é hardcodado como uma lista de gameobjects dentro de um gameobject no editor.
/// este código só serve para salvar se o jogador já completou o tutorial alguma vez, mostrar o tutorial novamente,
/// ou se o jogador nunca completou o tutorial (primeira vez jogando o jogo em alguma instalação) ele mostra o tutorial.
/// </summary>
public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Tutorial") || PlayerPrefs.GetInt("Tutorial") == 0)
        {
            tutorialPanel.SetActive(true);
        }
    }

    public void TutorialSetComplete()
    {
        PlayerPrefs.SetInt("Tutorial",1);
        PlayerPrefs.Save();
    }

    public void ResetTutorial()
    {
        PlayerPrefs.SetInt("Tutorial",0);
        PlayerPrefs.Save();
        tutorialPanel.SetActive(true);
    }
}
