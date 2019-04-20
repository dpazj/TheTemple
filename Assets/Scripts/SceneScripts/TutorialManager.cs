using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : LevelManager, IObserver {


    public Material completeMaterial;
    public Material uncompleteMaterial;
    public GameObject[] stages;
    private int stage = 0;


    //Movement
    float moveDistance = 0;
    public float requiredDistance = 150f;
    

    private void Start()
    {
        base.character.GetComponent<TutorialBehaviour>().setObserver(this);
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
                //TODO show info
                break;
            case 1:
                //Show jump info
                break;
            case 2:
                //Show momentum info (hold shift to sprint)
                break;
            case 3:
                //Show straffe jump info
                break;
            case 4:
                //Show right wall run info
                break;
            case 5:
                //Show left wall run info
                break;
            case 6:
                //Wallrun round corner
                break;
            case 7:
                //Walljump info
                break;
            case 8:
                //Wallrun + wall jump;
                break;
            case 9:
                //winner winner chicken dinner!!
                stage++;
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
        else
        {
            Debug.Log("COMPLETE");
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
