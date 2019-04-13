using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInfo : MonoBehaviour {

    [System.NonSerialized]
    public float forwardVelocity;
    [System.NonSerialized]
    public float straffeVelocity;
    public bool grounded;
    [System.NonSerialized]
    public bool wallRunning;
    [System.NonSerialized]
    public bool canWallRun;
    [System.NonSerialized]
    public bool jumping;
    [System.NonSerialized]
    public bool forwardJump;
    [System.NonSerialized]
    public bool cameraFinishedRotating;



    //Movement Settings
    public float maxSpeed = 8.0f;
    public float walkSpeed = 4.0f;
    public float runModifier = 0.1f;
    public float walkModifier = 0.1f;

    //Jump Settings
    public float jumpForce = 70.0f;
    public float jumpAirControl = 0.4f;
    public float minJumpTime = 0.5f;
    [System.NonSerialized]
    public float timeJumping = 0;

    //Wall run Settings
    public float wallRunHeight = 2.5f;
    public float wallRunDistanceModifier = 8f;
    [System.NonSerialized]
    public bool mustSwapWallrunSide;

    //Wall jump settings
    public float wallJumpCoolDown = 0.3f;
    public int wallJumpWithoutReset = 2;
    [System.NonSerialized]
    public int wallJumpCounter = 0;


    //Observes these classes
    CapsuleCollider capsule;
    PlayerCamController cameraController;
    

    // Use this for initialization
    void Start () {
        capsule = GetComponent<CapsuleCollider>();
        cameraController = transform.Find("Camera").GetComponent<PlayerCamController>();
        wallRunning = false;
	}
 
    // Update is called once per frame
    void FixedUpdate () {
        CheckGrounded();
	}

    private void CheckGrounded()
    {
        //Modified from "RigidbodyFirstPersonController" GroundCheck() method
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, capsule.radius * (1.0f - 0.1f), Vector3.down, out hitInfo, ((capsule.height / 2f) - capsule.radius) + 0.1f, Physics.AllLayers, QueryTriggerInteraction.Ignore)) //Check if player is toutching ground
        {
            grounded = true;
            jumping = false;
            mustSwapWallrunSide = false;
            canWallRun = true;
            resetCounters();
        }
        else
        {
            grounded = false;
        }
    }

    private void resetCounters()
    {
        timeJumping = 0;
        resetWallJumpCounter();
    }

    public void resetWallJumpCounter()
    {
        wallJumpCounter = 0;
    }

    public void rotateCamera(float rot, float time)
    {
        cameraController.setCameraRotation(rot, time);
    }

    

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 100), "Forward Velocity: " + forwardVelocity.ToString());
        GUI.Label(new Rect(0, 10, 200, 100), "Straffe Velocity: " + straffeVelocity.ToString());
        GUI.Label(new Rect(0, 20, 100, 100), Input.GetAxis("Horizontal").ToString());
        GUI.Label(new Rect(0, 30, 100, 100), Input.GetAxis("Vertical").ToString());

    }
}
