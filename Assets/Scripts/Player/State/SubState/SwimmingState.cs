using UnityEngine;

public class SwimmingState : State
{
    private float speedMultiplier;           // Speed multiplier for movement
    private Vector3 inputDirection;         // Direction based on input
    private Vector3 velocity;               // Current velocity of the character

    // Constructor
    public SwimmingState(Player player, StateMachine stateMachine, PlayerData playerData)
        : base(player, stateMachine, playerData) {}

    // Called when entering the state
    public override void Enter()
    {
        base.Enter();
        inputDirection = Vector3.zero;
        velocity = player.RB.linearVelocity;
        player.Anim.SetBool("isSwimming", true); // Start swimming animation
    }

    // Handle player input
    public override void HandleInput()
    {
        base.HandleInput();
        // Calculate movement direction relative to the camera
        Vector3 rawInput = new Vector3(xInput, 0, yInput); // Horizontal input
        rawInput = rawInput.x * player.cameraTransform.right.normalized +
                   rawInput.z * player.cameraTransform.forward.normalized;
        rawInput.y = 0;

        // Check for spacebar to move upward
        if (Input.GetKey(KeyCode.Space))
        {
            rawInput.y = 1; // Move up
        }
        else
        {
            rawInput.y = 0; // Stay horizontal
        }

        inputDirection = rawInput.normalized;

        // Set speed multiplier based on input
        speedMultiplier = (inputDirection == Vector3.zero) ? 0 : playerData.swimSpeed;
    }

    // Update logic every frame
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // Exit to StandingState if no longer in water
        if (!player.CollisionSenses.IsInWater)
        {
            stateMachine.ChangeState(player.standingState);
        }
    }

    // Update physics every fixed timestep
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        // Smoothly adjust velocity toward target
        Vector3 targetVel = inputDirection * speedMultiplier;
        float accel = playerData.swimAcceleration;
        float decel = playerData.swimDeceleration;
        float smoothTime = (targetVel.magnitude > velocity.magnitude) ? accel : decel;
        velocity = Vector3.MoveTowards(velocity, targetVel, smoothTime * Time.fixedDeltaTime);

        // Apply buoyancy to counteract gravity partially
        float buoyancyForce = playerData.buoyancyForce;
        player.RB.AddForce(Vector3.up * buoyancyForce, ForceMode.Acceleration);

        // Set the character's velocity
        player.Movement.SetVelocity(velocity);

        // Rotate character to face movement direction
        if (inputDirection.sqrMagnitude > 0)
        {
            Quaternion targetRot = Quaternion.LookRotation(inputDirection);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRot, playerData.rotationDampTime);
        }
    }

    // Called when exiting the state
    public override void Exit()
    {
        base.Exit();
        player.Anim.SetBool("isSwimming", false); // Stop swimming animation
    }
}