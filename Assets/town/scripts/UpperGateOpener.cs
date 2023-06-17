using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperGateOpener : MonoBehaviour
{
    public GameObject vaccine;

    private Animator animator = null;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        animator.SetBool("open", true);
        vaccine.SetActive(false);
        gameManager.showCongratulations();
    }
}
