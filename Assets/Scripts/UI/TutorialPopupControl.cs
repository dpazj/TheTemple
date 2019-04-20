using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TutorialPopupControl : MonoBehaviour {

    TextMeshProUGUI textMesh;
    bool popup = false;
    
	
	void Awake () {
        textMesh = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0);
        
    }

    private void setText(string text)
    {
        textMesh.text = text;
    }

    private void fadeTextIn()
    {
        StartCoroutine(fadeText(true, 0.5f));
    }

    private void fadeTextOut()
    {
        StartCoroutine(fadeText(false, 0.5f));
    }

    public void createPopup(string text)
    {
       setText(text);
       fadeTextIn();
    }

    public void changePopup(string text)
    {
        StartCoroutine(change(text));
    }

    private IEnumerator change(string text)
    {
        fadeTextOut();
        yield return new WaitForSeconds(0.5f);
        setText(text);
        fadeTextIn();
    }

    IEnumerator fadeText(bool fadeIn, float time)
    {
        Color color = textMesh.color;
        float count = 0;

        float desired = 0;
        if (fadeIn)
        {
            desired = 1f;
        }

        while(count < time)
        {
            count += Time.deltaTime;
            
            float alpha = Mathf.Lerp(color.a, desired, count / time);
            textMesh.color = new Color(color.r, color.g, color.b, alpha);
            yield return new WaitForFixedUpdate();
        }

        color = new Color(color.r, color.g, color.b, desired);
    }

}
