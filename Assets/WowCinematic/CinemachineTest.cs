using UnityEngine;

public class CinemachineTest : MonoBehaviour
{
    [SerializeField]
    private UIController UC;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        UC.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            UC.ShowTitle();
        }
    }
}
