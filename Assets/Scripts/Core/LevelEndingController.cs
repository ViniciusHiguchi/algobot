using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Controla o final do nível, exibindo uma sobreposição com estrelas e eficiência alcançada.
/// </summary>
public class LevelEndingController : MonoBehaviour
{
    // Array de GameObjects representando as estrelas.
    public GameObject[] estrelas = new GameObject[3];
    
    private float eficiencia;
    
    // Atualização gradual da eficiência para exibição suave
    private float updateEficiencia = 0;
    
    // Índice para controlar a ativação das estrelas.
    private float i = 0;
    
    // Referência ao controlador de eficiência.
    public GameObject eficienciaController;
    
    // TextMeshProUGUI para exibir a eficiência na sobreposição.
    public GameObject texto_eficiencia;
    

    
    /// <summary>
    /// Chamado a cada quadro.
    /// Atualiza gradualmente a eficiência e ativa as estrelas conforme necessário.
    /// </summary>
    private void Update()
    {
        
        // Atualiza gradualmente a eficiência para exibição suave.
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
        
        // Ativa estrelas conforme a eficiência alcançada. 
        if ((updateEficiencia) / (100f*((i+1f)/3f)) >= 1 && i <= 3)
        {
            estrelas[(int)i].SetActive(true);
            i += 1;
        }

    }

    /// <summary>
    /// Exibe a sobreposição de final de nível após um breve intervalo.
    /// </summary>
    public void ShowOverlay()
    {
        i = 0;
        updateEficiencia = 0f;
        texto_eficiencia.GetComponent<TextMeshProUGUI>().text = ("0%");
        eficiencia = eficienciaController.GetComponent<Eficiencia>().GetEficiencia();
        Invoke("EndingOverlay", 1);
    }
    
    /// <summary>
    /// Ativa a sobreposição de final de nível.
    /// </summary>
    private void EndingOverlay()
    {
        this.gameObject.SetActive(true);
        estrelas[0].SetActive(false);
        estrelas[1].SetActive(false);
        estrelas[2].SetActive(false);
    }
    
}
