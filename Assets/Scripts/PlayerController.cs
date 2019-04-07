using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    private float m_ForwardVelocity;
    private float m_StraffeVelocity;

    private bool m_Jump;
    
    private bool m_Moving;
    
    //Components 
    private Rigidbody m_RigidBody;
    private Camera m_Cam;
  
    private MovementInfo movementInfo;

    //initialization
    private void Start () {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Cam = GetComponentInChildren<Camera>();
       
        movementInfo = GetComponentInChildren<MovementInfo>();
        //prevents rigidbody falling over
        m_RigidBody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        Cursor.lockState = CursorLockMode.Locked;

        m_ForwardVelocity = 0;
        m_StraffeVelocity = 0;

    }
	
	private void Update ()
    {
        /*/Check jump pressed
        if (!m_Jump && Input.GetKey(KeyCode.Space))
        {
            m_Jump = false;
        }*/

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        GetSpeed(h, v);

        if (v == 0) { m_ForwardVelocity = 0; }
        if (h == 0) { m_StraffeVelocity = 0; }

        if (!movementInfo.grounded && movementInfo.jumping)
        {
            h *= movementInfo.jumpAirControl;
            if (movementInfo.forwardJump)
            {
                v = 1;
            }
            else
            {
                v = -1;
            }
        }

        h *= Time.deltaTime;
        v *= Time.deltaTime;
        transform.Translate(h * Mathf.Abs(m_StraffeVelocity), 0, v * Mathf.Abs(m_ForwardVelocity));

        /*
        if (movementInfo.grounded)
        {
            if (m_Jump)
            {
                m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
                m_RigidBody.AddForce(new Vector3(0f, m_JumpForce, 0f), ForceMode.Impulse);
                movementInfo.jumping = true;
                if (m_ForwardVelocity > 1)
                {
                    m_ForwardJump = true;
                }
                else if (m_ForwardVelocity < 1)
                {
                    m_ForwardJump = false;
                }

            }
        }

        m_Jump = false;
        */
    }

   


    private void GetSpeed(float h, float v){
        if (!movementInfo.grounded) { return; }
        bool run = Input.GetKey(KeyCode.LeftShift);

        float forwardVelocity = m_ForwardVelocity;
        float straffeVelocity = m_StraffeVelocity;

        int forwardMultiplier = ((v >= 0) ? 1 : -1);
        
        forwardVelocity += forwardMultiplier * (run ? movementInfo.runModifier : movementInfo.walkModifier);
        straffeVelocity += (run ? movementInfo.runModifier : movementInfo.walkModifier);

        //Prevent player from going over speed forwards/backwards
        if(forwardMultiplier == 1)
        {
            //handles forwards
            if (run && Mathf.Abs(forwardVelocity) > movementInfo.maxSpeed)
            {
                forwardVelocity = movementInfo.maxSpeed;
            }
            else if (!run && Mathf.Abs(forwardVelocity) > movementInfo.walkSpeed)
            {
                forwardVelocity = forwardVelocity - 0.25f;
                if (forwardVelocity < movementInfo.walkSpeed)
                {
                    forwardVelocity = movementInfo.walkSpeed;
                }
            }
        }
        else
        {
            //handles backwards
            if (run && forwardVelocity < (movementInfo.maxSpeed / 2*-1))
            {
                forwardVelocity = forwardMultiplier * movementInfo.maxSpeed / 2;
            }
            else if (!run && forwardVelocity < (movementInfo.walkSpeed / 2 * -1))
            {
                forwardVelocity = forwardMultiplier * movementInfo.walkSpeed / 2;
            }
        }
        
        //Prevent player from going over speed sideways
        if (run && Mathf.Abs(straffeVelocity) > movementInfo.maxSpeed / 2)
        {
            straffeVelocity = movementInfo.maxSpeed / 2;
        }
        else if (!run && Mathf.Abs(straffeVelocity) > movementInfo.walkSpeed / 2)
        {
            straffeVelocity = movementInfo.walkSpeed / 2;
        }

        //smooths transition from moving forwards to backwards and vice versa
        if (forwardMultiplier == -1 && forwardVelocity > 0) { forwardVelocity = -0.5f; }
        if (forwardMultiplier == 1 && forwardVelocity < 0) { forwardVelocity = 0.5f; }
        
        m_ForwardVelocity = forwardVelocity;
        m_StraffeVelocity = straffeVelocity;
    }


    public float GetForwardVelocity()
    {
        return this.m_ForwardVelocity;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 100), "Forward Velocity: " + m_ForwardVelocity.ToString());
        GUI.Label(new Rect(0, 10, 200, 100), "Straffe Velocity: " + m_StraffeVelocity.ToString());
        GUI.Label(new Rect(0, 20, 100, 100), Input.GetAxis("Horizontal").ToString());
        GUI.Label(new Rect(0, 30, 100, 100), Input.GetAxis("Vertical").ToString());
        GUI.Label(new Rect(0, 40, 100, 100), m_Cam.transform.eulerAngles.y.ToString());
    }
        
    
}
