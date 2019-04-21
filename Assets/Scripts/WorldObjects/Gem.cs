using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, ICollectable {

    IObserver observer;
    public string gemName;
    public float spinSpeed = 100f;

    public Vector3 templeSpot;
    public bool collected = false;
    
    private void Start()
    {
       
    }
    private void Update()
    {
        rotateGem();
    }

    public void collect()
    {
        if (collected) { return; }
        collected = true;
        sendGemToTemple();
        observer.Notify("");
        
    }

    public void setObserver(IObserver obs)
    {
        observer = obs;
    }

    public void updateRotateSpeed(float newSpeed, float time)
    {
        StartCoroutine(changeRotSpeed(newSpeed,time));
    }

    public void moveTo(Vector3 pos, float time)
    {
        StartCoroutine(moveSmooth(transform.position, pos,time));
    }
    
    private void rotateGem()
    {
        transform.Rotate(0,spinSpeed * Time.deltaTime,0);
    }

    public void sendGemToTemple()
    {
        StartCoroutine(toTemple());
    }


    IEnumerator toTemple()
    {
        yield return StartCoroutine(moveSmooth(transform.position,new Vector3(transform.position.x, transform.position.y + 20, transform.position.z), 1f));
        yield return StartCoroutine(moveSmooth(transform.position, templeSpot, 1f));
    }


    IEnumerator moveSmooth(Vector3 start, Vector3 end, float duration)
    {
        
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, counter / duration);
            yield return new WaitForFixedUpdate();
        }
        transform.position = end;

    }

    IEnumerator changeRotSpeed(float desired, float duration)
    {
        float start = spinSpeed;
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            spinSpeed = Mathf.Lerp(start, desired, counter / duration);
            yield return new WaitForFixedUpdate();
        }
        spinSpeed = desired;

    }

}
