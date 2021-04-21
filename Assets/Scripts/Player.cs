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
    public int score = 0;
    public float gravity;

    public float rayRadius;
    public LayerMask layerDie;
    public LayerMask layerScore;
    private bool isDead;

    private bool isMovingLeft;
    private bool isMovingRight; 
    private Animator anim;

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

        anim = this.gameObject.transform.Find("MaleFree1").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        speed += 0.002f;
        OnCollision();
        Vector3 direction = Vector3.forward * speed;

        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 5.2f && !isMovingRight) {
            isMovingRight = true;

            StartCoroutine(RightMove());
        }
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -5.2f && !isMovingLeft) {
            isMovingLeft = true; 

            StartCoroutine(LeftMove());
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
        }
        controller.Move(direction * Time.deltaTime);

    }

    IEnumerator LeftMove() {
        controller.Move(Vector3.left * Time.deltaTime * 25f);
        isMovingLeft = false;
        yield return null;
    }

    IEnumerator RightMove() {
        controller.Move(Vector3.right * Time.deltaTime * 25f);
        isMovingRight = false;
        yield return null;
        
    }

    void OnCollision() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayRadius, layerDie) && !isDead) {
            anim.SetTrigger("wave");
            isDead = true;
            speed = 0;
            Invoke("GameOver", 1f);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Score"))
        {
            score++;
            Destroy(collision.gameObject);
            Debug.Log("Score: " + score);

        }
    }
    
    void GameOver(){
        gc.ShowGameOver();
    }
}


