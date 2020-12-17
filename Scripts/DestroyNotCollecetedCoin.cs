using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNotCollecetedCoin : MonoBehaviour
{
    private GameObject player;
    public int rotateSpeed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
        if (gameObject.transform.position.z < player.transform.position.z - 30)
        {
            Destroy(gameObject);
        }
    }
}
