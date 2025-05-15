using UnityEngine;

public class ActionState : State
{
    protected bool isActionDone;

    public ActionState(Player _player, StateMachine _stateMachine, PlayerData _playerData) : base(_player, _stateMachine, _playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();

        isActionDone = false;
    }
 
    public override void HandleInput()
    {
        base.HandleInput();
    }
 
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isActionDone)
        {
            if(!isGrounded)
            {
                stateMachine.ChangeState(player.inAirState);
            }
            if(isGrounded && player.RB.linearVelocity.y < 0.1f)
            {
                stateMachine.ChangeState(player.standingState);
            }
        }        
    }
 
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

 
    public override void Exit()
    {
        base.Exit();

        player.InputHandler.UseJumpInput();
    }
}