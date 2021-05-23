using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowArrow : MonoBehaviour
{
    public GameObject arrow;
    private PlayerController playerController;
    public Vector3 offset = new Vector3(19f, 0, 12.2f);
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
            transform.position = arrow.transform.position + offset;
        }
    }
    
}
