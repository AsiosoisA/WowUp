using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool SprintInput { get; private set; }
    public bool WalkInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool Skill1Input { get; private set; }
    public bool Skill2Input { get; private set; }

    #region Input Manage Methods
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);       
    }

    public void OnWalkInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            WalkInput = true;
        }

        if(context.canceled)
        {
            WalkInput = false;
        }
    }

    public void OnSprintInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            SprintInput = true;
        }

        if(context.canceled)
        {
            SprintInput = false;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            JumpInput = true;
        }
    }
    
    public void OnSkill1Input(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Skill1Input = true;
        }

        if(context.canceled)
        {
            Skill1Input = false;
        }
    }

    public void OnSkill2Input(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Skill2Input = true;
        }

        if(context.canceled)
        {
            Skill2Input = false;
        }
    }

    public void UseJumpInput() => JumpInput = false;
    #endregion
}