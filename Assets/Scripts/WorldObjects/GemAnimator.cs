using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemAnimator : MonoBehaviour {

    public float spinSpeed = 100f;

    private void Update()
    {
        rotateGem();
    }

    private void rotateGem()
    {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0, Space.World);
    }

    public void updateRotateSpeed(float newSpeed, float time)
    {
        StartCoroutine(changeRotSpeed(newSpeed, time));
    }

    public void moveTo(Vector3 pos, float time)
    {
        StartCoroutine(moveSmooth(transform.position, pos, time));
    }

    public IEnumerator moveSmooth(Vector3 start, Vector3 end, float duration)
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

    public IEnumerator changeRotSpeed(float desired, float duration)
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
