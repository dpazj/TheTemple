using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorBehaviour : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (collision.gameObject.GetComponent<ICollectable>() != null)
        {
            collision.gameObject.GetComponent<ICollectable>().collect();
        }
    }
}
