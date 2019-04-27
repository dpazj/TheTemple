using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

     
    private Rigidbody m_RigidBody;
    private MovementInfo movementInfo;

    private void Start () {
        m_RigidBody = GetComponent<Rigidbody>();
              
        movementInfo = GetComponentInChildren<MovementInfo>();
        //prevents rigidbody falling over
        m_RigidBody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        Cursor.lockState = CursorLockMode.Locked;

        movementInfo.forwardVelocity = 0;
        movementInfo.straffeVelocity = 0;

    }

    private void FixedUpdate()
    {
        if (movementInfo.paused) { return; }
        Movement();
    }

  

    private void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        GetSpeed(h, v);

        if (v == 0) { movementInfo.forwardVelocity = 0; }
        if (h == 0) { movementInfo.straffeVelocity = 0; }

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

        h *= Time.fixedDeltaTime;
        v *= Time.fixedDeltaTime;

        
        transform.Translate(h * Mathf.Abs(movementInfo.straffeVelocity), 0, v * Mathf.Abs(movementInfo.forwardVelocity));
    }

   


    private void GetSpeed(float h, float v){
        if (!movementInfo.grounded) { return; }
        if (!movementInfo.canWalk)  //If player is trying to go up a hill, reduce their speed;
        {
            movementInfo.forwardVelocity -= movementInfo.steepSpeedReduction;
            movementInfo.straffeVelocity -= movementInfo.steepSpeedReduction;
        }
        else
        {


            bool run = Input.GetKey(KeyCode.LeftShift);

            float forwardVelocity = movementInfo.forwardVelocity;
            float straffeVelocity = movementInfo.straffeVelocity;

            int forwardMultiplier = ((v >= 0) ? 1 : -1);

            forwardVelocity += forwardMultiplier * (run ? movementInfo.runModifier : movementInfo.walkModifier);
            straffeVelocity += (run ? movementInfo.runModifier : movementInfo.walkModifier);

            //Prevent player from going over speed forwards/backwards
            if (forwardMultiplier == 1)
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
                if (run && forwardVelocity < (movementInfo.maxSpeed * -1))
                {
                    forwardVelocity = forwardMultiplier * movementInfo.maxSpeed;
                }
                else if (!run && forwardVelocity < (movementInfo.walkSpeed * -1))
                {
                    forwardVelocity = forwardMultiplier * movementInfo.walkSpeed;
                }
            }

            //Prevent player from going over speed sideways
            if (run && Mathf.Abs(straffeVelocity) > movementInfo.maxSpeed / 2)
            {
                straffeVelocity = movementInfo.maxSpeed / 2;
            }
            else if (!run && Mathf.Abs(straffeVelocity) > movementInfo.walkSpeed)
            {
                straffeVelocity = movementInfo.walkSpeed;
            }

            //smooths transition from moving forwards to backwards and vice versa
            if (forwardMultiplier == -1 && forwardVelocity > 0) { forwardVelocity = -0.5f; }
            if (forwardMultiplier == 1 && forwardVelocity < 0) { forwardVelocity = 0.5f; }



            movementInfo.forwardVelocity = forwardVelocity;
            movementInfo.straffeVelocity = straffeVelocity;
        }
    }
}
