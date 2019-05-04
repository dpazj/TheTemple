using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAbility : MonoBehaviour {

    MovementInfo info;
    Vector3 respawnPoint;
    private bool canRespawn = false;
    private PlayerAudioController audio;
	
    void Start()
    {
        info = GetComponent<MovementInfo>();
        audio = GetComponent<PlayerAudioController>();
    }
	void Update () {
        if (Input.GetKeyDown(KeyCode.C) && info.grounded)
        {
            setRespawn();
        }else if (Input.GetKeyDown(KeyCode.R) && info.grounded)
        {
            respawnAtPoint();
        }
	}

    private void setRespawn()
    {
        canRespawn = true;
        respawnPoint = transform.position;
        respawnPoint.y += 1f;

        if(audio != null)
        {
            audio.playCheckPoint();
        }
    }

    private void respawnAtPoint()
    {
        if(!canRespawn) { return;}
        transform.position = respawnPoint;
    }
}
