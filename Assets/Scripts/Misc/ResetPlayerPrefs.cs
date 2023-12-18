using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// isso provavelmente foi usado só para debugging, como eu estava testando coisas como colocar a taxa de performance na UI no menu principal, eu queria ter certeza
/// que nada salvo pelo código antigo continuasse sendo usada nos meus dispositivos.
///
/// Vou deixar por aqui porque pode ser útil.
/// </summary>
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
