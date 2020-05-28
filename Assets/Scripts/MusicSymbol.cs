using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class MusicSymbol : MonoBehaviour
{

    public AnimatorController noteAnimator;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().SetFloat("speed", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
