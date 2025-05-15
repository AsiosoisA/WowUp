using UnityEngine;

public class WallJumpState : ActionState
{
    private bool isWallLeft;
    private bool isWallRight;

    public WallJumpState(Player player, StateMachine stateMachine, PlayerData playerData)
        : base(player, stateMachine, playerData) {}

    public override void Enter()
    {
        base.Enter();

        player.Anim.SetBool("wallJump", true);
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        DoCheck();
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Exit()
    {
        base.Exit();

        player.Anim.SetBool("wallJump", false);
    }
}
