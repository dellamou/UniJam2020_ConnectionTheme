using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MtoOpenMap : MonoBehaviour
{
    private Canvas MapCanvas;
    private GameObject DimScreenPlane; 
    private MeshRenderer dimCam;
    // Start is called before the first frame update
    void Start()
    {
        MapCanvas = this.gameObject.GetComponent<Canvas>();
        DimScreenPlane = GameObject.Find("DimScreen");
        this.dimCam=DimScreenPlane.GetComponent<MeshRenderer>();
        MapCanvas.enabled = false;
        dimCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)){
            MapCanvas.enabled = !MapCanvas.enabled;
            dimCam.enabled = !dimCam.enabled;
        }
    }
}
