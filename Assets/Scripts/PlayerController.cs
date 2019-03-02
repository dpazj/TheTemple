﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float m_MaxSpeed = 8.0f;
    public float m_WalkSpeed = 4.0f;
    public float m_RunModifier = 0.1f;
    public float m_WalkModifier = 0.1f;

    private float m_ForwardVelocity;
    private float m_StraffeVelocity;


    public float m_JumpForce = 70.0f;
    private float m_JumpAirControl = 0.4f; 

    private bool m_Jump;
    private bool m_Jumping;
    private bool m_ForwardJump;
    private bool m_IsGrounded;
    private bool m_Moving;
    

    private Rigidbody m_RigidBody;
    private CapsuleCollider m_Capsule;
    private Camera m_Cam;
    

    //initialization
    private void Start () {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Capsule = GetComponent<CapsuleCollider>();
        m_Cam = GetComponentInChildren<Camera>();
        //prevents rigidbody falling over
        m_RigidBody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        Cursor.lockState = CursorLockMode.Locked;
        m_ForwardVelocity = 0;
        m_StraffeVelocity = 0;
}
	
	private void Update ()
    {
        //Check jump pressed
        if (!m_Jump && Input.GetKey(KeyCode.Space))
        {
            m_Jump = true;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void FixedUpdate()
    {
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        CheckGrounded();
        GetSpeed(h,v);

        if (v == 0){m_ForwardVelocity = 0;}
        if (h == 0 ){m_StraffeVelocity = 0;}

        if (!m_IsGrounded && m_Jumping) {
            h *= m_JumpAirControl;
            if (m_ForwardJump)
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

        if (m_IsGrounded)
        {
            if (m_Jump)
            {
                m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
                m_RigidBody.AddForce(new Vector3(0f, m_JumpForce, 0f), ForceMode.Impulse);
                m_Jumping = true;
                if(m_ForwardVelocity > 1)
                {
                    m_ForwardJump = true;
                }else if(m_ForwardVelocity < 1)
                {
                    m_ForwardJump = false;
                }
                
            }
        }

        m_Jump = false;

    }


    private void GetSpeed(float h, float v){
        if (!m_IsGrounded) { return; }
        bool run = Input.GetKey(KeyCode.LeftShift);

        float forwardVelocity = m_ForwardVelocity;
        float straffeVelocity = m_StraffeVelocity;

        int forwardMultiplier = ((v >= 0) ? 1 : -1);
        
        forwardVelocity += forwardMultiplier * (run ? m_RunModifier : m_WalkModifier);
        straffeVelocity += (run ? m_RunModifier : m_WalkModifier);

        //Prevent player from going over speed forwards/backwards
        if(forwardMultiplier == 1)
        {
            //handles forwards
            if (run && Mathf.Abs(forwardVelocity) > m_MaxSpeed)
            {
                forwardVelocity = m_MaxSpeed;
            }
            else if (!run && Mathf.Abs(forwardVelocity) > m_WalkSpeed)
            {
                forwardVelocity = forwardVelocity - 0.25f;
                if (forwardVelocity < m_WalkSpeed)
                {
                    forwardVelocity = m_WalkSpeed;
                }
            }
        }
        else
        {
            //handles backwards
            if (run && forwardVelocity < (m_MaxSpeed/2*-1))
            {
                forwardVelocity = forwardMultiplier * m_MaxSpeed/2;
            }
            else if (!run && forwardVelocity < (m_WalkSpeed / 2 * -1))
            {
                forwardVelocity = forwardMultiplier * m_WalkSpeed/2;
            }
        }
        
        //Prevent player from going over speed sideways
        if (run && Mathf.Abs(straffeVelocity) > m_MaxSpeed/2)
        {
            straffeVelocity = m_MaxSpeed/2;
        }
        else if (!run && Mathf.Abs(straffeVelocity) > m_WalkSpeed/2)
        {
            straffeVelocity = m_WalkSpeed/2;
        }

        //smooths transition from moving forwards to backwards and vice versa
        if (forwardMultiplier == -1 && forwardVelocity > 0) { forwardVelocity = -0.5f; }
        if (forwardMultiplier == 1 && forwardVelocity < 0) { forwardVelocity = 0.5f; }
        
        m_ForwardVelocity = forwardVelocity;
        m_StraffeVelocity = straffeVelocity;
    }

    private void CheckGrounded() {

        //Modified from "RigidbodyFirstPersonController" GroundCheck() method
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - 0.1f), Vector3.down, out hitInfo, ((m_Capsule.height / 2f) - m_Capsule.radius) + 0.1f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            m_IsGrounded = true;
            m_Jumping = false;
        }
        else
        {
            m_IsGrounded = false;
        }

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
