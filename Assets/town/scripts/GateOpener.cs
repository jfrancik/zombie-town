using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GateOpener : MonoBehaviour
{
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
        if (gameManager.isCodeTaken)
        {
            animator.SetBool("open", true);
            gameManager.showAccessGranted();
        }
        else
            gameManager.showNoAccess();
    }

    void OnTriggerExit(Collider other)
    {
        gameManager.hideNoAccess();
    }
}
