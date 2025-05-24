using UnityEngine;

public class StandUpState : State
{  
    private float groundedTime;


    public StandUpState(Player _player, StateMachine _stateMachine, PlayerData _playerData)
        : base(_player, _stateMachine, _playerData) {}

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetBool("standUp", true);
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.standingState);
        }
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
        player.Anim.SetBool("standUp", false);
    }
}