using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGem : GemAnimator, ICollectable{

    private List<IObserver> observers = new List<IObserver>();

    public void collect()
    {
        gameObject.SetActive(false);

        foreach (IObserver obs in observers)
        {
            obs.Notify("GemCollected");
        }
    }

    public void setObserver(IObserver observer)
    {
        observers.Add(observer);
    }
}
