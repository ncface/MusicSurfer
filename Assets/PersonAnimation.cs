using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonAnimation : MonoBehaviour
{
    public RuntimeAnimatorController IdleAnimation;
    public RuntimeAnimatorController RunAnimation;
    public RuntimeAnimatorController JumpAnimation;

    public void idle()
    {
        GetComponent<Animator>().runtimeAnimatorController = IdleAnimation;
    }

    public void run()
    {
        if (GetComponent<Animator>().runtimeAnimatorController != RunAnimation)
        {
            GetComponent<Animator>().runtimeAnimatorController = RunAnimation;
        }
    }

    public void jump()
    {
        GetComponent<Animator>().runtimeAnimatorController = JumpAnimation;
    }
}
