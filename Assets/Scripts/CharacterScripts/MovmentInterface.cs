using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentInterface : MonoBehaviour {

    [SerializeField]
    private bool controllable = true;
    [SerializeField]
    private bool canJump = true;
    [SerializeField]
    private bool canWallrun = true;
    [SerializeField]
    private bool canWallJump = true;
    

    private PlayerController playerController;
    private JumpAbility jumpAbility;
    private WallrunAbility wallrunAbility;
    private WallJumpAbility wallJumpAbility;
	
	void Start () {
        playerController = gameObject.AddComponent(typeof(PlayerController)) as PlayerController;
        jumpAbility = gameObject.AddComponent(typeof(JumpAbility)) as JumpAbility;
        wallrunAbility = gameObject.AddComponent(typeof(WallrunAbility)) as WallrunAbility;
        wallJumpAbility = gameObject.AddComponent(typeof(WallJumpAbility)) as WallJumpAbility;
        

        setControllable(controllable);
        setCanJump(canJump);
        setCanWallrun(canWallrun);
        setCanWallJump(canWallJump);
    }


    public void setControllable(bool x)
    {
        controllable = x;
        playerController.enabled = controllable;
    }

    public void setCanJump(bool x)
    {
        canJump = x;
        jumpAbility.enabled = canJump;
    }

    public void setCanWallrun(bool x)
    {
        canWallrun = x;
        wallrunAbility.enabled = canWallrun;
    }

    public void setCanWallJump(bool x)
    {
        canWallJump = x;
        wallJumpAbility.enabled = canWallJump;
    }
}
