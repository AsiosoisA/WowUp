using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class LoopTimeLine : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector director;
    [SerializeField]
    private double loopStart = 28.0;
    //[SerializeField]
    //private double SkipTime = 43.5;

    private bool getAnyKey =false;
    private float EnableInput = 0f;
    [SerializeField]
    private float EnableInputTime = 25f;
    //private bool Skip= false;

    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Escape))
        {
            if (Skip == false)
            {
                SkipIntro();
                
            }

        }
        */
        
        EnableInput += Time.deltaTime;
        if(EnableInput>=EnableInputTime)
        {
            if (Input.anyKeyDown)
            {
                getAnyKey = true;
            }
        }
        
        
  
    }

    public void StartLoopTimeline()
    {
        if(getAnyKey == false)
        {
            director.time = loopStart;
            director.Play();
            Debug.Log("StartLoopTimeLine »£√‚µ ");
        }
    }

    /*
    private void SkipIntro()
    {
        director.time = SkipTime;
        director.Play();
        Skip = true;
    }
    */

}
