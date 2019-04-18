using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDeathBehaviour : MonoBehaviour, IRespawn {
    
    public Vector3[] respawnPoints { get; set;}
    MovementInfo movementInfo;

    private void Awake()
    {
        getRespawnPoints();
    }

    private void Start()
    {
        movementInfo = GetComponent<MovementInfo>();
    }

    public void getRespawnPoints()
    {
        respawnPoints = GameObject.Find("GameMap").GetComponent<RespawnPoints>().respawnPoints;
    }
    public void respawn()
    {
        Vector3 closest = transform.position;
        float smallestDistance = Mathf.Infinity;

        for (int i = 0; i < respawnPoints.Length; i++)
        {
            Vector3 direction = respawnPoints[i] - transform.position;
            float distance = direction.sqrMagnitude;
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                closest = respawnPoints[i];
            }
        }

        movementInfo.zeroValues();
        transform.position = closest;
    } 


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ocean")
        {
            respawn();
        }
    }

    
}
