using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBounce : MonoBehaviour
{
    public ParticleSystem appearParticles;
    private PlayerController playerController;
    
    Vector3 lastPos;

    public float force;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(appearParticles, transform.position, appearParticles.transform.rotation);
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private Vector3 startPos;

    void Update()
    {
        startPos = transform.position;
        //adds force to obstacles when space pressed
        if (playerController.gameStarted && Input.GetKeyDown(KeyCode.Space) && !playerController.gameOver)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Impulse);
        }

        BoundsKeeping();

        DragIncrease();
        lastPos = transform.position;
    }
    
    //Increase air friction when obstacle goes up
    void DragIncrease()
    {
        if ((startPos - lastPos).y > 0f)
        {
            gameObject.GetComponent<Rigidbody>().drag = 2f;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().drag = 0f;
        }
        
    }

    //Prevent logs from falling through the ground
    void BoundsKeeping()
    {
        if (transform.position.y < 0f)
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
        else if (transform.position.y > 20f)
        {
            transform.position = new Vector3(transform.position.x, 20f, transform.position.z);
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * force / 10f, ForceMode.Impulse);
        }
    }
}