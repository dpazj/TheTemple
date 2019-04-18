using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class LevelManager : MonoBehaviour {

    public event System.Action CompleteNotify;

    GameObject character;
    GameObject gameMap;

    private void Awake()
    {
        character = GameObject.Find("Character");
        gameMap = GameObject.Find("GameMap");
    }

    public void spawnPlayer(bool postProActive)
    {
        
        character.transform.position = gameMap.GetComponent<RespawnPoints>().spawn;
        setPostProcessing(postProActive);
    }

    public void setPostProcessing(bool active)
    {
        character.transform.Find("Camera").GetComponent<PostProcessingBehaviour>().enabled = active; 
    }

    public void test()
    {
        Debug.Log("aa");
    }

    private void levelComplete()
    {
        if (this.CompleteNotify != null) this.CompleteNotify();
    }
    
}
