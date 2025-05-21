using UnityEngine;

public class WallJumpState : ActionState
{
    private bool isWallLeft;
    private bool isWallRight;

    // Vector3로 변경
    private Vector3 wallJumpDirection;

    public WallJumpState(Player player, StateMachine stateMachine, PlayerData playerData)
        : base(player, stateMachine, playerData) {}

    public override void Enter()
    {
        base.Enter();

        // 방향 체크
        DoCheck();

        // 벽이 왼쪽에 있으면 플레이어 로컬 오른쪽 + 위, 
        // 오른쪽에 있으면 로컬 왼쪽 (−right) + 위 로 점프
        if (isWallLeft)
            wallJumpDirection = (player.transform.right + Vector3.up).normalized;
        else if (isWallRight)
            wallJumpDirection = (-player.transform.right + Vector3.up).normalized;
        else
            wallJumpDirection = Vector3.up; // 예외 처리 (벽 없음)


        // 점프 힘 적용 (3D용 velocity 초기화)
        player.RB.linearVelocity = Vector3.zero;
        player.RB.AddForce(wallJumpDirection * playerData.wallJumpForce, ForceMode.Impulse);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        // 추가 입력 처리 필요 시 여기에 작성
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished) isActionDone = true;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        DoCheck();
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isWallLeft = player.CollisionSenses.WallLeft;
        isWallRight = player.CollisionSenses.WallRight;
    }

    public override void Exit()
    {
        base.Exit();
        
        player.Anim.SetBool("wallJump", false);
    }
}
