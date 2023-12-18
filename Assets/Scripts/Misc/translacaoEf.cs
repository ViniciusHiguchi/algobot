using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// esse código serve para fazer a engrenagem da barra inferior da GameScene rodar quando o código do robô está sendo executado.
///
/// se tem uma coisa que eu aprendi com os jogos da Blizzard, é que detalhes são extremamente importantes para fazer os jogos parecerem mais vivos.
/// Inlusive neste jogo ainda não foi tão bem trabalhado estes aspectos.
/// </summary>
public class translacaoEf : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0f,0f,-60*Time.deltaTime);
    }
}
