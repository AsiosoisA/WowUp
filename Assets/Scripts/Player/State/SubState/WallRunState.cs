using UnityEngine;

public class WallRunState : ActionState
{
    private bool isWallLeft;
    private bool isWallRight;

    public WallRunState(Player player, StateMachine stateMachine, PlayerData playerData)
        : base(player, stateMachine, playerData) {}

    public override void Enter()
    {
        base.Enter();

        player.RB.useGravity = false;

        isWallLeft = player.CollisionSenses.WallLeft;
        isWallRight = player.CollisionSenses.WallRight;

        if(isWallLeft)
            player.Anim.SetBool("wallRunLeft", true);
        else if (isWallRight)
            player.Anim.SetBool("wallRunRight", true);
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished || (!isWallLeft && !isWallRight)) isActionDone = true;

        if(jumpInput)
        {
            stateMachine.ChangeState(player.wallJumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        DoCheck();

        player.Movement.SetVelocity(player.Movement.orientation.forward * playerData.wallRunSpeed);

        // 벽 쪽으로 붙는 힘
        if (isWallRight)
        {
            player.RB.AddForce(player.Movement.orientation.right * 3f, ForceMode.Force);
        }
        else if (isWallLeft)
        {
            player.RB.AddForce(-player.Movement.orientation.right * 3f, ForceMode.Force);
        }
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

        player.RB.useGravity = true;
    
        player.Anim.SetBool("wallRunLeft", false);
        player.Anim.SetBool("wallRunRight", false);
    }
}
