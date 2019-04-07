﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInfo : MonoBehaviour {

    public float forwardVelocity { get;  set;}
    public float straffeVelocity { get;  set; }

    public bool grounded { get; private set;}
    public bool wallrunnig { get; set;}
    public bool canWallRun { get;  set; }
    public bool jumping;
    public bool forwardJump;

    //Settings
    public float maxSpeed = 8.0f;
    public float walkSpeed = 4.0f;
    public float runModifier = 0.1f;
    public float walkModifier = 0.1f;

    public float jumpForce = 70.0f;
    public float jumpAirControl = 0.4f;


    //Observes these classes
    PlayerController pc;
    WallrunAbility wallrun;
    CapsuleCollider capsule;
    

    // Use this for initialization
    void Start () {
        pc = GetComponent<PlayerController>();
        wallrun = GetComponent<WallrunAbility>();
        capsule = GetComponent<CapsuleCollider>();
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
            canWallRun = true;
        }
        else
        {
            grounded = false;
        }
    }


    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 100), "Forward Velocity: " + forwardVelocity.ToString());
        GUI.Label(new Rect(0, 10, 200, 100), "Straffe Velocity: " + straffeVelocity.ToString());
        GUI.Label(new Rect(0, 20, 100, 100), Input.GetAxis("Horizontal").ToString());
        GUI.Label(new Rect(0, 30, 100, 100), Input.GetAxis("Vertical").ToString());
    }
}
