using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBase : MonoBehaviour
{
    public float FuseTime;
    public GameObject explosion;
    [HideInInspector] public AudioSource sauce;
    public AudioClip boom;
    public float boomvol;
    // Start is called before the first frame update
    public virtual void Start()
    {
        if(GameObject.FindGameObjectWithTag("Music") != null)
        {
            sauce = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.4f);

        StartCoroutine(TickTock());
    }
    public virtual void Detonate()
    {
        if(sauce != null)
        {
            sauce.PlayOneShot(boom, boomvol);
        }

        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    IEnumerator TickTock()
    {
        yield return new WaitForSeconds(FuseTime);
        Detonate();
        yield break;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopAllCoroutines();
        Detonate();
    }
}
