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

    public float rayRadius;
    public LayerMask layer;

    private bool isMovingLeft;
    private bool isMovingRight; 

    private float firstPathPosition_X = (float)-5.4;
    private float secondPathPosition_X = (float)0;
    private float thirdPathPosition_X = (float)5.2;
    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent <Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        OnCollision();
        Vector3 direction = Vector3.forward * speed;

        // Make character jump - TODO
        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     rb.AddForce(Vector3.up * jumpSpeed);
        // }

        if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < 2.5f && !isMovingRight) {
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
        // nao funciona, nao aparece qual o erro :(
        // float step =   1.0f * Time.deltaTime;
        // Vector3 newPosition;
        // if (transform.position.x == secondPathPosition_X) {
        //     newPosition =  new Vector3(firstPathPosition_X, transform.position.y, transform.position.z);
        // } else if (transform.position.x == thirdPathPosition_X) {
        //     newPosition = new Vector3(secondPathPosition_X, transform.position.y, transform.position.z);
        // }
        // transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
        //Vector3 newPosition = (transform.position.x);
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

    void OnCollision() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayRadius)) {
            Debug.Log("bateu?");
        };
    }
}


