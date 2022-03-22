using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private MeshRenderer ren;
    private int myAlpha;
    // Start is called before the first frame update
    void Start()
    {
        ren = GetComponent<MeshRenderer>();
        StartCoroutine(Fade());
        ren.material.SetFloat(myAlpha, 1);
        if (ren.material.HasFloat("_CustomAlpha"))
        {
            myAlpha = Shader.PropertyToID("_CustomAlpha");
            Debug.Log("got it");
        }
        else
        {
            Debug.Log("houston");
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Fade()
    {
        for(float alpha = 1f; alpha > 0.1f; alpha -= 2 * alpha * Time.deltaTime)
        {
            transform.localScale -= 2 * Time.deltaTime * transform.localScale;
            ren.material.SetFloat(myAlpha, alpha);
            yield return 0;
        }
        Destroy(gameObject);
        yield break;

    }
}
