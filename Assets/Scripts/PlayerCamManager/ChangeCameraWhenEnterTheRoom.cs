using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraWhenEnterTheRoom : MonoBehaviour
{

    public GameObject endCamera;
    public GameObject model;
    public GameObject friend;

    public GameObject connection;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("MainCamera").SetActive(true);
        //GameObject.FindGameObjectWithTag("EndCamera").SetActive(false);
        //GameObject.FindGameObjectWithTag("EndPlayer").SetActive(false);
        endCamera.SetActive(false);
        model.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EndTrigger")
        {
            Destroy(collision.gameObject);
            this.GetComponent<MoveCam>().enabled = false;
            connection.SetActive(false);
            GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
            //GameObject.FindGameObjectWithTag("EndCamera").SetActive(true);
            //GameObject.FindGameObjectWithTag("EndPlayer").SetActive(true);
            endCamera.SetActive(true);
            model.SetActive(true);
            model.GetComponent<selfMoveTowards>().moveTowards(friend.transform.position);
        }
    }
}
