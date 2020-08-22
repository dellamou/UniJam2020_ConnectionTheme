using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PressFToTalk : MonoBehaviour
{
     private float distance;
    public Transform origin;
    public Transform dest;
    public float interactDistance = 8f;
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
                SceneManager.LoadScene("TrueEndDisplay");
            }
        }
    }
}
