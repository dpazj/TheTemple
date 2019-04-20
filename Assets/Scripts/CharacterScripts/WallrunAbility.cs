using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallrunAbility : MonoBehaviour {

    private MovementInfo movementInfo;
    private Rigidbody rigidBody;

    private RaycastHit rcRight;
    private RaycastHit rcLeft;

    private bool leftPress = false;
    private bool rightPress = false;

    private enum LastWallSide { Right, Left};
    private int lastSide;

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
            movementInfo.attemptingWallrun = true;
            leftPress = true;
            rightPress = false;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            movementInfo.attemptingWallrun = true;
            leftPress = false;
            rightPress = true;
        }
        else
        {
            movementInfo.attemptingWallrun = leftPress = rightPress = false;
        }
    }


    private void FixedUpdate()
    {
        
        CheckWallRun();
    }

    private void CheckWallRun()
    {
        if (!movementInfo.canWallRun || movementInfo.forwardVelocity < (movementInfo.maxSpeed * 0.75)) {return; } //player must be at least at 75% of max speed to wall run

        if (!CheckSideValid())
        {
            //cancelWallRun();
            return;
        }

        if (rightPress)
        {
            WallRunExecute(transform.right);
        }
        else if (leftPress)
        {
            WallRunExecute(-transform.right); 
        }
        else
        {
            cancelWallRun();
        }
    }

    private bool CheckSideValid()
    {
        if (movementInfo.mustSwapWallrunSide)
        {
            if((leftPress && lastSide == (int)LastWallSide.Left) || (rightPress && lastSide == (int)LastWallSide.Right))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    private void WallRunExecute(Vector3 dir)
    {
        if (Physics.Raycast(transform.position, dir, out rcLeft, 1.3f) && rcLeft.transform.tag == "Wall") //if player is toutching wall and pressing correct key
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

    private void initWallRun()
    {
        if (leftPress) { lastSide = (int)LastWallSide.Left; }
        else if (rightPress){lastSide = (int)LastWallSide.Right;}
        movementInfo.mustSwapWallrunSide = false;
        movementInfo.resetWallJumpCounter();
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
        if ((wallRunDistanceDone / wallRunDistance) > 0.45f) { cancelWallRun();}

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
