using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressFToDestroy : MonoBehaviour
{
    public GameObject player;
    public float DestroyRegion = 2f;

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
        if (distance <= DestroyRegion)
        {
            Debug.Log("press F to interact");
            if (Input.GetKey(KeyCode.F))
            {
                if (player.GetComponent<ItemCollectionManager>().axe)
                {
                    Destroy(this.gameObject);
                } else
                {
                    Debug.Log("I might need something to open this door...");
                }

            }
        }
    }
}
