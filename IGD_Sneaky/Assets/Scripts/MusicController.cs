using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource source;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.transform);
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        source.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (source.isPlaying)
            {
                source.Pause();
            }
            else
            {
                source.UnPause();
            }

        }
    }
}
