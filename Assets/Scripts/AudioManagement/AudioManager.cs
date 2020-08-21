using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {

    }
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            this.GetComponent<AudioSource>().clip = audioClips[0];
            this.GetComponent<AudioSource>().Play();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            this.GetComponent<AudioSource>().clip = audioClips[0];
            this.GetComponent<AudioSource>().Play();
        }
    }
}
