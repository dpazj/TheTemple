using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : MonoBehaviour {

    private MovementInfo movementInfo;
    private Rigidbody rigidBody;
    private bool jump = false;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        movementInfo = GetComponent<MovementInfo>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!jump && Input.GetKey(KeyCode.Space))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void Jump()
    {
        if (movementInfo.grounded && jump)
        {

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
        }

        jump = false;
    }
}
