using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObejectSpawner : MonoBehaviour
{
    public static ObejectSpawner instance;
    public GameObject player;
    public GameObject[] trianglePrefabs;
     public  Vector3 SpawnObstaclePosition;


    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        float distanceToHorizon = Vector3.Distance(player.gameObject.transform.position, SpawnObstaclePosition);
        if(distanceToHorizon < 120)
        {
            SpawnTriangles();
           
        }
    }

    void SpawnTriangles()
    {
        SpawnObstaclePosition = new Vector3(0, 0, SpawnObstaclePosition.z+30);
        Instantiate(trianglePrefabs[(Random.Range(0,trianglePrefabs.Length))], SpawnObstaclePosition, Quaternion.identity);
    }

   
}
