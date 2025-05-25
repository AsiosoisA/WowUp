using UnityEngine;

public class CollisionSenses : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform shoulderCheck;
    [SerializeField] private Transform headCheck;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float climbUpCheckLength = 0.5f;
    [SerializeField] private float waterCheckRadius = 0.2f; // 물 감지 반경 (조정 가능)

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsObject;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private LayerMask whatIsWater; // 물 레이어

    public bool Ground => Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);

    public bool WallRight => Physics.Raycast(shoulderCheck.position, shoulderCheck.right, 0.5f, whatIsWall);
    public bool WallLeft => Physics.Raycast(shoulderCheck.position, -shoulderCheck.right, 0.5f, whatIsWall);

    /// <summary>
    /// 캐릭터가 물 속에 있는지 확인
    /// </summary>
    public bool IsInWater => Physics.CheckSphere(shoulderCheck.position, waterCheckRadius, whatIsWater);

    /// <summary>
    /// 원시 클라임업 조건: groundCheck 또는 shoulderCheck에서 충돌, headCheck에서는 미충돌
    /// </summary>
    public bool CanClimbUp
    {
        get
        {
            bool shoulderHit = Physics.Raycast(shoulderCheck.position, transform.forward, climbUpCheckLength, whatIsObject);
            bool headHit = Physics.Raycast(headCheck.position, transform.forward, climbUpCheckLength, whatIsObject);
            return shoulderHit && !headHit;
        }
    }

    /// <summary>
    /// 감지된 오브젝트의 ledge Y 좌표(상단 경계 Y값)
    /// </summary>
    /// <returns>감지된 오브젝트 상단 Y값, 없으면 Mathf.NegativeInfinity 반환</returns>
    public float GetClimbUpLedgeHeight()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(shoulderCheck.position, transform.forward, out hitInfo, climbUpCheckLength, whatIsObject))
        {
            Collider col = hitInfo.collider;
            float rawTopY = col.bounds.max.y;
            const float heightOffset = 1.425f;
            return rawTopY - heightOffset;
        }
        return Mathf.NegativeInfinity;
    }

    public Vector3 GetClimbUpDirection()
    {
        if (!CanClimbUp) return Vector3.zero;

        RaycastHit hitInfo;
        if (Physics.Raycast(shoulderCheck.position, transform.forward, out hitInfo, climbUpCheckLength, whatIsObject) ||
            Physics.Raycast(groundCheck.position, transform.forward, out hitInfo, climbUpCheckLength, whatIsObject))
        {
            Vector3 dir = hitInfo.point - transform.position;
            dir.y = 0f;
            return dir.normalized;
        }
        return Vector3.zero;
    }

    private void OnDrawGizmosSelected()
    {
        // Ground sphere
        if (groundCheck != null)
        {
            Gizmos.color = Ground ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        // Water sphere
        if (groundCheck != null)
        {
            Gizmos.color = IsInWater ? Color.blue : Color.cyan;
            Gizmos.DrawWireSphere(groundCheck.position, waterCheckRadius);
        }

        // Climb up checks
        if (groundCheck != null && shoulderCheck != null && headCheck != null)
        {
            bool groundHit = Physics.Raycast(groundCheck.position, transform.forward, climbUpCheckLength, whatIsObject);
            bool shoulderHit = Physics.Raycast(shoulderCheck.position, transform.forward, climbUpCheckLength, whatIsObject);
            bool headHit = Physics.Raycast(headCheck.position, transform.forward, climbUpCheckLength, whatIsObject);
            Color rayColor = shoulderHit && !headHit ? Color.green : Color.red;
            Gizmos.color = rayColor;

            Gizmos.DrawLine(groundCheck.position, groundCheck.position + transform.forward * climbUpCheckLength);
            Gizmos.DrawLine(shoulderCheck.position, shoulderCheck.position + transform.forward * climbUpCheckLength);
            Gizmos.DrawLine(headCheck.position, headCheck.position + transform.forward * climbUpCheckLength);
        }

        // Wall checks
        if (shoulderCheck != null)
        {
            // WallRight check
            Gizmos.color = WallRight ? Color.green : Color.red;
            Gizmos.DrawLine(shoulderCheck.position, shoulderCheck.position + shoulderCheck.right * 0.5f);

            // WallLeft check
            Gizmos.color = WallLeft ? Color.green : Color.red;
            Gizmos.DrawLine(shoulderCheck.position, shoulderCheck.position - shoulderCheck.right * 0.5f);
        }
    }
}