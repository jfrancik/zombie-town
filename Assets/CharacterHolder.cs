using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHolder : MonoBehaviour
{
    // When something collides with the moving platform, make the moving platform the parent
    void OnTriggerEnter(Collider other)
    {
        other.transform.parent = gameObject.transform;

        GameManager gameManager = GameManager.Instance;
        gameManager.startFinalSequence();
    }

    void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
