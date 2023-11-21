using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LevelEndingRouter : MonoBehaviour
{
    private string nextMap;

    private int dificuldadeAtual = 0; //dificuldade em níveis discretos para escolha do mapa
    private int comboAtual; //quantidade de partidas consecutivas com eficiencia >= 66%
    private int mapasCompletadosAtual = 0; //int que represanta qual a posição do mapa na lista de mapas atuais.
    private int identificadorNivel;
    private int partidasToDificuldade = 3; //constante de quantas partidas em comboAtual são necessárias pra avançar
    private int tentativa = 1;

    public GameObject retryOverlay;
    public GameObject levelEndingOverlay;
    //referencias para reset
    public GameObject character;
    public GameObject painelAcoes;
    public GameObject interpretador;
    public GameObject bottomMenu;

    public GameObject eficienciaController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //este metodo deve ser chamado depois da primeira execução do interpretador.
    public void SetLevelControlVariables()
    {
        if (dificuldadeAtual == 0)
        {
            dificuldadeAtual = Int32.Parse(""+Intepretador.currentMap[0]);
            identificadorNivel = Int32.Parse(""+Intepretador.currentMap[2]+Intepretador.currentMap[3]);
        }

        int count = 0;
        List<string> maps = new List<string>();
        foreach (var levelFile in ReadLevel.levelFiles)
        {
            if (dificuldadeAtual.ToString() == levelFile[0].ToString())
            {
                count++;
                print(dificuldadeAtual.ToString() + " == " + levelFile[0].ToString());
                if (Intepretador.currentMap == levelFile)
                {
                    mapasCompletadosAtual = count - 1;
                }
            }
        }
        
        print("(start) combo: " + comboAtual);
        print("(start) mapas completados: " +mapasCompletadosAtual);
        //print("(start) eficiencia: " + eficienciaController.GetComponent<Eficiencia>().GetEficiencia());
        print("(start) dificuldade: " + dificuldadeAtual);
    }
    
    public void LoadNextLevel()
    {
        tentativa = 1;
        nextMap = NextMap();
        print("np "+ nextMap);
        interpretador.GetComponent<Intepretador>().ResetMapa();
        interpretador.GetComponent<Intepretador>().Iterador(nextMap);
        bottomMenu.GetComponent<Status>().SetStatusText(tentativa, nextMap);
        eficienciaController.GetComponent<Eficiencia>().ResetEficiencia();
        print("crr map "+ Intepretador.currentMap);
        ResetGameScene();
        print("LevelEndingRouter Line 44");
    }

    private string NextMap()
    {

        mapasCompletadosAtual++;
        if (eficienciaController.GetComponent<Eficiencia>().GetEficiencia() > 0.66f)
        {
            comboAtual++;
        }
        else
        {
            comboAtual = 0;
        }
        
        if (dificuldadeAtual == 0)
        {
            dificuldadeAtual = Int32.Parse(""+Intepretador.currentMap[0]);
            identificadorNivel = Int32.Parse(""+Intepretador.currentMap[2]+Intepretador.currentMap[3]);
        }

        if (comboAtual >= partidasToDificuldade)
        {
            mapasCompletadosAtual = 0;
            comboAtual = 0;
            dificuldadeAtual = Mathf.Clamp(dificuldadeAtual + 1, 1, 5);
            Debug.Log("nova dificuldade: "+dificuldadeAtual);
        }
        
        print("combo: " + comboAtual);
        print("mapas completados: " +mapasCompletadosAtual);
        print("eficiencia: " + eficienciaController.GetComponent<Eficiencia>().GetEficiencia());
        print("dificuldade: " + dificuldadeAtual);
        
        int count = 0;
        List<string> maps = new List<string>();
        foreach (var levelFile in ReadLevel.levelFiles)
        {
            print(dificuldadeAtual.ToString()+" == "+ levelFile[0].ToString());
            if (dificuldadeAtual.ToString() == levelFile[0].ToString())
            {
                print("found one more");
                maps.Add(levelFile);
                count++;
                if (count > mapasCompletadosAtual)
                {
                    return levelFile;
                }
            }
        }
        //reset da variável, porque se o jogador não conseguir mudar a dificuldade todos os mapas depois de acabar
        //os mapas disponíveis serão o primeiro mapa, pois o codigo sempre vai cair nessa exceção
        mapasCompletadosAtual = 0;
        return maps.First();
    }
    
    public void ResetGameScene()
    {
        character.GetComponent<CharacterController>().ResetChar();
        painelAcoes.GetComponent<PainelAcoes>().ResetPainel();
    }

    public void StartEndingOverlay(bool isFinish)
    {
        if (isFinish)
        {
            if (!PlayerPrefs.HasKey(Intepretador.currentMap) ||
                eficienciaController.GetComponent<Eficiencia>().GetEficiencia() >
                PlayerPrefs.GetFloat(Intepretador.currentMap))
            {
                PlayerPrefs.SetFloat(Intepretador.currentMap, eficienciaController.GetComponent<Eficiencia>().GetEficiencia());
                PlayerPrefs.Save();
            }
            
            levelEndingOverlay.GetComponent<LevelEndingController>().ShowOverlay();
        }
        else
        {
            retryOverlay.GetComponent<LevelEndingController>().ShowOverlay();
        }
    }

    public void Retry()
    {
        tentativa++;
        bottomMenu.GetComponent<Status>().SetStatusText(tentativa, Intepretador.currentMap);
        ResetGameScene();
    }
    public int GetTentativa()
    {
        return tentativa;
    }
    
    public void AddTentativa()
    {
        tentativa++;
    }
}
