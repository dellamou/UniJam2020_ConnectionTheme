using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    private bool mapOpened = false;
    // Start is called before the first frame update
    void Start()
    {

    }
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mapOpened){
                this.mapOpened = !this.mapOpened;
                this.GetComponent<AudioSource>().clip = audioClips[1];
                this.GetComponent<AudioSource>().Play();
            }
            else {
                this.mapOpened = !this.mapOpened;
                this.GetComponent<AudioSource>().clip = audioClips[2];
                this.GetComponent<AudioSource>().Play();
            }
        }
        else {

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.GetComponent<AudioSource>().clip = audioClips[0];
            this.GetComponent<AudioSource>().Play();
        }
    }
}
