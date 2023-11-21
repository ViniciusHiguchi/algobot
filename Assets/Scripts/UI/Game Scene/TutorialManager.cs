using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
