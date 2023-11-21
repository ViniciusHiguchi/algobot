using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
