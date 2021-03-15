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
    private bool isMovingLeft;
    private bool isMovingRight; 
    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent <Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.forward * speed;

        // Make character jump - TODO
        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     rb.AddForce(Vector3.up * jumpSpeed);
        // }

        if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < 3f && !isMovingRight) {
            isMovingRight = true;
            StartCoroutine(RightMove());
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > -3f && !isMovingLeft) {
            isMovingLeft = true;
            StartCoroutine(LeftMove());
        }
        controller.Move(direction * Time.deltaTime);

    }

    IEnumerator LeftMove() {
        for(float i=0; i<5; i += 0.1f) {
            controller.Move(Vector3.left * Time.deltaTime * 5);
            yield return null;
        }
        isMovingLeft = false;
    }

    IEnumerator RightMove() {
        for(float i=0; i<5; i += 0.1f) {
            controller.Move(Vector3.right * Time.deltaTime * 5);
            yield return null;
        }
        isMovingRight = false;
    }
}


