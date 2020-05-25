using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Player : MonoBehaviour
{

    [Range(-2, 2)] public float moveValue;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");

        float newPosX = transform.position.x + moveValue * speed * Time.deltaTime;

        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
    }
}
