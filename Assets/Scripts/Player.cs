using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    public Rigidbody rb;
    public float speed;
    public float jumpHeight;
    private float jumpSpeed = 50;
    public float gravity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent <Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Makes character run        
        transform.Translate(Vector3.forward * (speed) * Time.deltaTime);

        // Make character jump - TODO
        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     rb.AddForce(Vector3.up * jumpSpeed);
        // }

    }
}


