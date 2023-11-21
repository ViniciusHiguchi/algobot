using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //reset dos mapas para não confundir na hora dos testes dessa versão
        if (!PlayerPrefs.HasKey("(1.1.3)_Reset"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("(1.1.3)_Reset", 1);
            PlayerPrefs.Save();
        }
    }
}
