using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject GreenPlatformPrefab;
    public GameObject BluePlatformPrefab;
    public GameObject BrownPlatformPrefab;
    public GameObject WhitePlatformPrefab;


    public int platformCount=1000;

    // Limites du terrain
    private float minX = -3f;
    private float maxX = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = new Vector3();
        for (int i=0; i<platformCount;i++){
            spawnPosition.y +=Random.Range(.5f, 1.5f);
            spawnPosition.x = Random.Range(minX,maxX);
            //Instantiate(GreenPlatformPrefab,spawnPosition,Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
