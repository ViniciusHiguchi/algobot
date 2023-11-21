using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

#if UNITY_ANDROID && !UNITY_EDITOR
using System.IO.Compression;
#endif
using UnityEngine;

/// <summary>
/// Responsável por ler os arquivos do dispositivo e armazenar os arquivos lidos, conforme necessário.
/// </summary>

public class ReadLevel : MonoBehaviour
{
    private string filePath;

    public static List<string> levelFiles;
    //# if UNITY_ANDROID
    //private UnityWebRequest webRequest = new UnityWebRequest();
    public void Read()
    {
        filePath = "not found";

        List<string> levels = new List<string>();
        
        #if UNITY_ANDROID && !UNITY_EDITOR
        filePath = Application.dataPath;
        using (ZipArchive archive = ZipFile.OpenRead(filePath))
        {
            foreach (ZipArchiveEntry entry in archive.Entries)
            { 
                if (entry.FullName.Contains("assets/levels/") && entry.FullName.Contains(".txt")) 
                {
                        //print(entry.FullName.Remove(0,14));
                        levels.Add(entry.FullName.Remove(0,14)); 
                }
                //entry.ExtractToFile(Path.Combine(destFolder, entry.FullName));
            }
        }

        #endif
        #if UNITY_EDITOR
        filePath = "Assets/StreamingAssets/levels/";
        print("editor");
        if (filePath != "not found")
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(filePath);
                FileInfo[] files = dir.GetFiles("*.txt");
                //load
                foreach (var file in files)
                {
                    levels.Add(file.Name);
                    Debug.Log(file.Name);
                }
            }
            catch (Exception e)
            {
                print(e);
            }
        }
        #endif
        if (filePath == "not found")
        {
            print("platform not supported");
        }

        levelFiles = levels;
    }
    
   // #endif
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

