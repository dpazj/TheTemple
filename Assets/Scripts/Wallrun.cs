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
            if(Physics.Raycast(transform.position, transform.right, out m_rcRight, 1) && m_rcRight.transform.tag == "Wall")
            {
                setWallRunning();
            }
            else
            {
                cancelWallRun();
            }
        }
        else if (m_LeftPress)
        {
            if (Physics.Raycast(transform.position, -transform.right, out m_rcLeft, 1) && m_rcLeft.transform.tag == "Wall")
            {
                setWallRunning();
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

    public void setWallRunning()
    {
        m_WallRunning = true;
        transform.Translate(0, 1 * Time.deltaTime, 0);
        m_rb.useGravity = false;
    }

    public void cancelWallRun()
    {
        m_WallRunning = false;
        m_rb.useGravity = true;
    }
}
