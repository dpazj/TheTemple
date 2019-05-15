using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetection : MonoBehaviour {

    public float height;
    public float width;
    public float depth;

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
        transform.localScale = new Vector3(width, height, depth);

    }

    private void checkCollision(Vector3 position)
    {
        Vector3 boxPos = transform.position;
        //Create points
        Vector3 p1 = rotatePoint(boxPos + new Vector3(width/2,height/2,depth/2));
        Vector3 p2 = rotatePoint(boxPos + new Vector3(width / 2, height / 2, -depth / 2)); 
        Vector3 p4 = rotatePoint(boxPos + new Vector3(width / 2, -height / 2, -depth / 2));
        Vector3 p5 = rotatePoint(boxPos + new Vector3(-width / 2, height / 2, depth / 2));

        Vector3 u = p2 - p1;
        Vector3 v = p4 - p1;
        Vector3 w = p5 - p1;
        Vector3 x = position - p1;

        float dotu = Vector3.Dot(x, u);
        float dotv = Vector3.Dot(x, v);
        float dotw = Vector3.Dot(x, w);

        //Is point inside the box
        if (dotu > 0 && dotu < Vector3.Dot(u,u) && dotv > 0 && dotv < Vector3.Dot(v, v) && dotw > 0 && dotw < Vector3.Dot(w, w))
        {
            Debug.Log("Collision");
        }

    }

    //https://answers.unity.com/questions/532297/rotate-a-vector-around-a-certain-point.html
    private Vector3 rotatePoint(Vector3 point)
    {
        return Quaternion.Euler(transform.eulerAngles) * (point - transform.position) + transform.position;
    }
}
