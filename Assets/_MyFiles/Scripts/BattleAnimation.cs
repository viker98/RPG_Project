using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimation : MonoBehaviour
{
    Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public Animator GetAnimator()
    {
        return Animator;
    }

}
