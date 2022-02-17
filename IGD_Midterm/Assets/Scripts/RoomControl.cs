using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    public List<SurveillanceCam> Cams = new List<SurveillanceCam>();

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void Register(SurveillanceCam cscript)
    {
        Cams.Add(cscript);
    }
    public void Respawn()
    {
        foreach(SurveillanceCam cam in Cams)
        {
            cam.Respawn();
        }
    }
    public void Ping(Vector2 pos)
    {
        foreach (SurveillanceCam cam in Cams)
        {
            cam.Ping(pos);
        }
    }
    public void Distract(Vector2 pos)
    {
        foreach (SurveillanceCam cam in Cams)
        {
            cam.Ping(pos);
        }

    }

}
