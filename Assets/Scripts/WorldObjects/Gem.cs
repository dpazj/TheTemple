using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : GemAnimator, ICollectable {

    List<IObserver> observers = new List<IObserver>();
    public string gemName;
    

    public Vector3 templeSpot;
    public bool collected = false;
     

    public void collect()
    {
        if (collected) { return; }
        collected = true;

        sendGemToTemple();

        foreach(IObserver obs in observers)
        {
            obs.Notify("");
        }
    }

    public void setObserver(IObserver obs)
    {
        observers.Add(obs);
    }

    
    public void sendGemToTemple()
    {
        StartCoroutine(toTemple());
    }


    IEnumerator toTemple()
    {
        yield return StartCoroutine(moveSmooth(transform.position,new Vector3(transform.position.x, transform.position.y + 20, transform.position.z), 5f));
        yield return StartCoroutine(moveSmooth(transform.position, templeSpot, 5f));
    }



}
