using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PressFToInteract : MonoBehaviour
{
    private GameObject player;
    public float interactRegion = 1f;

    private Vector3 playerPos;
    private Vector3 thisPos;
    private float distance;

    public Text MidScreenNotice;
    public Text TopScreenNotice;

    private GameObject soundEffect;

    void Start()
    {
        thisPos = this.transform.position;
        player = GameObject.Find("Player");
        soundEffect = GameObject.Find("AudioManager");
    }

    void Update()
    {
        playerPos = player.transform.position;
        distance = Vector3.Distance(thisPos, playerPos);
        if (distance <= interactRegion)
        {
            MidScreenNotice.text = "Press F to interact";

            if (this.tag == "Door")
            {
                if (Input.GetKey(KeyCode.F))
                {
                    if (player.GetComponent<ItemCollectionManager>().axe)
                    {
                        Destroy(this.gameObject);
                        // play audio when interact (destroying doors..)
                        soundEffect.GetComponent<AudioSource>().clip = soundEffect.GetComponent<AudioManager>().audioClips[4];
                        soundEffect.GetComponent<AudioSource>().Play();
                        TopScreenNotice.text = "Now I need to keep going...";
                        MidScreenNotice.text = "";
                    }
                    else
                    {
                        soundEffect.GetComponent<AudioSource>().clip = soundEffect.GetComponent<AudioManager>().audioClips[5];
                        soundEffect.GetComponent<AudioSource>().Play();
                        TopScreenNotice.text = "I might need something to open this door...";
                    }

                }
            }
            else
            {
                if (Input.GetKey(KeyCode.F))
                {
                    player.GetComponent<ItemCollectionManager>().pickUp(this.tag);
                    // play audio when pickup
                    soundEffect.GetComponent<AudioSource>().clip = soundEffect.GetComponent<AudioManager>().audioClips[3];
                    soundEffect.GetComponent<AudioSource>().Play();
                    Destroy(this.gameObject);
                    MidScreenNotice.text = "";
                }
            }

        } else if (distance - interactRegion < 0.01f) // clear the notice after player leave the area
        {
            MidScreenNotice.text = "";
            TopScreenNotice.text = "";
        }
    }
}
