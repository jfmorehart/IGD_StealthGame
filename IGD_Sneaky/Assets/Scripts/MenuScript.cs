using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public TextMeshProUGUI anyText;

    [SerializeField] private MeshRenderer yourloveisfade;
    private Material fadeout;
    private string btext;
    private float ptimer;
    private int pnum = 0;

    [SerializeField] private float pdelay;

    private void Start()
    {
        btext = "Press any key to start";
        fadeout = yourloveisfade.material;
        fadeout.color = new Color(fadeout.color.r, fadeout.color.g, fadeout.color.b, 0);
        anyText.text = btext;
    }

    void Update()
    {
        if (Time.time - ptimer > pdelay)
        {
            anyText.text = btext + new string('.', pnum);
            ptimer = Time.time;
            pnum++;
            if(pnum > 3)
            {
                pnum = 0;
            }
        }

        if (Input.anyKeyDown)
        {
            StartCoroutine(Fade());
            btext = "Loading";
        }
    }


    public IEnumerator Fade()
    {
        Debug.Log("your love is fade");
        yield return new WaitForSeconds(1.8f);
        for (float alpha = 0.02f; alpha < 0.9f; alpha += 0.01f)
        {
            fadeout.color = new Color(fadeout.color.r, fadeout.color.g, fadeout.color.b, alpha);
            yield return new WaitForFixedUpdate();
        }
        fadeout.color = new Color(fadeout.color.r, fadeout.color.g, fadeout.color.b, 1);
        //next level
        SceneManager.LoadScene(1);
        yield break;

    }

}
