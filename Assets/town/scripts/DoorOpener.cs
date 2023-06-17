using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private Animator animator = null;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("door open");
        animator.SetBool("open", true);
    }

    //void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("door close");
    //    animator.SetBool("open", false);
    //}
}
