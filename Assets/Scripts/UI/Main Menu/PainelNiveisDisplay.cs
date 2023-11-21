using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Instancia e atribui valores (dificuldade) aos botões do tipo SelectLevelButton,
/// de acordo com dados recebidos da classe ReadLevel.
/// </summary>
public class PainelNiveisDisplay : MonoBehaviour
{
    private List<string> levels;
    private List<GameObject> lvlButtonList;

    public GameObject lvlButton;
    public GameObject readLevel;
    public void DisplayDifficulty(int dif, Color clr)
    {
        print("is here (dd): " + levels.Count);
        foreach (var button in lvlButtonList)
        {
            if (button) {
                Destroy(button);
            }
        }

        lvlButtonList = new List<GameObject>();
        levels.Sort();
        foreach (var file in levels) {
            print("is here");
            print(file[0].ToString()+ "=="+ dif.ToString());
            if (file[0].ToString() == dif.ToString()) {
                GameObject btn = Instantiate(lvlButton, this.gameObject.transform);
                btn.GetComponent<SelectLevelButton>().SetAttributes(Int32.Parse(""+file[2]+file[3]),dif,clr,file);
                lvlButtonList.Add(btn);
            }
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {
        print("is here (start)");
        readLevel.GetComponent<ReadLevel>().Read();
        levels = ReadLevel.levelFiles;
        lvlButtonList = new List<GameObject>();
        DisplayDifficulty(1, Color.white);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
