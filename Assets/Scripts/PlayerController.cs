using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator arrowAnimator;
    private Animator bowAnimator;

    private AudioSource audioSource;
    public AudioClip ropePull, fireSound, logHit, scoreSound;

    private SpawnManager spawnManager;

    public GameObject bow;

    public TextMeshProUGUI gameOverText, startText, instructionText, scoreText;

    public Button restartButton;
    
    private Vector3 mainCameraPos;

    private float speed = 7f;

    public bool gameStarted = false;
    private bool fireSoundPlayed = false;
    public bool gameOver;


    // Start is called before the first frame update
    void Start()
    {
        arrowAnimator = gameObject.GetComponent<Animator>();
        bowAnimator = bow.gameObject.GetComponent<Animator>();
        
        audioSource = GetComponent<AudioSource>();
        
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameStarted)
            spaceToStart();

        //Hides startText
        if (gameStarted)
        {
            startText.gameObject.SetActive(false);
        }
        
        //moves arrow to the left
        if (gameStarted && !gameOver)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        if (IsAnimationPlaying("Base Layer.fire"))
        {
            StartCoroutine(ArrowToFlyCountdownRoutine(0.36f));
        }
        
        
        if (gameOver)
            GameOver();
    }

    //after sec seconds plays fire sound and sets arrow animation to fire, and game started
    IEnumerator ArrowToFlyCountdownRoutine(float sec)
    {
        yield return new WaitForSeconds(sec);
        if (!fireSoundPlayed)
        {
            audioSource.PlayOneShot(fireSound, 0.3f);
            fireSoundPlayed = true;
        }

        gameStarted = true;
        arrowAnimator.SetBool("GameStarted", true);
        arrowAnimator.SetTrigger("Fire");
    }

    //check which animation is playing
    public bool IsAnimationPlaying(string animationName)
    {
        //get information about which animation is playing right now
        var bowAnimatorStateInfo = bowAnimator.GetCurrentAnimatorStateInfo(0);

        //if it matches needed name, returns true
        if (bowAnimatorStateInfo.IsName(animationName))
            return true;
        else
        {
            return false;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        //When arrow hits log gameover and obstacle becomes arrows parent
        if (other.gameObject.CompareTag("Obstacle"))
        {
            audioSource.PlayOneShot(logHit, 5f);
            gameOver = true;
            transform.parent = other.transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //counts scores, appears score text and plays score sound
        if (other.gameObject.CompareTag("ScoreTrigger"))
        {
            audioSource.PlayOneShot(scoreSound, 3f);
            scoreText.gameObject.SetActive(true);
            spawnManager.UpdateScore(1);
            instructionText.gameObject.SetActive(false);
        }
            
            
    }

    //when space pressed first time, set triggers on animators, and game is started
    void spaceToStart()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            arrowAnimator.SetTrigger("Fire");
            bowAnimator.SetTrigger("Fire");

            audioSource.PlayOneShot(ropePull, 1f);
        }
    }

    //game over text appear
    void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        arrowAnimator.enabled = false;
        gameObject.GetComponent<Rigidbody>().constraints = gameObject.GetComponent<Rigidbody>().constraints | RigidbodyConstraints.FreezePositionZ;
    }

    //reload current scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   
}