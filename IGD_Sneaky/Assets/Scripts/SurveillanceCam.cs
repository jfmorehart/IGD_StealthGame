using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveillanceCam : MonoBehaviour
{
    public GameObject Player;
    public Transform Rotater;
    public Transform Arm;

    //Stats
    public float Frequency;
    public int AngleRange; //Controls direction

    public float Direction;
    public float ArmDirection;
    private float forward;
    public float lightWidthdeg;

    //AI
    [HideInInspector] public int state = 1; //0 idle, 1 back and forth, 2 track player
    private float stateclock;
    public int forgetTime;
    private float LockPos;

    private RoomControl rm;
    private float timeval;

    //RotationUpgrade
    private float Rdir;
    public float turnfraction;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        forward = transform.eulerAngles.z;
        ArmDirection = forward;
        Direction = forward;
        Rdir = forward;
        rm = GameObject.FindGameObjectWithTag("Manager").GetComponent<RoomControl>();
        rm.Register(this);
    }

    // Update is called once per frame
    public void Ping(Vector2 pos)
    {
        stateclock = Time.time;
        state = 2;
        Vector2 Delta = (Vector2)transform.position - pos;
        LockPos = Mathf.Atan2(Delta.y, Delta.x) * Mathf.Rad2Deg;
    }
    public void Respawn()
    {
        stateclock = -10;
        state = 1;
        timeval = 0;
        Rdir = forward;
    }
    public void Spotted(GameObject playa)
    {
        Vector3 delta = playa.transform.position - transform.position;
        float deltarot = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        float currot = Mathf.Atan2(-Rotater.transform.right.y, -Rotater.transform.right.x) * Mathf.Rad2Deg;
        if (Mathf.Abs(DifferenceFinder(deltarot, currot)) < lightWidthdeg)
        {
            playa.GetComponent<PlayerController>().SeeYa(gameObject);
        }
    }
    void Update()
    {
        if (state == 1)
        {
            Direction = forward + Mathf.Sin(timeval / Frequency) * AngleRange;
            ArmDirection = forward - Mathf.Sin(timeval / Frequency) * AngleRange/3;
            timeval += Time.deltaTime;
        }
        if (state == 2)
        {
            Direction = LockPos;
            if (Time.time - stateclock > forgetTime)
            {
                //timeval = Mathf.Asin((LockPos - forward) / AngleRange) * Frequency;
                state = 1;
                stateclock -=10 ;
            }
        }
        if (state == 3)
        {
            Vector2 Delta = transform.position - Player.transform.position;
            Direction = Mathf.Atan2(Delta.y, Delta.x) * Mathf.Rad2Deg;
            ArmDirection = Direction / 3;
            if (Time.time - stateclock > forgetTime)
            {
                state = 2;
                stateclock -= 10;
            }
        }

        if(Mathf.Abs(DifferenceFinder(forward, Rdir)) > AngleRange)
        {
            if (DifferenceFinder(forward, Rdir) > 0)
            {
                Direction = forward + AngleRange;
                if(Rdir > Direction)
                {
                    Rdir -= turnfraction * Time.deltaTime;
                }
            }
            else
            {
                Direction = forward - AngleRange;
                if (Rdir < Direction)
                {
                    Rdir += turnfraction * Time.deltaTime;
                }
            }
        }


        Arm.transform.eulerAngles = new Vector3(0, 0, ArmDirection);
        if (Mathf.Abs(DifferenceFinder(Rdir, Direction)) > turnfraction * Time.deltaTime)
        {
            if (DifferenceFinder(Rdir, Direction) > 0)
            {
                Rdir += turnfraction * Time.deltaTime;
            }
            else
            {
                Rdir -= turnfraction * Time.deltaTime;
            }
        }
        else
        {
            //Rdir = Direction;
        }

        Rotater.transform.eulerAngles = new Vector3(0, 0, Rdir);
    }
    float DifferenceFinder(float angleA, float angleB)
    {

        float diffA = angleA - angleB;

        if (diffA > 0 && diffA < 360)
        {

            if (angleB + 360 - angleA < diffA)
            {
                return ((angleB + 360) - angleA);
            }
            else
            {
                return -diffA;
            }
        }
        float diffB = angleB - angleA;
        if (angleA + 360 - angleB  < diffB)
        {
            return -(angleA + 360)- angleB;
        }
        else
        {
            return (diffB);
        }
    }
}
