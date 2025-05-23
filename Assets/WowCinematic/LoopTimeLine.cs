using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class LoopTimeLine : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector director;
    [SerializeField]
    private double loopStart = 28.0;

    private bool getAnyKey =false;

    void Update()
    {
        if(Input.anyKeyDown)
        {
            getAnyKey = true;
        }
    }

    public void StartLoopTimeline()
    {
        if(getAnyKey == false)
        {
            director.time = loopStart;
            director.Play();
            Debug.Log("»£√‚µ ");
        }
    }

}
