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
              
		if(Input.GetKeyDown(KeyCode.Space) && !wallJump) //Player has to be jumping/wall running
        {
            if(movementInfo.wallJumpCounter < movementInfo.wallJumpWithoutReset && movementInfo.timeJumping > movementInfo.minJumpTime)
            {
                wallJump = true;
            }
            
        }
        
    }

    private void FixedUpdate()
    {
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
            return;
        }
        
        if (wallJump && !movementInfo.grounded && (movementInfo.jumping || movementInfo.wallRunning))
        {
            if (movementInfo.wallRunning)
            {
                if (Physics.Raycast(transform.position, transform.right, out rayhit, 0.7f) && rayhit.transform.tag == "Wall") //raycast right
                {
                    wallRunJump(-1);
                }
                else if(Physics.Raycast(transform.position, -transform.right, out rayhit, 0.7f) && rayhit.transform.tag == "Wall") //raycast left
                {
                    wallRunJump(1);
                }

                wallJump = false;
            }
        }
    }

    private void wallRunJump(int directionalMultiplier)
    {
        //Cancels wall run and then applies jump
        movementInfo.mustSwapWallrunSide = true;
        movementInfo.rotateCamera(0, 0.4f);
        movementInfo.wallRunning = false;
        movementInfo.canWallRun = true;
        rigidBody.useGravity = true;
        applyForce(new Vector3(directionalMultiplier * 40f, 100f, 0));

    }

    private void applyForce(Vector3 force)
    {
        Vector3 forceWithDirection = transform.up * force.y;
        forceWithDirection += transform.right * force.x;
        forceWithDirection += transform.forward * force.z;
        
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(forceWithDirection, ForceMode.Impulse);

        coolDown = movementInfo.wallJumpCoolDown;
        movementInfo.wallJumpCounter++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint hit = collision.contacts[0];

        if (wallJump && hit.normal.y < 0.1f)
        {
            applyForce(new Vector3(0, 90f, 0));
            wallJump = false;
        }
    }

}
