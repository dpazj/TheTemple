using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour {

    GameManager gameManager;

    

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void setQuality(int level)
    {
        gameManager.setQualityLevel(level);
    }

    public void setPostProcessing(bool val)
    {
        gameManager.setPostProcessing(val);
    }

}
