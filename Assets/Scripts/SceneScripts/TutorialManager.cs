using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : LevelManager, IObserver {

    public GameObject blindingLight;
    public GameObject tutorialMessages;
    public GameObject gem;
    public Material completeMaterial;
    public Material uncompleteMaterial;
    public GameObject[] stages;

    private int stage = 0;
    private TutorialPopupControl tutorialPopup;

    bool cPressed = false;
    bool rPressed = false;

    //Movement
    float moveDistance = 0;
    public float requiredDistance = 150f;

    void Start()
    {
      initTutorial(); //Remove in build
    }
    public void initTutorial()
    {
        tutorialPopup = tutorialMessages.GetComponent<TutorialPopupControl>();
        character.GetComponent<CollisionNotifier>().setObserver(this);
        gem.GetComponent<TutorialGem>().setObserver(this);
        stageControl();
    }

    void Update()
    {

        if(stage == 0)
        {
            teachMovement();
        }else if(stage == 10)
        {
            teachRespawn();
        }
    }

  

    private void stageControl()
    {

        switch (stage)
        {
            case 0:
                tutorialPopup.createPopup("Use WASD to move");
                break;
            case 1:
                tutorialPopup.changePopup("Use SPACE to jump");
                break;
            case 2:
                tutorialPopup.changePopup("Use SHIFT to sprint. You may need to have a run up to build enough momentum!");
                break;
            case 3:
                tutorialPopup.changePopup("This one requires a strafe jump: run along the top of the stairs to get some speed up, jump and then turn to face the direction you wish to go");
                break;
            case 4:
                tutorialPopup.changePopup("Use the mouse to shift the direction of your momentum as you jump around");
                break;
            case 5:
                tutorialPopup.changePopup("Hold E to wall run on the right");
                break;
            case 6:
                tutorialPopup.changePopup("Hold Q to wall run on the left");
                break;
            case 7:
                tutorialPopup.changePopup("You can wall run round corners!");
                break;
            case 8:
                tutorialPopup.changePopup("While jumping and touching a wall, press space and move the mouse in the direction you wish to wall jump to");
                break;
            case 9:
                tutorialPopup.changePopup("While wall running, press space to launch yourself off the wall");
                break;
            case 10:
                tutorialPopup.changePopup("Use C to mark a checkpoint");
                break;
            case 11:
                tutorialPopup.changePopup("Collect the gem!");
                break;
            case 12:
                StartCoroutine(loadMainGame());
                tutorialPopup.changePopup("Tutorial Complete!");
                break;
        }
    }


    private void teachMovement()
    {
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        moveDistance += Mathf.Abs(h) + Mathf.Abs(v);

        if(moveDistance > requiredDistance)
        {
            stageComplete();
            load(stage);
        }
    }

    private void teachRespawn()
    {
        

        if (!cPressed) //c has to be pressed first
        {
            cPressed = Input.GetKey(KeyCode.C);
            if (cPressed) { tutorialPopup.changePopup("Use R to respawn there"); }
        }
        else if(cPressed && !rPressed) //then checks player has pressed r
        {
            rPressed = Input.GetKey(KeyCode.R);
        }
        else if (cPressed && rPressed)
        {
            load(stage);
            stageComplete();
        }
        
    }


    private void stageComplete()
    {
        updateChildColor(completeMaterial);
        untagSection();
        stage++;
        stageControl();
    }

    private void load(int stageToLoad)
    {
        stages[stage].SetActive(true);
    }

    private void updateChildColor(Material color)
    {
        foreach (Transform child in stages[stage].transform)
        {
            updateColor(child, color);
        }
    }

    private void untagSection()
    {
        foreach (Transform child in stages[stage].transform)
        {
            if(child.gameObject.tag != "Wall")
            {
                child.gameObject.tag = "Untagged";
            }
        }
        
    }


    void updateColor(Transform transform, Material color)
    {
        transform.GetComponent<MeshRenderer>().material = color;
    }
   

    private void respawn()
    {
        character.transform.position = respawnPoints[stage];
        if(stage != stages.Length)
        {
            updateChildColor(uncompleteMaterial);
        }
    }

    public void Notify<T>(T t)
    {


        object t1 = (object)t;
        if(t1.GetType() == typeof(string))
        {
            string message = (string)t1;
            if(message == "GemCollected")
            {
                
                stageComplete();
            }
        }
        else
        {
            Collision collision = (Collision)(object)t1;

            if (collision.gameObject.tag == "LightUp")
            {
                updateColor(collision.gameObject.transform, completeMaterial);
            }
            else if (collision.gameObject.tag == "TutorialFloor")
            {
                respawn();
            }
            else if (collision.gameObject.tag == "SectionComplete")
            {
                stageComplete();

                if (stage != 10)
                {
                    load(stage + 1);
                }
            }
            else if (collision.gameObject.tag == "Wall")
            {
                updateColor(collision.gameObject.transform, completeMaterial);
            }
        }

        
        
        
    }

    private IEnumerator loadMainGame()
    {

        yield return new WaitForSeconds(1.5f);
        tutorialPopup.createPopup("");
        BlinderController blind = Instantiate(blindingLight).GetComponent<BlinderController>();
        blind.setBright(true, 2f);
        yield return new WaitForSeconds(2);
        complete("Island");
        blind.setBright(false, 0f);
    }

}
