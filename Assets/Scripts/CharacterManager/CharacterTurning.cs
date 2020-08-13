using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTurning : MonoBehaviour
{
    Vector3 mouseDif;
    Vector3 lastMousePos;
    public float sensitivity = 0.5f;

    private void Start()
    {
        lastMousePos = Input.mousePosition;
    }

    void Update()
    {
        mouseDif = Input.mousePosition - lastMousePos;
        Vector3 rotation = new Vector3(0, mouseDif.x * sensitivity, 0);

        this.gameObject.transform.Rotate(rotation);
        lastMousePos = Input.mousePosition;
    }
}