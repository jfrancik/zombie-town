using UnityEngine;
using UnityEngine.AI;

public class MyCharacterController : MonoBehaviour
{
    public float turnSpeed = 45;
    public float walkForwardSpeed = 1;
    public float runForwardSpeed = 3;
    public float walkBackSpeed = 0.5f;
    public float gravity = -15;

    CharacterController characterController;
    Animator animator;
    GameManager gameManager;

    Vector3 move = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // nothing happens until first click
        if (!gameManager.isGameStarted)
            return;

        if (gameManager.isGameOver)
        {
            animator.SetFloat("speed", 0);
            animator.SetBool("isTurningLeft", false);
            animator.SetBool("isTurningRight", false); 
            animator.SetBool("gameOver", true);
            return;
        }

        // read input
        float forward = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");
        bool run = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // check ground contact
        bool isGrounded = characterController.isGrounded;

        // setup speed
        float speed = 0;
        if (forward > 0)
        {
            speed = forward * walkForwardSpeed;
            if (run) speed = forward * runForwardSpeed;
        }
        else
            speed = forward * walkBackSpeed;

        // rotate character
        transform.Rotate(new Vector3(0, turn * turnSpeed * Time.deltaTime));

        // move character forward
        if (isGrounded)
            move = transform.forward * speed;
        
        // add up gravity
        move += transform.up * gravity * Time.deltaTime;

        // actually, move it now!
        characterController.Move(move * Time.deltaTime);

        // setup animations
        animator.SetFloat("speed", speed);
        animator.SetBool("isTurningLeft", turn < 0);
        animator.SetBool("isTurningRight", turn > 0);

        // NavMesh
        NavMeshHit navhit;
        gameManager.isPlayerSafe = !NavMesh.SamplePosition(transform.position, out navhit, 0.1f, NavMesh.AllAreas);
    }
}
