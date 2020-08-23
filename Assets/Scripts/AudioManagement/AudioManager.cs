using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    private bool torchOn = false;
    // Start is called before the first frame update
    void Start()
    {
    }
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && GameObject.Find("HasTorch"))
        {
            if (torchOn){
                this.torchOn = !this.torchOn;
                this.GetComponent<AudioSource>().clip = audioClips[1];
                this.GetComponent<AudioSource>().Play();
            }
            else {
                this.torchOn = !this.torchOn;
                this.GetComponent<AudioSource>().clip = audioClips[2];
                this.GetComponent<AudioSource>().Play();
            }
        }
        else {

        }
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.M))
        {
            this.GetComponent<AudioSource>().clip = audioClips[0];
            this.GetComponent<AudioSource>().Play();
        }
    }
}
