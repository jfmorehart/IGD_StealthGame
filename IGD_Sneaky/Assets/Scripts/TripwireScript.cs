using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripwireScript : MonoBehaviour
{

    private RoomControl rm;


    public bool A;
    public bool B;
    public bool C;
    public bool D;

    public float cycleTime;
    private float privateTimer = Mathf.Infinity;

    [SerializeField] private MeshRenderer cren;
    [SerializeField] private BoxCollider ccol;

    // Start is called before the first frame update
    void Start()
    {
        privateTimer = Time.time;
        rm = GameObject.FindGameObjectWithTag("Manager").GetComponent<RoomControl>();
        rm.Register(this);
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.time - privateTimer > cycleTime)
        {
            privateTimer = Time.time;
            //A
            cren.enabled = A;
            ccol.enabled = A;
        }
        if (Time.time - privateTimer > 3 * cycleTime / 4)
        {
            //D
            cren.enabled = D;
            ccol.enabled = D;
        }
        else if (Time.time - privateTimer > 2 * cycleTime / 4)
        {
            //C
            cren.enabled = C;
            ccol.enabled = C;
        }
        else if(Time.time - privateTimer > cycleTime / 4)
        {
            //B
            cren.enabled = B;
            ccol.enabled = B;
        }
    
       
    }

    public void Respawn()
    {
        privateTimer = Time.time;
    }
}
