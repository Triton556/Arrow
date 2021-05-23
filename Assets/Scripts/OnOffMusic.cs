using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffMusic : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool musicIsPlaying = audioSource.isPlaying;
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (musicIsPlaying)
            {
                audioSource.Stop();
            }
            else if (!musicIsPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
