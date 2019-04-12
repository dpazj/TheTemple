using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpAbility : MonoBehaviour {

    private MovementInfo movementInfo;
    private Rigidbody rigidBody;

    private float coolDown = 0f;
    private bool wallJump = false;

    private RaycastHit rayhit;


    void Start () {
        movementInfo = GetComponent<MovementInfo>();
        rigidBody = GetComponent<Rigidbody>();
    }
	
	void Update () {
        if(coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
       
		if(Input.GetKeyDown(KeyCode.Space) && !wallJump && !movementInfo.grounded && (movementInfo.jumping || movementInfo.wallRunning)) //Player has to be jumping/wall running
        {
            if(coolDown <= 0 && movementInfo.wallJumpCounter < movementInfo.wallJumpWithoutReset)
            {
                Debug.Log(movementInfo.wallJumpCounter);
                wallJump = true;
            }
            
        }
        
    }

    private void FixedUpdate()
    {

        if (wallJump)
        {
            if (movementInfo.wallRunning)
            {
                if (Physics.Raycast(transform.position, transform.right, out rayhit, 0.7f) && rayhit.transform.tag == "Wall") //raycast right
                {
                    wallRunJump();
                }
                else if(Physics.Raycast(transform.position, -transform.right, out rayhit, 0.7f) && rayhit.transform.tag == "Wall") //raycast left
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
        rigidBody.velocity = Vector3.zero;//new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        rigidBody.AddForce(force, ForceMode.Impulse);
        coolDown = movementInfo.wallJumpCoolDown;
        movementInfo.wallJumpCounter++;
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
