using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
    //uma variável public static no Unity tem duas propriedades importantes:
    // - você não pode atribuir outro valor para ela,
    // - e a variável se mantém ao trocar de cena. Esta é a forma que o parâmetro do nível a ser construido chega na cena de jogo.
    public static string levelToLoad;
    public void GameScene(string level)
    {
        /*
         * Função responsável por carregar a cena do jogo a partir do menu principal.
         *
         * Parâmetros:
         * (str) level -> nome do arquivo na pasta StreamingAssets referente ao nível que a função vai carregar (ex: '1_01.txt')
        */
        
        levelToLoad = level;
        SceneManager.LoadScene("Scenes/GameScene");
    }
}
