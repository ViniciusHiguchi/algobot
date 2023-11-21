using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelEndingController : MonoBehaviour
{
    public GameObject[] estrelas = new GameObject[3];
    private float eficiencia;
    private float updateEficiencia = 0;
    private float i = 0;
    
    public GameObject eficienciaController;
    public GameObject texto_eficiencia;
    

    

    private void Update()
    {
        if (updateEficiencia < eficiencia*100)
        {
            updateEficiencia += Time.deltaTime * ((10+eficiencia*100)-updateEficiencia);
            texto_eficiencia.GetComponent<TextMeshProUGUI>().text = ((int)updateEficiencia + "%");
        }
        else if (updateEficiencia > eficiencia*100)
        {
            updateEficiencia = eficiencia * 100;
            texto_eficiencia.GetComponent<TextMeshProUGUI>().text = ((int)updateEficiencia + "%");
        }
        //print(updateEficiencia +" "+(100f*(i+1f)/3f) +" "+ updateEficiencia/(100f*((i+1f)/3f)));
        if ((updateEficiencia) / (100f*((i+1f)/3f)) >= 1 && i <= 3)
        {
            estrelas[(int)i].SetActive(true);
            i += 1;
        }

    }

    public void ShowOverlay()
    {
        i = 0;
        updateEficiencia = 0f;
        texto_eficiencia.GetComponent<TextMeshProUGUI>().text = ("0%");
        eficiencia = eficienciaController.GetComponent<Eficiencia>().GetEficiencia();
        Invoke("EndingOverlay", 1);
    }
    
    private void EndingOverlay()
    {
        this.gameObject.SetActive(true);
        estrelas[0].SetActive(false);
        estrelas[1].SetActive(false);
        estrelas[2].SetActive(false);
    }
    
}
