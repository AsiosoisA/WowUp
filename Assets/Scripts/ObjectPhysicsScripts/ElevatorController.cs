using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour
{
    public float speed = 2f;
    public float delayBeforeMove = 3f;
    public float targetDistance = 20f;

    private bool isMoving = false;
    private bool isReturning = false;
    private bool playerOnElevator = false;
    private Vector3 originPosition;
    private Vector3 targetPosition;

    void Start()
    {
        originPosition = transform.position;
        targetPosition = transform.position + new Vector3(0, targetDistance, 0);
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

        // �÷��̾ ���������� ���� ���������ٸ� ����
        if (!playerOnElevator)
        {
            isReturning = true;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveTowards(targetPosition, () => isMoving = false);
        }
        if (isReturning)
        {
            MoveTowards(originPosition, () => isReturning = false);
        }
    }

    void MoveTowards(Vector3 target, System.Action onComplete)
    {
        // �̵� 
        transform.position
            = Vector3.MoveTowards(
                transform.position,
                target,
                speed * Time.deltaTime
            );
        // ���� �� ���߱�
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            transform.position = target;
            onComplete();
        }
    }
}
