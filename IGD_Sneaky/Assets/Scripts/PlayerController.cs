using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Housekeeping
    private Rigidbody rb;
    private MeshRenderer ren;
    private UIData ui;
    private AudioSource source;
    public AudioClip rewind;

    //Movement Stats
    [Header("Movement")]
    public int speed;
    public float drag;
    public float accel;
    public bool canMove = true;


    //Movement Logic
    private Vector2 wasd;

    [Header("Detection")]
    //Detection
    private float detectionAmt;
    public Color detected;
    private RoomControl rm;
    private Vector3 start;

    [Header("Abilities")]
    public bool canThrow = true;
    public float throwCooldown;
    public GameObject flashbang;
    public float throwForce;
    public float maxThrow;
    private bool dashing;
    public float dashDuration;
    public float dashMult;
    private float lastDashtime;
    public float dashCooldown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ren = GetComponent<MeshRenderer>();
        rm = GameObject.FindGameObjectWithTag("Manager").GetComponent<RoomControl>();
        start = transform.position;
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIData>();


        source = GetComponent<AudioSource>();

    }
    public void SeeYa(GameObject cam)
    {
        int state = cam.GetComponent<SurveillanceCam>().state;
        float dist = (cam.transform.position - transform.position).magnitude;
        if(state == 1)
        {
            detectionAmt += 8000 / (dist * dist) * Time.deltaTime ;
        }
        if (state == 2)
        {
            detectionAmt += 8000 / (dist * dist) * Time.deltaTime;
        }

        if (detectionAmt > 254)
        {
            rm.Respawn();
            Respawn();
            return;
        }
        if (detectionAmt > 100)
        {
            rm.Ping(transform.position);
        }
 
    }
    void Respawn()
    {
        transform.position = start;
        detectionAmt = 0;
        ui.Died();
        source.PlayOneShot(rewind);

    }

    // Update is called once per frame
    void Update()
    {
        MovementLogic();
        ren.material.color = ColorUpdate();
        Grenades();

        if (Input.GetKey(KeyCode.Escape))
        {
            //SceneManager.LoadScene(0);
        }

    }
    Color ColorUpdate()
    {
        Color c = Color.black;
        if (detectionAmt > 0)
        {
            detectionAmt -= 10 * Time.deltaTime;
            c = new Vector4(detectionAmt/100, 0, 0, 1);
        }
        return c;
    }
    void MovementLogic()
    {
        wasd = Vector2.zero;
        if (canMove)
        {
            if (Input.GetKey(KeyCode.W))
            {
                wasd.y++;
            }
            if (Input.GetKey(KeyCode.A))
            {
                wasd.x--;
            }
            if (Input.GetKey(KeyCode.S))
            {
                wasd.y--;
            }
            if (Input.GetKey(KeyCode.D))
            {
                wasd.x++;
            }
            rb.velocity -= drag * Time.deltaTime * rb.velocity;

            if (dashing)
            {
                rb.velocity += accel * Time.deltaTime * dashMult * (Vector3)wasd;
            }
            else
            {
                rb.velocity += accel * Time.deltaTime * (Vector3)wasd;
            }


            rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
            if (dashing)
            {
                rb.velocity *= dashMult;
            }
      


            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(Time.time - lastDashtime > dashCooldown)
                {
                    StartCoroutine(Dash());
                }
            }
        }
    }
    IEnumerator Dash()
    {
        lastDashtime = Time.time;
        dashing = true;
        yield return new WaitForSeconds(dashDuration);
        dashing = false;
        yield break;

    }
    void Grenades()
    {
        if(Input.GetMouseButtonDown(0) && canThrow)
        {
            ThrowGrenade();
        }
    }
    void ThrowGrenade()
    {
        Vector2 mpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 delta = mpos - (Vector2)transform.position;



        StartCoroutine(GrenadeTime());

        GameObject grenade = Instantiate(flashbang, (Vector2)transform.position + delta.normalized / 2, transform.rotation);
        Vector2 throwDelta = Vector2.ClampMagnitude(delta, maxThrow);
        grenade.GetComponent<Rigidbody>().AddForce(throwDelta * throwForce, ForceMode.Impulse);

        //Check to make sure we wont throw through anything (scrapped after we made them impacts)
        //if (!Physics.Raycast(transform.position, delta, out RaycastHit hit, 0.5f))
        //{

        //}
        //else
        //{
        //   //We hit a wall or smth, so wont throw the grenade

        //}
    }
    IEnumerator GrenadeTime()
    {
        if (canThrow)
        {
            canThrow = false;
            yield return new WaitForSeconds(throwCooldown);
            canThrow = true;
            yield break;
        }
        else
        {
            yield break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trip"))
        {
            rm.Respawn();
            Respawn();
            //Somehow tell the player they sadly died
        }
    }
}
