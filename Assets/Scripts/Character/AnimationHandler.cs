using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private float blend = 0;
    private float time=-5;
    private bool playing = false;
    private bool playing1 = false;
    private bool playing2 = false;
    
    //para x ou z > 0 animação back (de costas pra tela)
    //para z a escala do transform.LocalScale.x deve ser sempre positiva, para x, negativa.

    private void Start()
    {
        GetComponent<Animator>().Play("Stand",0);
    }
    
    public void Stand(Vector2 forward)
    {
        if (forward == new Vector2(1,0))
        {
            GetComponent<Animator>().Play("Stand_back",0);
        }
        if (forward == new Vector2(-1,0))
        {
            GetComponent<Animator>().Play("Stand",0);
        }
        if (forward == new Vector2(0,1))
        {
            GetComponent<Animator>().Play("Stand_back",0);
        }
        if (forward == new Vector2(0,-1))
        {
            GetComponent<Animator>().Play("Stand",0);
        }
        SetEscalaX(forward);
    }
    
    void SetEscalaX(Vector2 forward)
    {
        if (forward == new Vector2(1,0))
        {
            GetComponent<Transform>().localScale = new Vector3(
                -1*Math.Abs(GetComponent<Transform>().localScale.x), 
                GetComponent<Transform>().localScale.y, 
                GetComponent<Transform>().localScale.z );
        }
        if (forward == new Vector2(-1,0))
        {
            GetComponent<Transform>().localScale = new Vector3(
                -1*Math.Abs(GetComponent<Transform>().localScale.x), 
                GetComponent<Transform>().localScale.y, 
                GetComponent<Transform>().localScale.z );
        }
        if (forward == new Vector2(0,1))
        {
            GetComponent<Transform>().localScale = new Vector3(
                Math.Abs(GetComponent<Transform>().localScale.x), 
                GetComponent<Transform>().localScale.y, 
                GetComponent<Transform>().localScale.z );
        }
        if (forward == new Vector2(0,-1))
        {
            GetComponent<Transform>().localScale = new Vector3(
                Math.Abs(GetComponent<Transform>().localScale.x), 
                GetComponent<Transform>().localScale.y, 
                GetComponent<Transform>().localScale.z );
        }
    }

    public void Jump(Vector2 forward)
    {
        if (forward == new Vector2(1,0))
        {
            GetComponent<Animator>().Play("jump_back",0);
        }
        if (forward == new Vector2(-1,0))
        {
            GetComponent<Animator>().Play("jump_front",0);
        }
        if (forward == new Vector2(0,1))
        {
            GetComponent<Animator>().Play("jump_back",0);
        }
        if (forward == new Vector2(0,-1))
        {
            GetComponent<Animator>().Play("jump_front",0);
        }
    }

    public void Land(Vector2 forward)
    {
        if (forward == new Vector2(1,0))
        {
            GetComponent<Animator>().Play("landing_back",0);
        }
        if (forward == new Vector2(-1,0))
        {
            GetComponent<Animator>().Play("landing_front",0);
        }
        if (forward == new Vector2(0,1))
        {
            GetComponent<Animator>().Play("landing_back",0);
        }
        if (forward == new Vector2(0,-1))
        {
            GetComponent<Animator>().Play("landing_front",0);
        }
    }
    
    public void Walk(Vector2 forward)
    {
        if (forward == new Vector2(1,0))
        {
            GetComponent<Animator>().Play("walk_back",0);
        }
        if (forward == new Vector2(-1,0))
        {
            GetComponent<Animator>().Play("walk_front",0);
        }
        if (forward == new Vector2(0,1))
        {
            GetComponent<Animator>().Play("walk_back",0);
        }
        if (forward == new Vector2(0,-1))
        {
            GetComponent<Animator>().Play("walk_front",0);
        }
    }
}
