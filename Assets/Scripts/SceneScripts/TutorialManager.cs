﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : LevelManager, IObserver {

    public GameObject tutorialMessages;
    public Material completeMaterial;
    public Material uncompleteMaterial;
    public GameObject[] stages;
    
    private int stage = 0;
    private TutorialPopupControl tutorialPopup;


    //Movement
    float moveDistance = 0;
    public float requiredDistance = 150f;
    

    private void Start()
    {
        
        tutorialPopup = Instantiate(tutorialMessages).GetComponent<TutorialPopupControl>();
        base.character.GetComponent<TutorialBehaviour>().setObserver(this);
        stageControl();
    }

    void Update()
    {
        if(stage == 0)
        {
            teachMovement();
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
                tutorialPopup.changePopup("Use the mouse to change direction mid jump. You may need to get some speed up for this one");
                break;
            case 4:
                tutorialPopup.changePopup("Hold E to wall run on the right");
                break;
            case 5:
                tutorialPopup.changePopup("Hold Q to wall run on the left");
                break;
            case 6:
                tutorialPopup.changePopup("You can wall run round corners!");
                break;
            case 7:
                tutorialPopup.changePopup("While jumping and toutching a wall, press space and move the mouse in the direction you wish to wall jump to");
                break;
            case 8:
                tutorialPopup.changePopup("While wall running, press space to launch yourself off the wall");
                break;
            case 9:
                tutorialPopup.changePopup("Tutorial Complete");
                StartCoroutine(loadMainGame());
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
        }
    }

   
    private void stageComplete()
    {
        updateChildColor(completeMaterial);
        untagSection();
        if (stage + 1 < stages.Length)
        {
            loadNext();
        }
                
        stage++;
        stageControl();
    }

    private void loadNext()
    {
        stages[stage + 1].SetActive(true);
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
        base.character.transform.position = base.respawnPoints[stage];
        if(stage != stages.Length)
        {
            updateChildColor(uncompleteMaterial);
        }
    }

    public void Notify<T>(T t)
    {
        
        T t1 = (T)(object)t;
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
        }
        else if (collision.gameObject.tag == "Wall")
        {
            updateColor(collision.gameObject.transform, completeMaterial);
        }
    }

    private IEnumerator loadMainGame()
    {
        yield return new WaitForSeconds(4);
        base.complete();
    }

}
