using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorBehaviour : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Gem" || tag == "GemTrail")
        {
            collision.gameObject.GetComponent<ICollectable>().collect();
        }
    }
}
