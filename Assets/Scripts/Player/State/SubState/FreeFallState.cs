using UnityEngine;

public class FreeFallState : State
{  
    private float groundedTime;
    public FreeFallState(Player _player, StateMachine _stateMachine, PlayerData _playerData)
        : base(_player, _stateMachine, _playerData) {}

    public override void Enter()
    {
        base.Enter();
        groundedTime = 0f;
        // RagDoll 시작
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isGrounded)  groundedTime += Time.deltaTime;
        else groundedTime = 0f;

        if(groundedTime > 5f)
        {
            // StandUpState 진입 ( 아직 미구현 )
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
    }
}
