using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinSpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject[] coinPrefabs;
    private Vector3 coinSpawnPos;

    //public Collider[] colliders;
    //public float radius;
   

    void Update()
    {
        
        float distanceToHorizon = Vector3.Distance(player.gameObject.transform.position, coinSpawnPos);
        if (distanceToHorizon < 120)
        {
          SpawnCoins();
        }
    }

    void SpawnCoins()
    {
        coinSpawnPos = new Vector3(0, 0, coinSpawnPos.z + 30);
        Instantiate(coinPrefabs[(Random.Range(0, coinPrefabs.Length))], coinSpawnPos, Quaternion.identity);
    }

    // void SpawnCoins()
    // {
    //     bool coinSpawned = false;
    //     bool canSpawnHere= false;

    //     while (!canSpawnHere && !coinSpawned)
    //     {

    //         coinSpawnPos = new Vector3(coinSpawnPos.x + Random.Range(-0.3f, 0.3f), 0, coinSpawnPos.z + Random.Range(33f, 35f));
    //         canSpawnHere = preventSpawnOverlap(coinSpawnPos);
    //     }

    //     Instantiate(coinPrefabs[(Random.Range(0, coinPrefabs.Length))], coinSpawnPos, Quaternion.identity);
    //     coinSpawned = true;
    // }

    //bool preventSpawnOverlap(Vector3 coinSpawnPos)
    // {
    //     colliders = Physics.OverlapSphere(ObejectSpawner.instance.SpawnObstaclePosition, radius);
    //     for(int i= 0; i< colliders.Length; i++)
    //     {
    //         Vector3 centrePoint = colliders[i].bounds.center;
    //         float length = colliders[i].bounds.extents.z;
    //         float breadth = colliders[i].bounds.extents.x;

    //         float frontExtent = centrePoint.z + length;
    //         float backExtent = centrePoint.z - length;

    //         float leftExtent = centrePoint.x - breadth;
    //         float rightExtent = centrePoint.x + breadth;

    //         if(coinSpawnPos.z >= backExtent && coinSpawnPos.z <= frontExtent)
    //         {   
    //             if(coinSpawnPos.x >= leftExtent && coinSpawnPos.x <= rightExtent)
    //             return false;
    //         }

    //     }
    //     return true;
    // }



}
