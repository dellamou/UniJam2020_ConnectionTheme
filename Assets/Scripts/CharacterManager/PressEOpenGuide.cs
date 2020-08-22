using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEOpenGuide : MonoBehaviour
{
    public GameObject guide;

    // Start is called before the first frame update
    void Start()
    {
        guide.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            guide.SetActive(!guide.activeSelf);
        }
    }
}
