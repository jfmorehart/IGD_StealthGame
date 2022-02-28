using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private MeshRenderer ren;
    // Start is called before the first frame update
    void Start()
    {
        ren = GetComponent<MeshRenderer>();
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
            ren.material.color = new Color(ren.material.color.r, ren.material.color.g, ren.material.color.b, alpha);
            yield return null;
        }
        Destroy(gameObject);
        yield break;

    }
}
