﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallrunAbility : MonoBehaviour {

    private MovementInfo movementInfo;
    private Rigidbody rigidBody;

    private RaycastHit rcRight;
    private RaycastHit rcLeft;

    private bool leftPress = false;
    private bool rightPress = false;

    private readonly float wallrunDistanceModifier = 3.0f;

    private float wallrunSpeed;
    private float wallRunDistance;
    private float wallRunDistanceDone;

    // Use this for initialization
    void Start () {
        movementInfo = GetComponent<MovementInfo>();
        rigidBody = GetComponent<Rigidbody>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Q))
        {
            leftPress = true;
            rightPress = false;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            leftPress = false;
            rightPress = true;
        }
        else
        {
            leftPress = rightPress = false;
        }
    }


    private void FixedUpdate()
    {
       
        WallRun();
    }

    private void WallRun()
    {
        if (!movementInfo.canWallRun) {return; }

        if (movementInfo.forwardVelocity < (movementInfo.maxSpeed * 0.75)) //player must be at least at 75% of max speed to wall run
        {
            return;
        }

        if (rightPress)
        {
            if (Physics.Raycast(transform.position, transform.right, out rcRight, 1.3f) && rcRight.transform.tag == "Wall") //if player is toutching wall and pressing correct key
            {
                if (!movementInfo.wallRunning) //now we need to check if player has been wall running already
                {
                    initWallRun(); //initialise wall run trajectory
                    continueWallRun();
                }
                else
                {
                    continueWallRun();  //continue on trajectory
                }
            }
            else
            {
                cancelWallRun();
            }
        }
        else if (leftPress)
        {
            if (Physics.Raycast(transform.position, -transform.right, out rcLeft, 1.3f) && rcLeft.transform.tag == "Wall") //if player is toutching wall and pressing correct key
            {
                //now we need to check if player has been wall running already
                if (!movementInfo.wallRunning)
                {
                    //initialise wall run trajectory
                    initWallRun();
                    continueWallRun();
                }
                else
                {
                    //continue on trajectory
                    continueWallRun();
                }
            }
            else
            {
                cancelWallRun();
            }
        }
        else
        {
            cancelWallRun();
        }
    }

    private void initWallRun()
    {

        //here we save the value of the speed starting the wall run to be used later
        wallrunSpeed = movementInfo.forwardVelocity;

        wallRunDistance = wallrunDistanceModifier * wallrunSpeed;
        wallRunDistanceDone = 0;
        movementInfo.wallRunning = true;
        rigidBody.useGravity = false;
        rigidBody.velocity = Vector3.zero;
        tiltCamera();
    }

    private void continueWallRun()
    {
        rigidBody.velocity = Vector3.zero;
        //runs along a sine curve
        wallRunDistanceDone += wallrunSpeed * Time.deltaTime;
        float up = (movementInfo.wallRunHeight * Mathf.Sin(movementInfo.wallRunDistanceModifier * (wallRunDistanceDone / wallRunDistance)));
        transform.Translate(0, up * Time.deltaTime, 0);

        if ((wallRunDistanceDone / wallRunDistance) >= 0.7) { cancelWallRun();}
    }


    private void cancelWallRun()
    {
        
        movementInfo.canWallRun = movementInfo.wallRunning ? false : true;
        if (movementInfo.wallRunning) {normalCameraTilt();}
        movementInfo.wallRunning = false;
        rigidBody.useGravity = true;
    }

    private void tiltCamera()
    {
        if (rightPress) //if wall running on the right tilt camera left
        {
            movementInfo.rotateCamera(15, 0.5f);
        }
        else //if wall running on the left tilt camera right
        {
            movementInfo.rotateCamera(-15, 0.5f);
        }

    }

    private void normalCameraTilt()
    {
        movementInfo.rotateCamera(0, 0.4f);
    }
}
