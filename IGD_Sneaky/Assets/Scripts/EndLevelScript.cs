using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndLevelScript : MonoBehaviour
{
    private bool playertouching;
    [SerializeField] private MeshRenderer yourloveisfade;
    private Material fadeout;
    private void Start()
    {
        fadeout = yourloveisfade.material;
        fadeout.color = new Color(fadeout.color.r, fadeout.color.g, fadeout.color.b, 0);
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
        if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings)
        {
            int next = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(next);
        }

        yield break;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playertouching)
        {
            Debug.Log("round over");
            StartCoroutine(Fade());
            
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playertouching = true;

        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playertouching = false;
        }
    }

}
