using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemShard : GemAnimator, ICollectable, IObserver{
    public void collect()
    {
        gameObject.SetActive(false);
    }

    public void Notify<T>(T t) //This is called when the main gem is collected
    {
        gameObject.SetActive(false);
    }

   
}
