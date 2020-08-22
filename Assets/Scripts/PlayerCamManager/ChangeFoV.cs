using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFoV : MonoBehaviour
{
    private float t = 0.01f;
    public float maxFOV = 70;
    private float minFOV = 30;
    private Camera MainCam;
    
    // Start is called before the first frame update
    void Start()
    {
        MainCam = this.GetComponent<Camera>();
        MainCam.fieldOfView = minFOV;
    }

    // Update is called once per frame
    void Update()
    {
        MainCam.fieldOfView = Mathf.Lerp(MainCam.fieldOfView, maxFOV, t);

        //if finish changing, disable this script and enable movement
        if (MainCam.fieldOfView >= maxFOV-1){
            GameObject.Find("Player").GetComponent<MoveCam>().enabled = true;
            MainCam.fieldOfView = maxFOV;
            this.enabled = false;
        }
    }
}
