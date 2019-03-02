using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrun : MonoBehaviour {

    // Use this for initialization
    private Rigidbody m_rb;
    private PlayerController m_pc;
    private RaycastHit m_rcRight;
    private RaycastHit m_rcLeft;


    private bool m_WallRunning = false;
    private bool m_LeftPress = false;
    private bool m_RightPress = false;

    public float wallrunHeight = 1.0f;
    public float wallrunDistanceModifier = 10.0f;
    private float wallrunSpeed;
    private float wallRunDistance;
    private float wallRunDistanceDone;
    
    
    void Start () {
		m_rb = GetComponent<Rigidbody>();
        m_pc = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            m_LeftPress = true;
            m_RightPress = false;
        }else if (Input.GetKey(KeyCode.E))
        {
            m_LeftPress = false;
            m_RightPress = true;
        }
        else
        {
            m_LeftPress = m_RightPress = false;
        }

	}

    void FixedUpdate()
    {

        if (m_RightPress)
        {
            if(Physics.Raycast(transform.position, transform.right, out m_rcRight, 1) && m_rcRight.transform.tag == "Wall") //if player is toutching wall and pressing correct key
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
        /*
        else if (m_LeftPress)
        {
            if (Physics.Raycast(transform.position, -transform.right, out m_rcLeft, 1) && m_rcLeft.transform.tag == "Wall")
            {
                
            }
            else
            {
                cancelWallRun();
            }
        }*/
        else
        {
            cancelWallRun();
        }
        
        

    }

    private void initWallRun()
    {
         //here we save the value of the speed starting the wall run to be used later
        wallrunSpeed = m_pc.GetForwardVelocity();
        wallRunDistance = wallrunDistanceModifier * wallrunSpeed;
        wallRunDistanceDone = 0;
        m_WallRunning = true;
        m_rb.useGravity = false;
    }

    private void continueWallRun()
    {
        wallRunDistanceDone += wallrunSpeed * Time.deltaTime;
        float up = 1.5f * Mathf.Sin(2 * (wallRunDistanceDone/wallRunDistance));
        Debug.Log(up);
        transform.Translate(0, up * Time.deltaTime, 0);
        /*else
        {
            
        }
        */
    }

    private void cancelWallRun()
    {
        m_WallRunning = false;
        m_rb.useGravity = true;
    }

   
}
