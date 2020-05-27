using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonAnimation : MonoBehaviour
{
    public RuntimeAnimatorController IdleAnimation;
    public RuntimeAnimatorController RunAnimation;
    public RuntimeAnimatorController JumpAnimation;

    public void Update()
    {
        //Vector3 playerPosition = transform.parent.transform.position;
        //float y = (-1) * (playerPosition.y - 1) - 1.05f;
        //transform.localPosition = new Vector3(0, y, 0);
    }

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
        //GetComponent<Animator>().runtimeAnimatorController = JumpAnimation;
        GetComponent<Animator>().runtimeAnimatorController = RunAnimation;
    }
}
