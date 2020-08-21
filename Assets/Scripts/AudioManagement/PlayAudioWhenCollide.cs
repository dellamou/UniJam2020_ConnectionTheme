using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioWhenCollide : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.GetComponent<AudioSource>().Play();
        }
    }
}
