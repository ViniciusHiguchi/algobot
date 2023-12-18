using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// esta classe tem como finalidade salvar quais são os movimentos permitidos por um bloco, e se ele é o final da fase ou não.
///
/// bem útil para fazer IAs pra medir a performance de uma forma menos péssima do que está sendo feito agora.
///
/// se porventura vocês quiserem adicionar blocos com funcionalidades diferentes, tipo, um bloco que você tem que ativar para fazer algo,
/// esta classe deveria ser incrementada com a nova ação possível, como é só um getter/setter, não tem problema se isso ficar grande demais.
/// </summary>
public class ActionStatus : MonoBehaviour
{
    private bool isJumpable;
    private bool isWalkable;
    private bool isFinish;
    // Start is called before the first frame update
    
    public bool IsJumpable
    {
        get => isJumpable;
        set => isJumpable = value;
    }

    public bool IsWalkable
    {
        get => isWalkable;
        set => isWalkable = value;
    }

    public bool IsFinish
    {
        get => isFinish;
        set => isFinish = value;
    }
}
