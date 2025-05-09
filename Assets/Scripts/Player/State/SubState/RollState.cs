using UnityEngine;

public class RollState : ActionState
{
    private Vector3 rollDirection;
    private float originalColliderHeight;
    private Vector3 originalColliderCenter;

    public RollState(Player player, StateMachine stateMachine, PlayerData playerData)
        : base(player, stateMachine, playerData) {}

    public override void Enter()
    {
        base.Enter();

        // 애니메이션
        player.Anim.SetBool("roll", true);

        // 바라보는 방향 기준으로 롤 방향 결정
        rollDirection = player.transform.forward.normalized;

        // CapsuleCollider 축소 (위쪽만 줄임)
        CapsuleCollider col = player.GetComponent<CapsuleCollider>();
        if (col != null)
        {
            originalColliderHeight = col.height;
            originalColliderCenter = col.center;

            col.height = originalColliderHeight * 0.5f;
            col.center = originalColliderCenter - new Vector3(0f, originalColliderHeight * 0.25f, 0f);
        }
    }

    public override void HandleInput()
    {
        base.HandleInput();
        // 롤 상태 중 입력을 받을 필요가 있다면 이곳에 작성
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(!isGrounded)
        {
            stateMachine.ChangeState(player.inAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        DoCheck();

        // Rigidbody로 전방 이동
        if(!isActionDone)
        {
            player.RB.linearVelocity = rollDirection * playerData.rollSpeed;
        }
        else 
        {
            player.RB.linearVelocity = Vector3.zero;
        }
    }

    public override void DoCheck()
    {
        base.DoCheck();
        // 필요한 물리 체크가 있다면 구현
    }

    public override void Exit()
    {
        base.Exit();

        // 애니메이션 종료
        player.Anim.SetBool("roll", false);

        // CapsuleCollider 원상복구
        CapsuleCollider col = player.GetComponent<CapsuleCollider>();
        if (col != null)
        {
            col.height = originalColliderHeight;
            col.center = originalColliderCenter;
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        isActionDone = true;
    }
}
