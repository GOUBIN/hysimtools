using UnityEngine;

public class HRT_TimeManager :MonoBehaviour
{
    private float _update_left_time;

    public static HRT_Timer pt = new HRT_Timer();

    void Update()
    {
        pt.Update();
        float timeDelta = Time.realtimeSinceStartup - _update_left_time;
    
        _update_left_time = Time.realtimeSinceStartup;
        TimeTick.instance.TickTick(timeDelta);

   
    }


    private void Start()
    {
        
    }

 

  
}
  


