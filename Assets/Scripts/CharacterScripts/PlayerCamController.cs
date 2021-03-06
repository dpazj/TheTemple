﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamController : MonoBehaviour {

    Vector2 mouseLook;
    Vector2 smoothV;

    public float sensitivity = 2.0f;
    public float smoothing = 2.0f;

    private float rotation;
    private bool rotating;

    GameObject character;
    private IEnumerator rotateCoroutine;

	// Use this for initialization
	void Start () {
        character = this.transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (character.GetComponent<MovementInfo>().paused) { return; }
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2((sensitivity * 0.5f) * smoothing, (sensitivity * 0.5f) * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);

        mouseLook += smoothV;

        if(mouseLook.y > 80f)
        {
            mouseLook.y = 80f;
        }
        else if(mouseLook.y < -70f)
        {
            mouseLook.y = -70f;
        }
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        
        transform.Rotate(0, 0, rotation);

    }

    public void setCameraRotation(float rot, float time)
    {
        if(rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
        }
        
        if (rot == rotation) { return;}
        rotateCoroutine = smoothRot(rot, time);
        StartCoroutine(rotateCoroutine);
    }

    IEnumerator smoothRot(float rot, float time)
    {
        float counter = 0;

        while(counter < time)
        {
            
            counter += Time.deltaTime;
            if(rot < rotation)
            {
                rotation -= (Mathf.Abs(rotation - rot) * (counter / time));
            }
            else
            {
                rotation += (Mathf.Abs(rotation - rot) * (counter / time));
            }
            
            yield return new WaitForFixedUpdate();
        }
        rotation = rot;
    }

    
  

}
