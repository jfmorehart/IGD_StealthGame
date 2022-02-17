using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBase : MonoBehaviour
{
    public float FuseTime;
    public GameObject explosion;

    // Start is called before the first frame update
    public virtual void Start()
    {
        StartCoroutine(TickTock());
    }
    public virtual void Detonate()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    IEnumerator TickTock()
    {
        yield return new WaitForSeconds(FuseTime);
        Detonate();
        yield break;
    }
}
