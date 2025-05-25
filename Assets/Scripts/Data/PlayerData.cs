using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Controls")]
    public float walkSpeed = 3.0f;
    public float moveSpeed = 8.0f;
    public float sprintSpeed = 13.0f;
    public float jumpForce = 5f;
    public float gravityMultiplier = 2f;
    public float rotationSpeed = 5f;
    public float acceleration = 20f;
    public float deceleration = 30f;    
    public float rollSpeed = 2f;
    // public float maxWallRunSpeed = 4f;
    public float wallRunSpeed = 8f;
    public float wallJumpForce = 5f;

    [Header("Animation Smoothing")]
    [Range(0, 1)] public float speedDampTime = 0.1f;
    [Range(0, 1)] public float velocityDampTime = 0.9f;
    [Range(0, 1)] public float rotationDampTime = 0.2f;
    [Range(0, 1)] public float airControl = 0.5f;

    [Header("Air Control Settings")]
    public float airControlDampTime = 0.2f;

    [Header("ShadowDash")]
    public float shadowDashHoldTime = 2f;
    public float shadowDashForce = 5f;
}
