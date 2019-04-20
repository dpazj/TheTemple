﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class LevelManager : MonoBehaviour {

    IObserver gameManager;
    GameObject gameMap;
    public GameObject character { get; protected set; }
    public Vector3[] respawnPoints { get; protected set; }
    public Vector3 spawn { get; protected set; }

    private void Awake()
    {
        character = GameObject.Find("Character");
        gameMap = GameObject.Find("GameMap");
        spawn = gameMap.GetComponent<RespawnPoints>().spawn;
        respawnPoints = gameMap.GetComponent<RespawnPoints>().respawnPoints;
    }

    public void spawnPlayer(bool postProActive)
    {
        
        character.transform.position = spawn;
        setPostProcessing(postProActive);
    }

    public void setPostProcessing(bool active)
    {
        character.transform.Find("Camera").GetComponent<PostProcessingBehaviour>().enabled = active; 
    }  

    public void complete()
    {
        gameManager.Notify("Island");
    }
}
