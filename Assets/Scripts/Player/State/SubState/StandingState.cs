using UnityEngine;

public class StandingState : State
{
    private float speedMultiplier;
    private bool climbUpCheck;

    // 입력 벡터와 속도 보간값
    private Vector3 inputDirection;
    private Vector3 velocity;

    public StandingState(Player player, StateMachine stateMachine, PlayerData playerData)
        : base(player, stateMachine, playerData) {}

    public override void Enter()
    {
        base.Enter();
        inputDirection = Vector3.zero;
        velocity = player.RB.linearVelocity;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        // 카메라 기준 입력
        Vector3 rawInput = new Vector3(xInput, 0, yInput);
        rawInput = rawInput.x * player.cameraTransform.right.normalized +
                   rawInput.z * player.cameraTransform.forward.normalized;
        rawInput.y = 0;
        inputDirection = rawInput.normalized;

        // 속도 결정
        if (inputDirection == Vector3.zero) speedMultiplier = 0;
        else if (walkInput) speedMultiplier = playerData.walkSpeed;
        else if (sprintInput) speedMultiplier = playerData.sprintSpeed;
        else speedMultiplier = playerData.moveSpeed;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.Anim.SetFloat("speed", velocity.magnitude, playerData.speedDampTime, Time.deltaTime);
        player.Anim.SetFloat("direction", xInput, playerData.speedDampTime, Time.deltaTime);

        if (!isGrounded)
        {
            stateMachine.ChangeState(player.inAirState);
            return;
        }
        if (jumpInput)
        {
            if (climbUpCheck) stateMachine.ChangeState(player.climbUpState);
            else stateMachine.ChangeState(player.jumpState);
            return;
        }
        if (isGrounded && skill2Input)
        {
            stateMachine.ChangeState(player.rollState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        DoCheck();

        // 가속/감속: MoveTowards로 부드러운 속도 제어
        Vector3 targetVel = inputDirection * speedMultiplier;
        float accel = playerData.acceleration;    // 새로 추가
        float decel = playerData.deceleration;    // 새로 추가
        float smoothTime = (targetVel.magnitude > velocity.magnitude) ? accel : decel;
        velocity = Vector3.MoveTowards(velocity, targetVel, smoothTime * Time.fixedDeltaTime);

        // y 유지
        velocity.y = player.RB.linearVelocity.y;
        player.Movement.SetVelocity(velocity);

        // 회전
        if (inputDirection.sqrMagnitude > 0)
        {
            Quaternion targetRot = Quaternion.LookRotation(inputDirection);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRot, playerData.rotationDampTime);
        }
    }

    public override void DoCheck()
    {
        base.DoCheck();
        climbUpCheck = player.CollisionSenses.CanClimbUp;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
