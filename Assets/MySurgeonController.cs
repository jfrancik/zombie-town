using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySurgeonController : MonoBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        gameManager.showSurgeonMonolog();
    }

    void OnTriggerExit(Collider other)
    {
        gameManager.hideSurgeonMonolog();
    }
}
