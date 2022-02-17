using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private SpriteRenderer ren;
    // Start is called before the first frame update
    void Start()
    {
        ren = GetComponent<SpriteRenderer>();
        StartCoroutine(Fade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Fade()
    {
        for(float alpha = 1; alpha > 0.02f; alpha *= 0.9f)
        {
            ren.color = new Color(ren.color.r, ren.color.g, ren.color.b, alpha);
            yield return null;
        }
        Destroy(gameObject);
        yield break;

    }
}
