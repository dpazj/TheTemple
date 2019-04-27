using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    GameManager gameManager;
    public Slider sensSlider;
    public Toggle ppToggle;
    


    

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        sensSlider.value = gameManager.sensitivity;
        ppToggle.isOn = gameManager.postProcessing;

        sensSlider.onValueChanged.AddListener((val) => { this.setSensitivity(val);});
        ppToggle.onValueChanged.AddListener((val) => { this.setPostProcessing(val); });

    }

    public void setQuality(int level)
    {
        QualitySettings.SetQualityLevel(level, true);
    }

    public void setPostProcessing(bool val)
    {
        gameManager.setPostProcessing(val);
    }

    public void setSensitivity(float sens)
    {
        gameManager.setSensitivity(sens);
    }

}
