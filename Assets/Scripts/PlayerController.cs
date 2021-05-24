using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public float speed = 15.0f;
    public float sideSpeed = 15.0f;
    private int timer = 0;
    private bool isGrounded;
    public bool playerAlive = true;
    private bool hasPowerUp = false;
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
    public Button exitGame;
    public Button exitMenu;
    public Transform checkpoint;
    public Text scoreText;
    public Text livesText;
    public Text winText;
    public Text timeText;
    private Animator animPlayer;
    public ParticleSystem bloodSplat;
    public GameObject speedPart;
    private bool playerWin = false;
    // Start is called before the first frame update
    void Start()
    {
        animPlayer = GetComponent<Animator>();
        StartCoroutine(TimeCounter());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        score = transform.position.z;
        if (score < 0)
        {
            score = 0;
        }
        scoreText.text = (score + scoreAdd).ToString("0");
        if (playerWin == false)
        {
            timeText.text = "Time Bonus: " + ((20 - timer) * 20).ToString("0");
        }
        


        if (transform.position.y < -3.0f && lives > 0)
        {
            transform.position = checkpoint.position;
            transform.rotation = checkpoint.rotation;
        }

        Debug.Log(timer);
        
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
            //scoreText.gameObject.SetActive(false);
            ButtonActivator();
            animPlayer.SetFloat("Speed_f", 0.0f);
            scoreAdd += (20 - timer) * 20;
            playerWin = true;

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
                ButtonActivator();
                Destroy(player);
            }

        }

        if (other.gameObject.CompareTag("Power Up"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdown());
            speedPart.SetActive(true);
            speed = 20.0f;
            sideSpeed = 20.0f;
        }
    }
    void RestartOnClick()
    {
        gameManager.Restart();
    }

    void QuitOnClick()
    {
        gameManager.EndGame();
    }

    void ExitToMenuClick()
    {
        gameManager.ExitToMenu();
    }

    void ButtonActivator()
    {
        playerAlive = false;
        playAgain.gameObject.SetActive(true);
        playAgain.onClick.AddListener(RestartOnClick);
        exitGame.gameObject.SetActive(true);
        exitGame.onClick.AddListener(QuitOnClick);
        exitMenu.gameObject.SetActive(true);

    }

    IEnumerator PowerUpCountdown()
    {
        //speed = 20.0f;
        yield return new WaitForSeconds(5);
        hasPowerUp = false;
        speedPart.SetActive(false);
        speed = 15.0f;
        sideSpeed = 15.0f;
    }

    IEnumerator TimeCounter()
    {
        for(timer = 0; timer < 20; timer++)
        {
            yield return new WaitForSeconds(1);
        }
    }

    
}
