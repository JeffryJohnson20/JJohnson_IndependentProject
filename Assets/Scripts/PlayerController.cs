using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 15.0f;
    public float sideSpeed = 15.0f;
    private bool isGrounded;
    private float horizontalInput;
    private Vector3 jump = new Vector3(0, 1, 0);
    private int lives = 3;
    private float score = 0;
    private float scoreAdd = 0;
    public AudioSource playSound;
    public AudioSource playSound2;
    public GameManager gameManager;
    public Transform checkpoint;
    public Text scoreText;
    public Text livesText;
    public Text winText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            transform.Translate(Vector3.up + jump);
            isGrounded = false;
        }
        transform.Translate(Vector3.right * Time.deltaTime * sideSpeed * horizontalInput);
        score = transform.position.z;
        scoreText.text = (score + scoreAdd).ToString("0");

        if(transform.position.y < -3.0f)
        {
            transform.position = checkpoint.position;
            transform.rotation = checkpoint.rotation;
            if(lives == 0)
            {
                gameManager.EndGame();
                playSound2.Play();
            }
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
        if(other.gameObject.CompareTag("Kill Block"))
        {
            Debug.Log("Lives: " + lives);
            lives = lives - 1;

            livesText.text = "Lives: " + lives;

            transform.position = checkpoint.position;
            transform.rotation = checkpoint.rotation;

            if (lives == 0)
            {
                gameManager.EndGame();
                playSound2.Play();
            }
        }
        if(other.gameObject.CompareTag("Finish Line"))
        {
            Debug.Log("You Win!");
            winText.gameObject.SetActive(true);
            livesText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);

        }
        if(other.gameObject.CompareTag("Fall Detect"))
        {
            lives = lives - 1;
            Debug.Log("Lives: " + lives);
            livesText.text = "Lives: " + lives;

        }
    }
}
