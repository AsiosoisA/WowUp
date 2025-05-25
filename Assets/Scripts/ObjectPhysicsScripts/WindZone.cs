using UnityEngine;

public class WindZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MyBall>().enterWindZone();
        }
        // Debug.Log("onTrigger");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MyBall>().exitWindZone();
        }
    }
}
