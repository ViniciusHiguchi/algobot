using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Status : MonoBehaviour
{
    public GameObject StatusNivel;
    public GameObject StatusDificuldade;
    public GameObject StatusTentativa;

    //public GameObject levelEndingRouter;
    
    // Start is called before the first frame update
    void Start()
    {
        StatusNivel.GetComponent<TextMeshProUGUI>().text = ""+LoadGameScene.levelToLoad[2] + LoadGameScene.levelToLoad[3];
        StatusDificuldade.GetComponent<TextMeshProUGUI>().text = ""+LoadGameScene.levelToLoad[0];
        StatusTentativa.GetComponent<TextMeshProUGUI>().text = "1";
    }

    public void SetStatusText(int tentativa, string level)
    {
        StatusNivel.GetComponent<TextMeshProUGUI>().text = ""+level[2] + level[3];
        StatusDificuldade.GetComponent<TextMeshProUGUI>().text = ""+level[0];
        StatusTentativa.GetComponent<TextMeshProUGUI>().text = ""+ tentativa;
    }
    
}
