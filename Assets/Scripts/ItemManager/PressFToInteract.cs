using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PressFToInteract : MonoBehaviour
{
    public GameObject player;
    public float interactRegion = 2f;

    private Vector3 playerPos;
    private Vector3 thisPos;
    private float distance;

    public Text MidScreenNotice;
    public Text TopScreenNotice;

    void Start()
    {
        thisPos = this.transform.position;
    }

    void Update()
    {
        playerPos = player.transform.position;
        distance = Vector3.Distance(thisPos, playerPos);
        if (distance <= interactRegion)
        {
            MidScreenNotice.text = "Press F to pick up " + this.tag;

            if (this.tag == "Door")
            {
                MidScreenNotice.text = "Press F to open this " + this.tag;
                if (Input.GetKey(KeyCode.F))
                {
                    if (player.GetComponent<ItemCollectionManager>().axe)
                    {
                        Destroy(this.gameObject);
                        TopScreenNotice.text = "Now I need to keep going...";
                        MidScreenNotice.text = "";
                    }
                    else
                    {
                        TopScreenNotice.text = "I might need something to open this door...";
                    }

                }
            } 
            else
            {
                if (Input.GetKey(KeyCode.F))
                {
                    player.GetComponent<ItemCollectionManager>().pickUp(this.tag);
                    Destroy(this.gameObject);
                    MidScreenNotice.text = "";
                }
            }

        } else if (distance - interactRegion < 0.1f) // clear the notice after player leave the area
        {
            MidScreenNotice.text = "";
            TopScreenNotice.text = "";
        }
    }
}
