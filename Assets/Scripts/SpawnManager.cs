using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject branches;
    public GameObject arrow;

    private PlayerController playerController;
    public TextMeshProUGUI scoreText;

    private int score;
    public float randomSpawnTime;
    public float upperTimeBound = 2f;
    public float bottomTimeBound = 0.7f;
    private float upperPosBound = 33f;
    private float bottomPosBound = 23f;
    private float branchSpawnPosY;
    private float lastSpawnPosY;

    private bool lastSpawn;

    // Start is called before the first frame update
    void Start()
    {
        playerController = arrow.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.gameStarted && !playerController.gameOver)
        {
            transform.position = arrow.transform.position + new Vector3(-1, 0, 25f);
            if (lastSpawn == false)
            {
                randomSpawnTime = Random.Range(bottomTimeBound, upperTimeBound);
                StartCoroutine(SpawnCountdownRoutine(randomSpawnTime));
            }

            lastSpawn = true;
        }
    }

    IEnumerator SpawnCountdownRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        branchSpawnPosY = Random.Range(bottomPosBound, upperPosBound) + branches.transform.position.y;
        if ((branchSpawnPosY - lastSpawnPosY) > 5f)
        {
            branchSpawnPosY -= 3f;
        }
        else if ((branchSpawnPosY - lastSpawnPosY) < 5f)
        {
            branchSpawnPosY += 3f;
        }

        if (!playerController.gameOver)
            Instantiate(branches, new Vector3(transform.position.x, branchSpawnPosY,
                transform.position.z), branches.transform.rotation);
        branchSpawnPosY = Random.Range(bottomPosBound, upperPosBound);
        lastSpawn = false;
        lastSpawnPosY = branchSpawnPosY;
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "" + score;
    }
}