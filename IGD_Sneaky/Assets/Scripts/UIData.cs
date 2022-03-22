using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIData : MonoBehaviour
{
    public int deaths = 0;
    public TextMeshProUGUI deathtext;
    public TextMeshProUGUI leveltext;
    public TextMeshProUGUI timertext;
    public float timer;

    private bool last = false;
    // Start is called before the first frame update

    void Start()
    {
        int level = SceneManager.GetActiveScene().buildIndex;


        if (level == 1)
        {
            PlayerPrefs.SetInt("deaths", 0);
            PlayerPrefs.SetFloat("timer", 0);
        }

        leveltext.text = "Level " + (level).ToString();
        deaths = PlayerPrefs.GetInt("deaths", 0);
        timer = PlayerPrefs.GetFloat("timer", 0);
        deathtext.text = deaths.ToString();

        Debug.Log( level + " " + SceneManager.sceneCountInBuildSettings);
        if (level + 1 == SceneManager.sceneCountInBuildSettings)
        {
            //Last level
            last = true;
            timertext.text = "Final Time: " + (Mathf.Round(timer * 100) / 100).ToString();
            deathtext.text = "You died " + deaths.ToString() + " times.";
            leveltext.text = "End";
        }
    }
    private void Update()
    {
        if (!last)
        {
            timer += Time.deltaTime;
            timertext.text = (Mathf.Round(timer * 100) / 100).ToString();
        }


    }
    public void Died()
    {
        deaths++;
        deathtext.text = deaths.ToString() + " Deaths";
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("deaths", deaths);
        PlayerPrefs.SetFloat("timer", timer);
    }

}
