using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IObserver {

    [SerializeField]
    private string menuScene;
    [SerializeField]
    private string tutorialScene;
    [SerializeField]
    private string islandScene;
    [SerializeField]
    private string finalLevelScene;
    [SerializeField]
    GameObject loadingBar;


    private int qualityLevel;
    private bool postProcessing = true;

    LevelManager currentLevelManager;
    IslandManager islandManager;
    string currentSceneName;

    public static GameManager instance = null;
    private void Awake() //Singleton
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    public void setQualityLevel(int level)
    {
        qualityLevel = level;
        QualitySettings.SetQualityLevel(qualityLevel, true);
    }
    public int getQualityLevel() { return this.qualityLevel; }

    public void setPostProcessing(bool val)
    {
        postProcessing = val;
        //TODO enable character post processing
    }

    public bool getPostProcessing() { return this.postProcessing; }

    public void loadScene(string scene)
    {
        
        if (scene.Equals(islandScene))
        {
            StartCoroutine(LoadScene(islandScene));
            currentSceneName = islandScene;
        }
        else
        {
            Debug.Log("Wrong Scene Name");
            return;
        }
    }

    private void finishSceneCreation()
    {

        currentLevelManager = GameObject.Find(currentSceneName + "Manager").GetComponent<LevelManager>();
        currentLevelManager.spawnPlayer(true);
        

    }

    IEnumerator LoadScene(string scene)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(scene);
        Slider slider = loadingBar.GetComponent<Slider>();

        while (!load.isDone)
        {
            float progress = Mathf.Clamp01(load.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }

        finishSceneCreation();
    }

    public void Notify<T>(T t)
    {
        T t1 = (T)(object)t;
        string scene = (string)(object)t1;
        loadScene(scene);


    }

   
}
