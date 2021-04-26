using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public float speed = 15.0f;
    public float sideSpeed = 15.0f;
    private bool isGrounded;
    public bool playerAlive = true;
    private float horizontalInput;
    private Vector3 jump = new Vector3(0, 1, 0);
    private int lives = 3;
    private float score = 0;
    private float scoreAdd = 0;
    public AudioSource playSound;
    public AudioSource playSound2;
    public AudioSource playSound3;
    public GameManager gameManager;
    public Button playAgain;
    public Transform checkpoint;
    public Text scoreText;
    public Text livesText;
    public Text winText;
    private Animator animPlayer;
    public ParticleSystem bloodSplat;
    // Start is called before the first frame update
    void Start()
    {
        animPlayer = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        score = transform.position.z;
        scoreText.text = (score + scoreAdd).ToString("0");

        if(transform.position.y < -3.0f && lives > 0)
        {
            transform.position = checkpoint.position;
            transform.rotation = checkpoint.rotation;
        }
        else if (transform.position.y < -3.0f && lives == 0)
        {
            //playSound2.Play();
            ButtonActivator();
        }
    }

    void PlayerMovement()
    {
        if (playerAlive == true)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                animPlayer.SetFloat("Speed_f", 1.0f);
            }
            else
            {
                animPlayer.SetFloat("Speed_f", 0.0f);
            }
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                transform.Translate(Vector3.up + jump);
                isGrounded = false;
                animPlayer.SetTrigger("Jump_trig");
            }
            transform.Translate(Vector3.right * Time.deltaTime * sideSpeed * horizontalInput);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            playSound.Play();
        }
    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    void OnCollisionExit()
    {
        isGrounded = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            scoreAdd += 100;
            other.gameObject.SetActive(false);
            Debug.Log("Score: " + score);
        }
        else if(other.gameObject.CompareTag("Kill Block"))
        {
            Debug.Log("Lives: " + lives);
            lives = lives - 1;

            livesText.text = "Lives: " + lives;
            if (lives > 0)
            {
                transform.position = checkpoint.position;
                transform.rotation = checkpoint.rotation;
            }
            else if (lives == 0)
            {
                playSound3.gameObject.SetActive(false);
                playSound2.Play();
                ButtonActivator();
                animPlayer.SetBool("Death_b", true);
            }
        }
        else if(other.gameObject.CompareTag("Finish Line"))
        {
            Debug.Log("You Win!");
            winText.gameObject.SetActive(true);
            livesText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
            ButtonActivator();
            animPlayer.SetFloat("Speed_f", 0.0f);

        }
        else if(other.gameObject.CompareTag("Fall Detect"))
        {
            lives = lives - 1;
            Debug.Log("Lives: " + lives);
            livesText.text = "Lives: " + lives;
            if (lives == 0)
            {
                playSound3.gameObject.SetActive(false);
                playSound2.Play();
                bloodSplat.Play();
                Destroy(player);
            }

        }
    }
    void TaskOnClick()
    {
        gameManager.EndGame();
    }

    void ButtonActivator()
    {
        playerAlive = false;
        playAgain.gameObject.SetActive(true);
        playAgain.onClick.AddListener(TaskOnClick);
    }

    
}
