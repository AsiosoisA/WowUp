using UnityEngine;

public class InAirState : State
{
    private bool climbUpCheck;

    // 공중 제어용
    private Vector3 airVelocitySmooth;
    [SerializeField] private float airControlDampTime = 0.2f;

    private float airTime; // 공중에 머문 시간

    public InAirState(Player _player, StateMachine _stateMachine, PlayerData _playerData)
        : base(_player, _stateMachine, _playerData) {}

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetBool("inAir", true);

        workspace = Vector3.zero;
        airVelocitySmooth = Vector3.zero;

        airTime = 0f; // 시간 초기화
    }

    public override void HandleInput()
    {
        base.HandleInput();

        workspace = new Vector3(xInput * playerData.airControl, 0, yInput * playerData.airControl);
        workspace = workspace.x * player.cameraTransform.right.normalized +
                    workspace.z * player.cameraTransform.forward.normalized;
        workspace.y = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        airTime += Time.deltaTime; // 시간 누적

        if (isGrounded)
        {
            stateMachine.ChangeState(player.standingState);
            return;
        }

        if (jumpInput && climbUpCheck)
        {
            stateMachine.ChangeState(player.climbUpState);
            return;
        }

        if (airTime >= 5f)
        {
            stateMachine.ChangeState(player.freeFallState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        DoCheck();

        if (player.RB.linearVelocity.y < 0.1f)
        {
            Vector3 currentHoriz = new Vector3(
                player.RB.linearVelocity.x,
                0,
                player.RB.linearVelocity.z);

            Vector3 targetHoriz = workspace.normalized * playerData.airControl * playerData.moveSpeed;

            Vector3 newHoriz = Vector3.SmoothDamp(
                currentHoriz,
                targetHoriz,
                ref airVelocitySmooth,
                airControlDampTime);

            player.RB.linearVelocity = new Vector3(
                newHoriz.x,
                player.RB.linearVelocity.y,
                newHoriz.z);
        }

        if (workspace.sqrMagnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(workspace);
            player.transform.rotation = Quaternion.Slerp(
                player.transform.rotation,
                targetRotation,
                playerData.rotationDampTime);
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
        player.Anim.SetBool("inAir", false);
    }
}
