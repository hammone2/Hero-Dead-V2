using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    public CharacterController controller;
    public GameBehaviour _gameManager;

    public float speed = 12f;
    public float gravity = -19.62f;
    public float jumpHeight = 3f;

    Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public GameObject cam;

    public GameObject bullet;
    public float bulletSpeed = 100f;

    public delegate void JumpingEvent();
    public event JumpingEvent playerJump;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;

        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = transform.right * x + transform.forward * z; // original code

        //this normalize code sucks fix later
        //Vector3 move = new Vector3(x, 0, z).normalized;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            playerJump();
        }

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding");
        if (collision.gameObject.tag == "Enemy")
        {
            _gameManager.HP -= 1;
            Debug.Log("Colliding with Enemy");
        }
    }
}