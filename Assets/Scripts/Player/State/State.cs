using UnityEngine;
 
public class State
{
    protected Player player;
    protected StateMachine stateMachine;
    protected PlayerData playerData;

    protected Vector3 currentVelocity;    
    protected Vector3 velocitySmooth;
    protected float currentSpeed;
 
    protected Vector3 workspace;    // 속도 계산용 워크스페이스 변수

    // Inputs
    private bool skill1Input;
    protected bool skill2Input;
    protected Vector2 input = Vector2.zero;
    protected int xInput;
    protected int yInput;
    protected bool jumpInput;
    protected bool sprintInput;
    protected bool walkInput;

    protected bool isAnimationFinished;
    protected bool isGrounded;
 
    public State(Player _player, StateMachine _stateMachine, PlayerData _playerData)
    {
        player = _player;
        stateMachine = _stateMachine;
        playerData = _playerData;
    }
 
    public virtual void Enter()
    {
        Debug.Log("Enter State: "+this.ToString());
        isAnimationFinished = false;
    }
 
    public virtual void HandleInput()
    {

    }
 
    public virtual void LogicUpdate()
    {
        skill1Input = player.InputHandler.Skill1Input;
        skill2Input = player.InputHandler.Skill2Input;
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        walkInput = player.InputHandler.WalkInput;
        sprintInput = player.InputHandler.SprintInput;
        jumpInput = player.InputHandler.JumpInput;
        input = new Vector2(xInput, yInput);

        if(skill1Input)
        {
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
 
    public virtual void PhysicsUpdate()
    {
        DoCheck();
    }
 
    public virtual void Exit()
    {
    }

    public virtual void DoCheck()
    {
        isGrounded = player.CollisionSenses.Ground;
        player.Anim.SetBool("isGrounded", isGrounded);
    }

    public virtual void AnimationTrigger() {}

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}