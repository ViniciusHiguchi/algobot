using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eficiencia : MonoBehaviour
{
    private Slider eficiencia;
    private float total=0;
    private float efetivo=0;
    
    // Start is called before the first frame update
    void Start()
    {
        eficiencia = this.gameObject.GetComponent<Slider>();
        eficiencia.value = 1;
    }

    public void ResetEficiencia()
    {
        eficiencia.value = 1;
        total=0;
        efetivo=0;
    }

    public void CalculoEficiencia(float statusAcao) //status ação assume valor 1 ou 0, sendo que 1 conta como uma ação efetiva e 0 como não efetiva
    {
        total += 1;
        efetivo += statusAcao;
        print("efetivo: "+ efetivo);
        print("total: "+ total);
        print("ef: "+ (efetivo/total));
        UpdateEficiencia((efetivo/total));
    }

    void UpdateEficiencia(float valor)
    {
        eficiencia.value = valor;
    }

    public float GetEficiencia()
    {
        return eficiencia.value;
    }
}
