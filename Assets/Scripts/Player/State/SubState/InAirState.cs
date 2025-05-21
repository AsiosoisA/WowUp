using UnityEngine;

public class InAirState : State
{  
    private bool climbUpCheck;
    private float airTime;

    // 공중 제어용
    private Vector3 airVelocitySmooth;
    [SerializeField] private float airControlDampTime = 0.2f;

    public InAirState(Player _player, StateMachine _stateMachine, PlayerData _playerData)
        : base(_player, _stateMachine, _playerData) {}

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetBool("inAir", true);
        // 초기 workspace 설정
        workspace = Vector3.zero;
        airVelocitySmooth = Vector3.zero;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        // 공중 입력 반영
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

        if (airTime >= 2f)
        {
            stateMachine.ChangeState(player.freeFallState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        DoCheck();

        // 최고점 이후(하강 시작)부터 수평 제어
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

        // 공중 회전 처리
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
