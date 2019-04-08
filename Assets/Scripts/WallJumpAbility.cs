using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpAbility : MonoBehaviour {

    private MovementInfo movementInfo;
    private PlayerCamController playerCam;
    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;

    private bool wallJump = false;

    private RaycastHit rayhit;


    void Start () {
        movementInfo = GetComponent<MovementInfo>();
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerCam = GetComponentInChildren<PlayerCamController>();
    }
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && !wallJump && !movementInfo.grounded && (movementInfo.jumping || movementInfo.wallRunning)) //Player has to be jumping/wall running
        {
            wallJump = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (wallJump)
        {
            if (movementInfo.wallRunning)
            {
                if (Physics.Raycast(transform.position, transform.right, out rayhit, 1) && rayhit.transform.tag == "Wall") //raycast right
                {
                    wallRunJump();
                }
                else if(Physics.Raycast(transform.position, -transform.right, out rayhit, 1) && rayhit.transform.tag == "Wall") //raycast left
                {
                    wallRunJump();
                }
                wallJump = false;
            }
            
        }
    }

    private void wallRunJump()
    {
        movementInfo.canWallRun = movementInfo.wallRunning ? false : true;
        movementInfo.rotateCamera(0, 0.4f);
        movementInfo.wallRunning = false;
        rigidBody.useGravity = true;
        applyForce(new Vector3(0f, 100f, 50f));
    }

    private void applyForce(Vector3 force)
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        rigidBody.AddForce(force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint hit = collision.contacts[0];
        if (wallJump && hit.normal.y < 0.5f)
        {
            applyForce(new Vector3(0, 100f, 0));
            wallJump = false;
        }
    }

}
