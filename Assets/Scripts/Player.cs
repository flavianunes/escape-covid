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
    public Animator anim;
    private bool isDead;

    private bool isMovingLeft;
    private bool isMovingRight; 

    private float firstPathPosition_X = (float)-5.4;
    private float secondPathPosition_X = (float)0;
    private float thirdPathPosition_X = (float)5.2;
    
    private GameController gc;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent <Rigidbody>();
        gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        OnCollision();
        Vector3 direction = Vector3.forward * speed;

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
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayRadius, layer) && !isDead) {
            anim.SetTrigger("die");
            isDead = true;
            speed = 0;
            Invoke("GameOver", 1f);
        };
    }

    void GameOver(){
        gc.ShowGameOver();
    }
}


