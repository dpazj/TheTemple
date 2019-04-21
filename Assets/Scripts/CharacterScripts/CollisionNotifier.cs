using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionNotifier : MonoBehaviour {

    public IObserver callback;

    // Use this for initialization
    private void OnCollisionEnter(Collision collision)
    {
        callback.Notify(collision);
    }

    public void setObserver(IObserver cb)
    {
        callback = cb;
    }
}
