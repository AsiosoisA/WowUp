using UnityEngine;

public class FreeFallState : State
{  
    private float groundedTime;
    private ToggleRagdoll Ragdoll;


    public FreeFallState(Player _player, StateMachine _stateMachine, PlayerData _playerData)
        : base(_player, _stateMachine, _playerData) {}

    public override void Enter()
    {
        base.Enter();
        groundedTime = 0f;
        // RagDoll 시작
        Ragdoll = Object.FindAnyObjectByType<ToggleRagdoll>();
        Ragdoll.Toggle();

    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Ragdoll.IsRagdollGrounded())  groundedTime += Time.deltaTime;
        else groundedTime = 0f;

        if (groundedTime > 2f)
        {
            // StandUpState 진입 ( 아직 미구현 )
            stateMachine.ChangeState(player.standingState);     // 일단 standing으로 설정
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
        // RagDoll 끝
        Ragdoll.Toggle();
    }
}
