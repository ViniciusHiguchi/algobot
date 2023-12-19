using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LevelEndingRouter : MonoBehaviour
{
    // Nome do próximo mapa a ser carregado.
    private string nextMap;

    // Dificuldade atual em níveis discretos para a escolha do mapa.
    private int dificuldadeAtual = 0;

    // Quantidade de partidas consecutivas com eficiência >= 66%.
    private int comboAtual;

    // Posição do mapa atual na lista de mapas.
    private int mapasCompletadosAtual = 0;
    
    // Identificador do nível.
    private int identificadorNivel;

    // Número de partidas em comboAtual necessárias para avançar na dificuldade.
    private int partidasToDificuldade = 3;

    // Contador de tentativas.
    private int tentativa = 1;

    // Referências para sobreposições de retry e level ending.
    public GameObject retryOverlay;
    public GameObject levelEndingOverlay;

    // Referências para objetos a serem resetados.
    public GameObject character;
    public GameObject painelAcoes;
    public GameObject interpretador;
    public GameObject bottomMenu;

    // Referência ao controlador de eficiência.
    public GameObject eficienciaController;

    /// <summary>
    /// Este método deve ser chamado depois da primeira execução do interpretador.
    /// Inicializa as variáveis de controle do nível.
    /// </summary>
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
    
    /// <summary>
    /// Carrega o próximo nível e reseta as variáveis do jogo.
    /// </summary>
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
    
    /// <summary>
    /// Calcula o próximo mapa com base nas variáveis de controle do jogo.
    /// </summary>
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
            dificuldadeAtual = Mathf.Clamp(dificuldadeAtual + 1, 1, 5); //Mathf.clamp impede que o valor do resultado da operação seja maior que os limites estabelecidos
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
        //reset da variável, porque se o jogador não conseguir mudar a dificuldade,
        //todos os mapas depois de acabar os mapas disponíveis serão o primeiro mapa, o codigo sempre vai cair nessa exceção
        mapasCompletadosAtual = 0;
        return maps.First();
    }
    
    /// <summary>
    /// Reseta os objetos da cena após a conclusão do nível.
    /// </summary>
    public void ResetGameScene()
    {
        character.GetComponent<CharacterController>().ResetChar();
        painelAcoes.GetComponent<PainelAcoes>().ResetPainel();
    }

    /// <summary>
    /// Inicia a sobreposição de final de nível, determinando se foi concluído ou se deve ser tentado novamente.
    /// </summary>
    public void StartEndingOverlay(bool isFinish)
    {
        if (isFinish)
        {
            // Atualiza a pontuação máxima salva na memória se a eficiência atual for maior.
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

    /// <summary>
    /// Incrementa o contador de tentativas e reseta a cena para uma nova tentativa.
    /// </summary>
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
