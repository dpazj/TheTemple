using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    [SerializeField]
    GameObject[] gameObjects;

    public GameObject[] getObjects()
    {
        return gameObjects;
    }
           
}
