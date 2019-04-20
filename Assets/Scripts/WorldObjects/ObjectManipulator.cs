using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManipulator : MonoBehaviour {

    private int animationsRunning = 0;

    public float getXPos()
    {
        return transform.localPosition.x;
    }
    public float getYPos()
    {
        return transform.localPosition.y;
    }
    public float getZPos()
    {
        return transform.localPosition.z;
    }

    public void scale(float newSize)
    {

        transform.localScale *= newSize;
    }

    public void rotate(Vector3 rota)
    {
        transform.Rotate(rota);
    }

    public void move(Vector3 dest)
    {
        transform.localPosition = dest;
    }


    public void moveSmooth(Vector3 dest, float time)
    {
        StartCoroutine(moveSmooth(transform.localPosition, dest, time));
    }

    public void setPosition(Vector3 dest)
    {
        transform.localPosition = dest;
    }

    IEnumerator moveSmooth(Vector3 start, Vector3 end, float duration)
    {
        animationsRunning++;
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(start, end, counter / duration);
            yield return new WaitForFixedUpdate();
        }
        transform.localPosition = end;
        animationsRunning--;
    }
    public void rotateSmooth(Vector3 direction, float angle, float time)
    {
        StartCoroutine(smoothRotate(direction, angle, time));
    }
    IEnumerator smoothRotate(Vector3 axis, float angle, float duration)
    {
        Quaternion target = transform.localRotation * Quaternion.Euler(axis * angle);
        Quaternion start = transform.localRotation;
        animationsRunning++;
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(start, target, counter / duration);
            yield return new WaitForFixedUpdate();
        }

        transform.localRotation = target;
        animationsRunning--;
    }

    public void zoomSmooth(float scale, float time)
    {
        StartCoroutine(smoothZoom(scale, time));
    }

    IEnumerator smoothZoom(float scale, float duration)
    {
        Vector3 target = transform.localScale * scale;
        Vector3 start = transform.localScale;
        float counter = 0;
        animationsRunning++;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            transform.localScale = Vector3.Slerp(start, target, counter / duration);
            yield return new WaitForFixedUpdate();
        }

        transform.localScale = target;
        animationsRunning--;
    }

    public bool animationRunning()
    {
        return animationsRunning != 0 ? true : false;
    }

}
