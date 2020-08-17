using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private bool isOnGround = true;
    private Rigidbody selfRigidbody;
    public float forceConst = 50f;
    public float forwardSpeed = 4f;
    public float backwardSpeed = 3.6f;
    public int rotationSpeed = 80;

    private float dirSpeed;

    private void Start()
    {
        selfRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        dirSpeed = Mathf.Sqrt((Mathf.Pow(forwardSpeed, 2) / 2));
        // move forward
        if (Input.GetKey(KeyCode.W))
        {
            // make sure when two direction key is pressed ,the directional speed is adjusted
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Translate(Vector3.forward * dirSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                this.transform.Translate(Vector3.forward * dirSpeed * Time.deltaTime);
            }
            else
            {
                this.transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
            }
        }

        // translation to left (without turning)
        if (Input.GetKey(KeyCode.A))
        {

            // if is moving forward or backward at the same time, speed need adjusted
            if (Input.GetKey(KeyCode.W))
            {
                this.transform.Translate(Vector3.left * dirSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                this.transform.Translate(Vector3.left * dirSpeed * Time.deltaTime);
            }
            else
            {
                this.transform.Translate(Vector3.left * forwardSpeed * Time.deltaTime);
            }
        }

        // translation to back (without turning) and will move lower than normal
        if (Input.GetKey(KeyCode.S))
        {

            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Translate(Vector3.back * dirSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                this.transform.Translate(Vector3.back * dirSpeed * Time.deltaTime);
            }
            else
            {
                this.transform.Translate(Vector3.back * backwardSpeed * Time.deltaTime);
            }
        }

        // translation to right (without turning)
        if (Input.GetKey(KeyCode.D))
        {

            if (Input.GetKey(KeyCode.W))
            {
                this.transform.Translate(Vector3.right * dirSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                this.transform.Translate(Vector3.right * dirSpeed * Time.deltaTime);
            }
            else
            {
                this.transform.Translate(Vector3.right * forwardSpeed * Time.deltaTime);
            }
        }
        /*
        // jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isOnGround)
            {
                selfRigidbody.AddForce(0, forceConst, 0, ForceMode.Impulse);
                isOnGround = false;
            }
        }
        */
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
        }
    }
    */
}
