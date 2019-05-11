using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderDetection : MonoBehaviour {

    public float height;
    public float radius;

    CollisionManager cm;
    public GameObject[] gameObjects;
    public void Start()
    {

        cm = GameObject.Find("CollisionManager").GetComponent<CollisionManager>();
        gameObjects = cm.getObjects();

        setSize();
    }

    void Update()
    {
        foreach (GameObject obj in gameObjects)
        {
            checkCollision(obj.transform.position);
        }

    }

    private void setSize()
    {
        transform.localScale = new Vector3(radius, height, radius);

    }

    private void checkCollision(Vector3 position)
    {
        Vector3 cylinderPos = transform.position;

        float unrooted = Mathf.Pow((position.x - cylinderPos.x), 2) + Mathf.Pow((position.z - cylinderPos.z), 2);
        float distance = Mathf.Sqrt(unrooted);

        float top = cylinderPos.y + (radius * 0.5f);
        float bottom = cylinderPos.y - (radius * 0.5f);

        if (distance < radius && position.y > bottom && position.y < top)
        {
            Debug.Log("Collision" + Time.deltaTime);
        }

    }
}
