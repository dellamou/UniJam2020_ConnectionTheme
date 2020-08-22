using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//the origin must go to the checkPoint and interact with checkPoint, then come back to the dest.
public class PressFCallFriend : MonoBehaviour
{
    private float distance;
    public Transform origin;
    public Transform checkPoint;
    public Transform dest;
    public float interactDistance = 8f;
    //private bool checked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   

        distance = Vector3.Distance(origin.position, dest.position);
        if (distance < interactDistance) {
            if (Input.GetKeyDown(KeyCode.F)) {
                SceneManager.LoadScene("GoodEndDisplay");
            }
        }
    }
}
