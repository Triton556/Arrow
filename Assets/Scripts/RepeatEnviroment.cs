using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatEnviroment : MonoBehaviour
{
    public GameObject repeatTrigger;
    public GameObject[] enviroment;

    private int i = 1;

    private Vector3 offset = new Vector3(0, 0, 2000f);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RepeatTrigger") && i == 1)
        {
            enviroment[0].transform.position += offset;
            i++;
            repeatTrigger.transform.position += offset / 2;
        }
        else if (other.CompareTag("RepeatTrigger") && i == 2)
        {
            enviroment[1].transform.position += offset;
            repeatTrigger.transform.position += offset / 2;
            i--;
        }
    }
}