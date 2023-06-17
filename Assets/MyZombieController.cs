using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class MyZombieController : MonoBehaviour
{
    public float walkForwardSpeed = 0.375f;
    public float runForwardSpeed = 2;

    NavMeshAgent agent;                 // nav mesh AI agent
    Animator animator;                  // animator object
    GameManager gameManager;            // Game Manager object

    // Targets
    public Transform player;            // player character
    public Transform[] randomTargets;   // an array of random targets
    
    Transform target;                   // currently selected random target other than the player

 
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // set first target
        target = randomTargets[Random.Range(0, randomTargets.Length - 1)];
    }

    // Update is called once per frame
    void Update()
    {
        // nothing happens until first click
        if (!gameManager.isGameStarted)
            return;

        // if the game is won, zombies stop and look around, healed
        if (gameManager.isGameWon)
        {
            animator.SetBool("isHealed", true);
            return;
        }

        // check if the player is spotted
        // using Raycast mechanics - similar to what we used in Interactions activity
        bool bPlayerSpotted = false;  // this means we initially think player is not spotted
        // directio in which we will cast the ray
        Vector3 direction = player.position - transform.position;
        // this variable will contain ray cast test result
        RaycastHit hit;
        // cast the actual ray
        if (Physics.Raycast(transform.position + Vector3.up, direction, out hit, 20))
            // check if the ray hit the player - this can be any kind of obstacle as well
            if (hit.transform == player)
                bPlayerSpotted = true;    // PLAYER SPOTTED!

        // if the player on unwalkable areas (inside the buildings, on the grass or on the monument)
        // or, if the game is over
        if (gameManager.isPlayerSafe || gameManager.isGameOver)
            bPlayerSpotted = false;

        if (bPlayerSpotted)
        {
            // if the player spotted, use NavMesh to chase them
            agent.SetDestination(player.position);
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance < 1)
                gameManager.gameOver();
        }
        else
        {
            // if not, follow your boring random target
            agent.SetDestination(target.position);
            // at some close distance from the target, change it to another equally boring random target
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < 4)
                target = randomTargets[Random.Range(0, randomTargets.Length - 1)];
        }

        // setup speed
        float speed = walkForwardSpeed;                 // normal walking speed
        if (bPlayerSpotted) speed = runForwardSpeed;    // or running if the player spotted
        agent.speed = speed;                            // setup the NavMesh agent
        animator.SetFloat("speed", speed);              // setup the animator
    }
}
