using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
 
    public StateMachine StateMachine { get; protected set; }
    public PlayerInputHandler InputHandler { get; protected set; }
    public CollisionSenses CollisionSenses { get; protected set; }
    public Movement Movement { get; protected set; }

    public StandingState standingState;
    public JumpState jumpState;
    public InAirState inAirState;
    public ClimbUpState climbUpState;
    public RollState rollState;
    public WallRunState wallRunState;
    public WallJumpState wallJumpState;
    public FreeFallState freeFallState;
 
    public Rigidbody RB { get; private set; }
    public Animator Anim { get; private set; }

    public Transform cameraTransform;
    public Vector3 playerVelocity; // 필요에 따라 활용
     
    // Start is called before the first frame update
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        // 만약 플레이어가 물리적 회전에 의해 넘어지지 않도록 하고 싶다면 아래와 같이 고정할 수 있음
        RB.freezeRotation = true;
        
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Movement = GetComponentInChildren<Movement>();
        cameraTransform = Camera.main.transform;
 
        StateMachine = new StateMachine();

        standingState = new StandingState(this, StateMachine, playerData);
        jumpState = new JumpState(this, StateMachine, playerData);
        inAirState = new InAirState(this, StateMachine, playerData);
        climbUpState = new ClimbUpState(this, StateMachine, playerData);
        rollState = new RollState(this, StateMachine, playerData);
        wallRunState = new WallRunState(this, StateMachine, playerData);
        wallJumpState = new WallJumpState(this, StateMachine, playerData);
        freeFallState = new FreeFallState(this, StateMachine, playerData);
 
        StateMachine.Initialize(standingState);
 
        // 중력 값 조정 (Rigidbody.useGravity를 사용하므로 실제 중력 계산은 Rigidbody에 맡김)
        // gravityValue *= gravityMultiplier;
    }
 
    private void Update()
    {
        StateMachine.CurrentState.HandleInput();
        StateMachine.CurrentState.LogicUpdate();
        Movement.LogicUpdate();
    }
 
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimtionFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
