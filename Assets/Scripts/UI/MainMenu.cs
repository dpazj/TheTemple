using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void quit()
    {
        Application.Quit();
    }

    public void resume()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().togglePause();
    }

    public void mainMenu()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().loadScene("Menu");
    }

    public void play()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().loadScene("Tutorial");
    }

        
    
}
