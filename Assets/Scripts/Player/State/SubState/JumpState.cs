using UnityEngine;

public class JumpState : ActionState
{  
    private bool climbUpCheck;
    private Vector3 jumpVelocity;
    private Vector3 airVelocitySmooth;       // 수평 속도 보간용

    private bool isWallLeft;
    private bool isWallRight;

    [SerializeField] private float airControlDampTime = 0.2f; // 공중 제어 반응 속도

    public JumpState(Player _player, StateMachine _stateMachine, PlayerData _playerData) : base(_player, _stateMachine, _playerData)
    {
    }
 
    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseJumpInput();

        player.Anim.SetBool("jump", true);
        player.Anim.SetBool("isGround", false);

        jumpVelocity = Vector3.up * playerData.jumpForce;
        currentVelocity = player.RB.linearVelocity;

        player.RB.linearVelocity = currentVelocity + jumpVelocity;
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

        if(isAnimationFinished) isActionDone = true;


        if(jumpInput && climbUpCheck)
        {
            stateMachine.ChangeState(player.climbUpState);
            return;
        }

        if(player.InputHandler.NormInputX == -1 && isWallLeft || player.InputHandler.NormInputX == 1 && isWallRight)
        {
            stateMachine.ChangeState(player.wallRunState);
        }
    }
 
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        DoCheck();
        // 최고점 달성 후 조금씩 이동 제어 가능
        if (player.RB.linearVelocity.y < 0.1f)
        {
            // 현재 수평 속도
            Vector3 currentHoriz = new Vector3(
                player.RB.linearVelocity.x, 
                0, 
                player.RB.linearVelocity.z);

            // 목표 수평 속도 = 입력 방향 * moveSpeed (또는 별도 공중 속도)
            Vector3 targetHoriz = workspace.normalized * playerData.airControl * playerData.moveSpeed;

            // SmoothDamp 으로 부드럽게 보간
            Vector3 newHoriz = Vector3.SmoothDamp(
                currentHoriz, 
                targetHoriz, 
                ref airVelocitySmooth, 
                airControlDampTime);

            // y 속도는 기존 그대로 유지
            player.RB.linearVelocity = new Vector3(
                newHoriz.x, 
                player.RB.linearVelocity.y, 
                newHoriz.z);
        }

        // 3) (옵션) 회전
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
        isWallLeft = player.CollisionSenses.WallLeft;
        isWallRight = player.CollisionSenses.WallRight;
    }
 
    public override void Exit()
    {
        base.Exit();

        player.Anim.SetBool("jump", false);
        player.Anim.SetBool("isGround", true);
    }
}
