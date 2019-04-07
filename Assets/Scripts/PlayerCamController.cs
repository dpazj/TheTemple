using System.Collections;
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

	// Use this for initialization
	void Start () {
        character = this.transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);

        mouseLook += smoothV;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        Debug.Log(rotation);
        transform.Rotate(0, 0, rotation);

    }

    public void setCameraRotation(float rot, float time)
    {
        //rotation = rot;
        //if (rotating) { return; }
        StartCoroutine(smoothRot(rot, time));
    }

    IEnumerator smoothRot(float rot, float time)
    {
        rotating = true;
        float counter = 0;
        float startRot = rotation;
        while(counter < time)
        {
            counter += Time.deltaTime;
            if(rot > rotation)
            {
                rotation += (Mathf.Abs(rotation - rot) * (counter / time));
            }
            else
            {
                rotation -= (Mathf.Abs(rotation - rot) * (counter / time));
            }
            
            yield return new WaitForFixedUpdate();
        }
        rotation = rot;
        rotating = false;

    }

    
  

}
