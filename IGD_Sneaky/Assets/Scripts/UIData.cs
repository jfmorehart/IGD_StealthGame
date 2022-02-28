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
    // Start is called before the first frame update

    void Start()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        leveltext.text = "Level " + level.ToString();

        if ( level == 0)
        {
            PlayerPrefs.SetInt("deaths", 0);
        }
        else
        {
            deaths = PlayerPrefs.GetInt("deaths", 0);
        }
        deathtext.text = deaths.ToString();
    }

    public void Died()
    {
        deaths++;
        deathtext.text = deaths.ToString() + " Deaths";
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("deaths", deaths);
    }

}
