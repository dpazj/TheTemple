using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinderController : MonoBehaviour {

    Image imgLight;

    void Start()
    {
        imgLight = transform.GetChild(0).GetComponent<Image>();
    }

    public void setBright(bool bright, float time)
    {
        StartCoroutine(lightUpScreen(bright, time));
    }

    public void changeAlpha(float desiredValue)
    {
        imgLight.color = new Color(imgLight.color.r, imgLight.color.g, imgLight.color.b, desiredValue);
    }

    IEnumerator lightUpScreen(bool fadeIn, float time)
    {
        Color color = imgLight.color;
        float count = 0;

        float desired = 0;
        if (fadeIn)
        {
            desired = 1f;
        }

        while (count < time)
        {
            count += Time.deltaTime;

            float alpha = Mathf.Lerp(color.a, desired, count / time);
            changeAlpha(alpha);
            yield return new WaitForFixedUpdate();
        }

        changeAlpha(desired);
    }
}
