using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, ICollectable {

    IObserver observer;
    GemAnimator animator;
    public string gemName;
    public void collect()
    {
        gameObject.SetActive(false);
        observer.Notify(gemName);
    }

    public void setObserver(IObserver obs)
    {
        observer = obs;
    }

    
}
