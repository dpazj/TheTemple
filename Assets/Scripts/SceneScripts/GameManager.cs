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
    [SerializeField]
    GameObject escapeMenu;
    private GameObject instantiatedEscapeMenu;

    LevelManager currentLevelManager;
    string currentSceneName;
    private bool paused = false;

    //Settings
    public float sensitivity = 2f;
    public bool postProcessing = true;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && currentSceneName != menuScene)
        {
            togglePause();
        }
    }

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
        else if(scene.Equals(menuScene))
        {
            StartCoroutine(LoadScene(menuScene));
            currentSceneName = menuScene;
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
        currentLevelManager.spawnPlayer();
        currentLevelManager.setPlayerSensitivity(sensitivity);
        currentLevelManager.setPostProcessing(postProcessing);
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
        else
        {
            Destroy(currentLevelManager);
        }
        
    }


    public void Notify<T>(T t)
    {
        T t1 = (T)(object)t;
        string scene = (string)(object)t1;
        
        loadScene(scene);
        
    }

    public void setSensitivity(float sens)
    {
        sensitivity = sens;
        if (currentLevelManager != null)
        {
            currentLevelManager.setPlayerSensitivity(sens);
        }
    }

    public void setPostProcessing(bool val)
    {
        postProcessing = val;
        if (currentLevelManager != null)
        {
            currentLevelManager.setPostProcessing(postProcessing);
        }
    }

    public void togglePause()
    {
        if (paused) {
            //do things to resume
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Destroy(instantiatedEscapeMenu);
            currentLevelManager.pauseCharacter(false);
        }
        else
        {
            //pause things
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            currentLevelManager.pauseCharacter(true);
            instantiatedEscapeMenu = Instantiate(escapeMenu);

        }

        paused = !paused;
               
    }

}
