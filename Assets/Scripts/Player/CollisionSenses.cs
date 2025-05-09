using UnityEngine;

public class CollisionSenses : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform shoulderCheck;
    [SerializeField] private Transform headCheck;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float climbUpCheckLength = 0.5f;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsObject;

    public bool Ground => Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);

    /// <summary>
    /// 원시 클라임업 조건: groundCheck 또는 shoulderCheck에서 충돌, headCheck에서는 미충돌
    /// </summary>
    public bool CanClimbUp
    {
        get
        {
            // bool groundHit = Physics.Raycast(groundCheck.position, transform.forward, climbUpCheckLength, whatIsObject);
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
        if (Physics.Raycast(shoulderCheck.position, transform.forward, out hitInfo, climbUpCheckLength, whatIsObject)) // ||Physics.Raycast(groundCheck.position, transform.forward, out hitInfo, climbUpCheckLength, whatIsObject)
            
        {
            Collider col = hitInfo.collider;
            float rawTopY = col.bounds.max.y;
            const float heightOffset = 1.425f;  // 경험적으로 도출된 보정치
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
    }
}