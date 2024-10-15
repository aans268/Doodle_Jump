using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fondrecycleur : MonoBehaviour
{
    public GameObject[] backgrounds;
    public Transform playerTransform;
    public float recycleOffset =20f;
    

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject background in  backgrounds)
        {
            if (background.transform.position.y + recycleOffset < playerTransform.position.y)
            {
                RecycleBackground(background);
            }
        }
        
    }

    void RecycleBackground(GameObject background)
    {
        float newYPosition = playerTransform.position.y + recycleOffset;
        background.transform.position= new Vector3(background.transform.position.x, newYPosition, background.transform.position.z);
    }
    
}
