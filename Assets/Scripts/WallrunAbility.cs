using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallrunAbility : MonoBehaviour {

    private MovementInfo movementInfo;
    private PlayerCamController m_PlayerCam;
    private Rigidbody m_RigidBody;

    private RaycastHit m_rcRight;
    private RaycastHit m_rcLeft;


    private bool m_WallRunning = false;
    private bool m_LeftPress = false;
    private bool m_RightPress = false;

    private readonly float wallrunDistanceModifier = 3.0f;
    private float wallRunHeight;

    private float wallrunSpeed;
    private float wallRunDistance;
    private float wallRunDistanceDone;
    private float cameraRotate = 0;

    // Use this for initialization
    void Start () {
        movementInfo = GetComponentInChildren<MovementInfo>();
        m_RigidBody = GetComponent<Rigidbody>();
        m_PlayerCam = GetComponentInChildren<PlayerCamController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Q))
        {
            m_LeftPress = true;
            m_RightPress = false;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            m_LeftPress = false;
            m_RightPress = true;
        }
        else
        {
            m_LeftPress = m_RightPress = false;
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

        if (m_RightPress)
        {
            if (Physics.Raycast(transform.position, transform.right, out m_rcRight, 1) && m_rcRight.transform.tag == "Wall") //if player is toutching wall and pressing correct key
            {
                if (!m_WallRunning) //now we need to check if player has been wall running already
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
        else if (m_LeftPress)
        {
            if (Physics.Raycast(transform.position, -transform.right, out m_rcLeft, 1) && m_rcLeft.transform.tag == "Wall") //if player is toutching wall and pressing correct key
            {
                //now we need to check if player has been wall running already
                if (!m_WallRunning)
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
        wallRunHeight = 2.75f * (wallrunSpeed / movementInfo.maxSpeed);

        wallRunDistance = wallrunDistanceModifier * wallrunSpeed;
        wallRunDistanceDone = 0;
        m_WallRunning = true;
        m_RigidBody.useGravity = false;
        m_RigidBody.velocity = Vector3.zero;
        tiltCamera();
    }

    private void continueWallRun()
    {
        //runs along a sine curve
        wallRunDistanceDone += wallrunSpeed * Time.deltaTime;
        float up = (2.75f * Mathf.Sin(8f * (wallRunDistanceDone / wallRunDistance)));
        transform.Translate(0, up * Time.deltaTime, 0);

        if ((wallRunDistanceDone / wallRunDistance) >= 0.7) { cancelWallRun(); }
    }


    private void cancelWallRun()
    {
        movementInfo.canWallRun = m_WallRunning ? false : true;
        if (m_WallRunning)
        {
            normalCameraTilt();
        }
        m_WallRunning = false;
        m_RigidBody.useGravity = true;

        
    }

    private void tiltCamera()
    {
        if (m_RightPress) //if wall running on the right tilt camera left
        {
            m_PlayerCam.setCameraRotation(15, 0.5f);
        }
        else //if wall running on the left tilt camera right
        {
            m_PlayerCam.setCameraRotation(-15, 0.5f);
        }

    }

    private void normalCameraTilt()
    {
        Debug.Log("Reset");
        m_PlayerCam.setCameraRotation(0, 1f);
    }
}
