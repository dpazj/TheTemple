using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInfo : MonoBehaviour {

    public float forwardVelocity { get; private set;}
    public float straffeVelocity { get; private set; }
    public bool grounded { get; private set;}
    public bool wallrunnig { get; private set; }
    public bool canWallRun { get;  set; }

    //Settings
    public float maxSpeed = 8.0f;



    //Observes these classes
    PlayerController pc;
    WallrunAbility wallrun;
    


    // Use this for initialization
    void Start () {
        pc = GetComponent<PlayerController>();
        wallrun = GetComponent<WallrunAbility>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        forwardVelocity = pc.GetForwardVelocity();
        grounded = pc.GetGrounded(); //TODO get grounded check should be in this class.
	}
}
