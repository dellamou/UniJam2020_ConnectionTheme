using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressFToPickUp : MonoBehaviour
{
    public GameObject player;
    public float pickUpRegion = 5f;

    private Vector3 playerPos;
    private Vector3 thisPos;
    private float distance;

    void Start()
    {
        thisPos = this.transform.position;
    }

    void Update()
    {
        playerPos = player.transform.position;
        distance = Vector3.Distance(thisPos, playerPos);
        if (distance <= pickUpRegion)
        {
            Debug.Log("press F to interact");
            if (Input.GetKey(KeyCode.F))
            {
                player.GetComponent<ItemCollectionManager>().pickUp(this.tag);
                Destroy(this.gameObject);
                Debug.Log("You got the " + this.tag);
            }
        }
    }
}
