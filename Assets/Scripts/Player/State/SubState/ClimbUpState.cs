using UnityEngine;

public class ClimbUpState : ActionState
{  
    private const float climbYOffset = 1.1f; // ledge 위로 올릴 높이(offset)

    public ClimbUpState(Player _player, StateMachine _stateMachine, PlayerData _playerData) : base(_player, _stateMachine, _playerData)
    {
    }
 
    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseJumpInput();

        // ledge y 위치에 맞춰 플레이어 y 조정
        float ledgeY = player.CollisionSenses.GetClimbUpLedgeHeight();
        if (!float.IsNegativeInfinity(ledgeY))
        {
            Vector3 pos = player.transform.position;
            pos.y = ledgeY + climbYOffset;
            player.transform.position = pos;
        }

        player.RB.useGravity = false;
        player.Movement.SetVelocityZero();

        // 회전은 기존 로직 유지
        player.Anim.SetBool("climbUp", true);
    }
 
    public override void HandleInput()
    {
        base.HandleInput();
    }
 
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished) isActionDone = true;
    }
 
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();

        float forwardOffsetDistance = 0.7f;
        float upwardOffsetDistance  = 0.5f;

        // transform.forward 방향으로 0.7, Y축 위로 1.5 만큼 이동
        Vector3 climbOffset = player.transform.forward * forwardOffsetDistance + Vector3.up * upwardOffsetDistance;
        player.transform.position += climbOffset;

        player.Anim.SetBool("climbUp", false);

        player.RB.useGravity = true;
    }
}