using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionNotifier : MonoBehaviour {

    public IObserver callback;

    // Use this for initialization
    private void OnCollisionEnter(Collision collision)
    {
        if(callback == null) { Debug.Log("Level Observer NotSet"); return;}
        callback.Notify(collision);
    }

    public void setObserver(IObserver cb)
    {
        callback = cb;
    }
}
