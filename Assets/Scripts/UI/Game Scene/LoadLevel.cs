using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


/// <summary>
/// (refatorado) Responśavel por receber o nome do arquivo e retornar a matriz do mapa.
/// </summary> 
public class LoadLevel : MonoBehaviour
{
    private int[,] map = new int[4,4];
    // Start is called before the first frame update
    public int[,] LoadLevelFromFile(string file)
    {
        print("start load");
        string path;
        string text;
        string[] lines;
        int i;
        int j;
        #if UNITY_ANDROID && !UNITY_EDITOR
        path = "jar:file://" + Application.dataPath + "!/assets/levels/" + file;
        var loadingRequest = UnityWebRequest.Get(path);
        loadingRequest.SendWebRequest();
        while (!loadingRequest.isDone)
        {
            print("is loading");
            if (loadingRequest.isNetworkError || loadingRequest.isHttpError)
            {
                break;
            }
        }
        if (loadingRequest.isNetworkError || loadingRequest.isHttpError) { }
        else
        {
            text = loadingRequest.downloadHandler.text;
            //File.WriteAllText(Path.Combine(Application.persistentDataPath,"currentLvl.txt"),loadingRequest.downloadHandler.data.ToString());
            //text = File.ReadAllText(Path.Combine(Application.persistentDataPath, "currentLvl.txt"));
            lines = text.Split('\n');
            i = 0;
            foreach (var line in lines)
            {   
                j = 0;
                if (i == 4)
                {
                    break;
                }
                string[] lineArray = line.Split(' ');
                foreach (var value in lineArray)
                {
                    if (j == 4)
                    {
                        break;
                    }
                    print(value);
                    try
                    {
                        map[i, j] = Int32.Parse(value);
                        j++;
                    }
                    catch (Exception e)
                    {
                        print(e);
                    }
                }
            i++;
            }
        }
        #endif
        #if UNITY_EDITOR
        path = Application.streamingAssetsPath + "/levels/" + file;
        text = File.ReadAllText(path);
        print(File.ReadAllText(path));
        print(text);
        lines = text.Split('\n');
        i = 0;
        foreach (var line in lines)
        {
            j = 0;
            if (i == 4)
            {
                break;
            }
            string[] lineArray = line.Split(' ');
            foreach (var value in lineArray)
            {
                if (j == 4)
                {
                    break;
                }
                print(value);
                try
                {
                    map[i, j] = Int32.Parse(value);
                    j++;
                }
                catch (Exception e)
                {
                    print(e);
                }
            }
            i++;
        }
        #endif
        
        print(map);
        return map;
    }
}
