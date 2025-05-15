using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    // Components
    public Rigidbody RB { get; private set; }
    public bool CanSetVelocity { get; set; }
    public Vector3 CurrentVelocity { get; private set; }
    public float CurrentSpeed { get; private set; }

    public Transform orientation;

    private Vector3 workspace;

    #region Unity Callback Functions
    public void Awake()
    {
        RB = GetComponentInParent<Rigidbody>();
        CanSetVelocity = true;
    }

    public void LogicUpdate()
    {
        CurrentVelocity = RB.linearVelocity;
        CurrentSpeed = CurrentVelocity.magnitude;
    }
    #endregion
    
    #region Velocity Management Methods
    public void SetVelocityZero()
    {
        workspace = Vector3.zero;
        SetFinalVelocity();
    }

    public void SetVelocity(float speed, Vector3 direction, int sign = 1)
    {
        direction = direction.normalized;
        workspace = direction * speed * sign;
        SetFinalVelocity();
    }

    public void SetVelocity(Vector3 velocity)
    {
        workspace = velocity;
        SetFinalVelocity();
    }

    public void SetVelocityX(float x)
    {
        workspace.Set(x, workspace.y, workspace.z);
        SetFinalVelocity();
    }

    public void SetVelocityY(float y)
    {
        workspace.Set(workspace.x, y, workspace.z);
        SetFinalVelocity();
    }

    public void SetVelocityZ(float z)
    {
        workspace.Set(workspace.x, workspace.y, z);
        SetFinalVelocity();
    }

    private void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            RB.linearVelocity = workspace;
            CurrentVelocity = workspace;
        }
    }
    #endregion

    #region Gravity Management Methods
    public void SetGravityScale(float gravity)
    {
        // RB.gravi = gravity;
    }
    #endregion
}
