using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviro_spawner : MonoBehaviour
{
    public GameObject player;
    public GameObject[] EnvPrefabs; 

    private Vector3 SpawnObstaclePosition;

    void Update()
    {
        float distanceToHorizon = Vector3.Distance(player.gameObject.transform.position, SpawnObstaclePosition);
        if (distanceToHorizon < 120)
        {
            SpawnTriangles();

        }
    }

    void SpawnTriangles()
    {
        SpawnObstaclePosition = new Vector3(0, 0, SpawnObstaclePosition.z + 30);
        Instantiate(EnvPrefabs[(Random.Range(0, EnvPrefabs.Length))], SpawnObstaclePosition, Quaternion.identity);
    }

}
