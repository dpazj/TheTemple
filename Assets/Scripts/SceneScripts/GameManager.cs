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
    GameObject loaderBar;


    private int qualityLevel;
    private bool postProcessing = true;
    private bool enableIslandScene = false;

    LevelManager currentLevelManager;
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
        else if (scene.Equals(tutorialScene))
        {
            StartCoroutine(LoadScene(tutorialScene));
            currentSceneName = tutorialScene;
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
        currentLevelManager.setObserver(this);
        specificInit();
    }

    private void specificInit()
    {
        if (currentSceneName == islandScene)
        {
            GameObject.Find(currentSceneName + "Manager").GetComponent<IslandManager>().initIsland();
        }
        else if (currentSceneName == tutorialScene) {
            GameObject.Find(currentSceneName + "Manager").GetComponent<TutorialManager>().initTutorial();
        }
    }

    IEnumerator LoadScene(string scene)
    {
        if(scene == menuScene)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        AsyncOperation load = SceneManager.LoadSceneAsync(scene);
        Instantiate(loaderBar);
        Slider slider = loaderBar.GetComponentInChildren<Slider>();

        
        while (!load.isDone)
        {

            float progress = load.progress;
            slider.value = progress;
            yield return null;
        }
        

        if(scene != menuScene)
        {
            finishSceneCreation();
        }
        
    }

    

    public void Notify<T>(T t)
    {
        T t1 = (T)(object)t;
        string scene = (string)(object)t1;
        
        loadScene(scene);
        
    }

   
}
