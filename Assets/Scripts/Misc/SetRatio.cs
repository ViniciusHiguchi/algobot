using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
