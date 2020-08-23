using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscOpenSetting: MonoBehaviour
{
    public GameObject Setting;
    public GameObject Cam;
    public GameObject UIs;

    // Start is called before the first frame update
    void Start()
    {
        Setting.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Setting.SetActive(!Setting.activeSelf);
            Cam.SetActive(!Cam.activeSelf);
            this.GetComponent<MoveCam>().enabled = Cam.activeSelf;
            UIs.SetActive(!UIs.activeSelf);;
            if (Cam.activeSelf){
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else{
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
