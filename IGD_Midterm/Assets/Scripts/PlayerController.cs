using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Housekeeping
    private Rigidbody2D rb;
    private SpriteRenderer ren;
    private UIData ui;

    //Movement Stats
    [Header("Movement")]
    public int speed;
    public int sprintspeed;
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
    private Vector2 start;

    [Header("Abilities")]
    public bool canThrow = true;
    public float throwCooldown;
    public GameObject flashbang;
    public float throwForce;
    public float maxThrow;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ren = GetComponent<SpriteRenderer>();
        rm = GameObject.FindGameObjectWithTag("Manager").GetComponent<RoomControl>();
        start = transform.position;
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIData>();
    }
    public void SeeYa(GameObject cam)
    {
        int state = cam.GetComponent<SurveillanceCam>().state;
        float dist = (cam.transform.position - transform.position).magnitude;
        if(state == 1)
        {
            detectionAmt += 10 / dist;
        }
        if (state == 2)
        {
            detectionAmt += 20 / dist;
        }

        if (detectionAmt > 100)
        {
            rm.Respawn();
            Respawn();
            return;
        }
        if (detectionAmt > 20)
        {
            rm.Ping(transform.position);
        }
 
    }
    void Respawn()
    {
        transform.position = start;
        detectionAmt = 0;
        ui.Died();
    }

    // Update is called once per frame
    void Update()
    {
        MovementLogic();
        ren.color = ColorUpdate();
        Grenades();


    }
    Color ColorUpdate()
    {
        Color c = Color.black;
        if (detectionAmt > 0)
        {
            detectionAmt -= 10 * Time.deltaTime;
            //int cnum = Mathf.CeilToInt(255 * detectionAmt / 100);
            c = new Vector4(detected.r * detectionAmt / 100, detected.g * detectionAmt / 100, detected.b * detectionAmt / 100, 1);
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
            rb.velocity += accel * Time.deltaTime * wasd;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, sprintspeed);
            }
            else
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
            }

        }
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

        //Check to make sure we wont throw through anything
      
        GetComponent<Collider2D>().enabled = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, delta, 0.5f);
        if (!hit)
        {
            StartCoroutine(GrenadeTime());
      
            GameObject grenade = Instantiate(flashbang, (Vector2)transform.position + delta.normalized / 2, transform.rotation);
            Vector2 throwDelta = Vector2.ClampMagnitude(delta, maxThrow);
            grenade.GetComponent<Rigidbody2D>().AddForce(throwDelta * throwForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("hit " + hit.collider.gameObject);
        }
        GetComponent<Collider2D>().enabled = true; //Fuck you, unity
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
}
