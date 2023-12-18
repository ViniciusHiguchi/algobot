using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// este código serve para que o aspect ratio em celulares seja sempre 16:9. No editor unity parece que não tem problema usar free aspect ou 4:3,
/// mas evita alguns possíveis scales estranhos, principalmente em celulares modernos, tipo quadrados virando retângulos em 16:10, ou circulos virando elipses.
/// </summary>
public class SetRatio : MonoBehaviour
{
    void Start()
    {
        SetScrRatio(16, 9);
    }
    void SetScrRatio(float w, float h)
    {
        if ((((float)Screen.width) / ((float)Screen.height)) > w / h)
        {
            Screen.SetResolution((int)(((float)Screen.height) * (w / h)), Screen.height, true);
        }
        else
        {
            Screen.SetResolution(Screen.width, (int)(((float)Screen.width) * (h / w)), true);
        }
    }
}
