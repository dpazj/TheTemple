using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class LevelManager : MonoBehaviour {

    IObserver gameManager;
    public GameObject gameMap;
    public GameObject character;
    public Vector3[] respawnPoints { get; protected set; }
    public Vector3 spawn { get; protected set; }

    private void Awake()
    {
        spawn = gameMap.GetComponent<RespawnPoints>().spawn;
        respawnPoints = gameMap.GetComponent<RespawnPoints>().respawnPoints;
    }

    public void spawnPlayer()
    {
        character.transform.position = spawn;
    }

    public void setPlayerSensitivity(float sens)
    {
        character.GetComponentInChildren<PlayerCamController>().sensitivity = sens;
    }
    public void setPostProcessing(bool active)
    {
        character.GetComponentInChildren<PostProcessingBehaviour>().enabled = active; 
    }

    public void complete(string nextScene)
    {
        if(gameManager == null) { Debug.Log("No GameManager in scene"); return;}
        gameManager.Notify(nextScene);
    }

    public void setObserver(IObserver cb)
    {
        gameManager = cb;
    }

    public void pauseCharacter(bool val)
    {
        character.GetComponent<MovementInfo>().paused = val;
    }

    
}
