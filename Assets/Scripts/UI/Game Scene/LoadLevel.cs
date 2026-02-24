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
    // Matriz que representa o mapa do nível.
    private int[,] map = new int[4,4];
    
    /// <summary>
    /// Carrega o nível a partir de um arquivo e retorna a matriz correspondente.
    /// </summary>
    /// <param name="file">Nome do arquivo do nível.</param>
    /// <returns>Matriz que representa o mapa do nível.</returns>
    public int[,] LoadLevelFromFile(string file)
    {
        print("start load");
        string path;
        string text;
        string[] lines;
        int i;
        int j;
        #if UNITY_ANDROID && !UNITY_EDITOR
        // Caminho para o arquivo no Android.
        path = "jar:file://" + Application.dataPath + "!/assets/levels/" + file;

        // Cria uma solicitação para carregar o arquivo usando UnityWebRequest.
        // isto faz com que o unity consiga ler o mapa do jogo do mesmo modo que um navegador lê os arquivos no armazenamento local
        var loadingRequest = UnityWebRequest.Get(path);
        loadingRequest.SendWebRequest();

        // Aguarda até que a solicitação de carregamento esteja concluída.
        while (!loadingRequest.isDone)
        {
            print("is loading");
            if (loadingRequest.isNetworkError || loadingRequest.isHttpError)
            {
                break;
            }
        }

        // Verifica se houve um erro durante o carregamento.
        if (loadingRequest.isNetworkError || loadingRequest.isHttpError) { }
        else
        {
            // Obtém o texto do arquivo carregado.
            text = loadingRequest.downloadHandler.text;

            // Divide o texto em linhas.
            lines = text.Split('\n');
            i = 0;

            // Itera sobre as linhas para preencher a matriz do mapa.
            foreach (var line in lines)
            {   
                j = 0;
                if (i == 4)
                {
                    break;
                }
                string[] lineArray = line.Split(' ');

                // Itera sobre os valores na linha para preencher as colunas da matriz.
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
        #if UNITY_EDITOR || UNITY_STANDALONE
        
        // Caminho para o arquivo no Editor Unity.
        path = Application.streamingAssetsPath + "/levels/" + file;
        
        
        //Eu tenho a impressão que dá para abstrair essa função de ler o mapa do jogo em apenas uma
        //reduzindo significativamente a quantidade de linhas nesse código
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
