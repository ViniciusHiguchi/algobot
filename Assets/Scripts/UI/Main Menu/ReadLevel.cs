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
    /// <summary>
    /// Lê os arquivos de nível do dispositivo e armazena os nomes dos arquivos na lista levelFiles.
    /// </summary>
    public void Read()
    {
        filePath = "not found";

        // Lista para armazenar os nomes dos arquivos de nível.
        List<string> levels = new List<string>();
        // Para Android, lê os arquivos de um arquivo ZIP (o APK é essencialmente um .zip).
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
        
        // Para o Editor Unity, lê os arquivos diretamente da pasta "Assets/StreamingAssets/levels/".
        #if UNITY_EDITOR
        filePath = "Assets/StreamingAssets/levels/";
        print("editor");
        if (filePath != "not found")
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(filePath);
                FileInfo[] files = dir.GetFiles("*.txt");
                // Adiciona os nomes dos arquivos de nível à lista.
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
        
    // Atualiza a lista estática com os nomes dos arquivos de nível.
        levelFiles = levels;
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

