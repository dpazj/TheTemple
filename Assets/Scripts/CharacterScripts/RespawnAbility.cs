using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAbility : MonoBehaviour {

    MovementInfo info;
    Vector3 respawnPoint;
    private bool canRespawn = false;
	
    void Start()
    {
        info = GetComponent<MovementInfo>();
    }
	void Update () {
        if (Input.GetKey(KeyCode.C) && info.grounded)
        {
            setRespawn();
        }else if (Input.GetKey(KeyCode.R) && info.grounded)
        {
            respawnAtPoint();
        }
	}

    private void setRespawn()
    {
        canRespawn = true;
        respawnPoint = transform.position;
    }

    private void respawnAtPoint()
    {
        if(!canRespawn) { return;}
        transform.position = respawnPoint;
    }
}
