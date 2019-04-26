using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : MonoBehaviour {

    private MovementInfo movementInfo;
    private Rigidbody rigidBody;
    private bool jump = false;
    private float coolDown;
 
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        movementInfo = GetComponent<MovementInfo>();
    }
	

	void Update () {
        if (!jump && Input.GetKey(KeyCode.Space))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        if (movementInfo.jumping) { movementInfo.timeJumping += Time.deltaTime;}
        Jump();
    }

    private void Jump()
    {
        if (coolDown > 0)
        {
            coolDown -= Time.fixedDeltaTime; 
            return;
        }

        if (movementInfo.grounded && jump && !movementInfo.jumping)
        {
           /* rigidBody.useGravity = true;
            movementInfo.rotateCamera(0, 0.4f);*/
            rigidBody.velocity = Vector3.zero;//new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
            rigidBody.AddForce(new Vector3(0f, movementInfo.jumpForce, 0f), ForceMode.Impulse);
            movementInfo.jumping = true;
                
            if (movementInfo.forwardVelocity > 1)
            {
                movementInfo.forwardJump = true;
            }
            else if (movementInfo.forwardVelocity < 1)
            {
                movementInfo.forwardJump = false;
            }

            coolDown = movementInfo.jumpCooldown;
        }
        
        jump = false;
    }
}
