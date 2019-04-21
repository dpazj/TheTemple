using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : LevelManager, IObserver {

    private int gemCount = 0;
    public GameObject gems;
    public GameObject templeRock;
    public GameObject blindingLight;
    MovementInfo movementInfo;

    private bool win = false;


    private void Start()
    {
        //create observers
        initGemObservers();
        character.GetComponent<CollisionNotifier>().setObserver(this);

        movementInfo = character.GetComponent<MovementInfo>();
    }

    private void initGemObservers()
    {
        foreach(Transform child in gems.transform)
        {
            child.GetComponent<Gem>().setObserver(this);
            gemCount++;
        }
    }

    public void Notify<T>(T t) //Notified from a gem collection, or collision with the sea/win zone.
    {
        //This is a little horrible and hacky :/
        string tag = "";
        T t1 = (T)(object)t;
        object test = (object)t1;
        if(test.GetType() != typeof(string))
        {
            Collision collision = (Collision)test;
            tag = collision.gameObject.tag;
        }
        
        
        if(tag == "LevelComplete")
        {
            startWinSequence();
        }
        else if(tag == "Ocean")
        {
            respawn();
        }
        else
        {
            gemCount--;
            checkWinCondition();
        }
        
    }
    private void checkWinCondition()
    {
        if(gemCount == 0)
        {
            toggleRock(true);
        }
    }

    private void toggleRock(bool open)
    {
        float distance = 2;
        if (!open)
        {
            distance = -2;
        }
        templeRock.transform.position = new Vector3(templeRock.transform.position.x, templeRock.transform.position.y, templeRock.transform.position.z + distance);
    }


    public void respawn()
    {
        Vector3 closest = Vector3.zero;
        float smallestDistance = Mathf.Infinity;

        for (int i = 0; i < respawnPoints.Length; i++)
        {
            Vector3 direction = respawnPoints[i] - character.transform.position;
            float distance = direction.sqrMagnitude;
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                closest = respawnPoints[i];
            }
        }

        movementInfo.zeroValues();
        character.transform.position = closest;
    }

    void startWinSequence()
    {
        if (win) { return;}
        win = true;
        toggleRock(false);
        blindingLight = Instantiate(blindingLight);
        StartCoroutine(winSequence());
    }

    IEnumerator winSequence()
    {
        
        foreach (Transform child in gems.transform)
        {
            child.GetComponent<Gem>().updateRotateSpeed(2500, 7f);
            child.GetComponent<Gem>().moveTo(new Vector3(367.05f, 191.2f,64.5f),5f);
            gemCount++;
        }
        yield return new WaitForSeconds(5);
        blindingLight.GetComponent<BlinderController>().setBright(true, 4f);
        yield return new WaitForSeconds(4);
        complete("Menu");
    }
}
