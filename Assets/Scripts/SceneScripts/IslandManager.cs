using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : LevelManager, IObserver {

    private int gemCount = 0;
    private int collectedGems = 0;
    public GameObject gems;
    public GameObject templeRock;
    public GameObject blindingLight;
    public GameObject messagePopup;
    private TutorialPopupControl popup;


    MovementInfo movementInfo;

    private bool win = false;

    void Start()
    {
        initIsland(); //remove in build
    }
    public void initIsland()
    {
        //create observers
        initGemObservers();
        character.GetComponent<CollisionNotifier>().setObserver(this);
        movementInfo = character.GetComponent<MovementInfo>();
        popup = messagePopup.GetComponent<TutorialPopupControl>();
        popup.createTempPopup("Find and return the five lost gems to the temple",4f);

    }

    private void initGemObservers()
    {
        foreach(Transform child in gems.transform)
        {

            Gem gem = child.Find("Gem").GetComponent<Gem>();
            gem.setObserver(this);
            foreach (Transform shard in child.Find("Shards"))
            {
                gem.setObserver(shard.GetComponent<GemShard>());
            }
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

            if (tag == "LevelComplete")
            {
                startWinSequence();
            }
            else if (tag == "Ocean")
            {
                respawn();
            }
        }else
        {
            collectedGems++;
            checkWinCondition();
            
        }
        
    }
    private void checkWinCondition()
    {

        if (collectedGems == gemCount)
        {
            toggleRock(true);
            popup.createTempPopup("All gems found! Head to the temple", 3f);
        }
        else
        {
            popup.createTempPopup(collectedGems + "/" + gemCount, 2f);
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
