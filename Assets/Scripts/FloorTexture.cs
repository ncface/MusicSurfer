using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTexture : MonoBehaviour
{
    public GameObject floorPrefab;
    public float yPosition;
    
    // Start is called before the first frame update
    public void createFloor(double length)
    {
        for (int i= 0; i <= (length / floorPrefab.transform.localScale.z); i++)
        {
            GameObject newFloor = Instantiate(
                floorPrefab, 
                new Vector3(0, yPosition, i * floorPrefab.transform.localScale.z), 
                Quaternion.identity);
            newFloor.transform.parent = transform;

        }
    }

    
}
