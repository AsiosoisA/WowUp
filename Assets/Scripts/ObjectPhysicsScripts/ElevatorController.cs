using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour
{
    public float speed = 2f;
    public float delayBeforeMove = 3f;
    public Transform targetPosition;

    private bool isMoving = false;
    private bool isReturning = false;
    private bool playerOnElevator = false;
    private Vector3 originPosition;

    void Start()
    {
        originPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            playerOnElevator = true;
            StartCoroutine(startElevatorAfterDelay());
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnElevator = false;
            StartCoroutine(ReturnToStartAfterDelay());
        }
    }

    IEnumerator startElevatorAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeMove);
        isMoving = true;
    }

    IEnumerator ReturnToStartAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeMove);

        // 플레이어가 엘리베이터 밖을 빠져나갔다면 복귀
        if (!playerOnElevator)
        {
            isReturning = true;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveTowards(targetPosition.position, () => isMoving = false);
        }
        if (isReturning)
        {
            MoveTowards(originPosition, () => isReturning = false);
        }
    }

    void MoveTowards(Vector3 target, System.Action onComplete)
    {
        // 이동 
        transform.position
            = Vector3.MoveTowards(
                transform.position,
                target,
                speed * Time.deltaTime
            );
        // 도착 시 멈추기
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            transform.position = target;
            onComplete();
        }
    }
}
