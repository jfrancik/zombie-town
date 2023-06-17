using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameStarted;
    public bool isCodeTaken;
    public bool isVaccineTaken;
    public bool isGameWon;
    public bool isGameOver;

    public bool isPlayerSafe;
    public int nZombies;

    public Animator HouseDoor;
    public Animator TowerDoor;
    public Animator MovingPlatform;
    public Animator Monument;

    public TextMeshProUGUI textBeware, textInstructions, textRemember, textShift;
    public TextMeshProUGUI textSurgeon1, textSurgeon2, textSurgeon3, textSurgeon4, textSurgeon5, textSurgeon6;
    public TextMeshProUGUI textNoAccess, textAcceessGranted;
    public TextMeshProUGUI textCemeteryWelcome;
    public TextMeshProUGUI textCongratulations, textFinalInstruction;
    public TextMeshProUGUI textGameOver, textGameWon;
    public Image imageCodeBig, imageCodeIcon;

    // This is how other game objects can easily access this Game Manager:
    // GameManager.Instance
    public static GameManager Instance { get; private set; }


    // This functionality ensures that other game objects can easily access this Game Manager
    // It initialises and processes GameManager.Instance calls
    private void Awake()
    {
        // Set the instance to this object when it is first created
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // Destroy duplicate GameManager instances
    }

    /// <summary>
    /// Basic Services available for other Game Objects (and not only).
    /// Note: some of them use co-routines to do some stuff in parallel with your regular gameplay
    /// </summary>

    // Shows the introductory instructions - used by the Game Manager itself
    public void initGame()
    {
        isGameStarted = isPlayerSafe = isCodeTaken = isVaccineTaken = isGameWon = isGameOver = false;
        nZombies = 0;
        textBeware.gameObject.SetActive(true);
    }

    // Starts the game and displays additional instructions - used by the Game Manager itself
    public void startGame()
    {
        textBeware.gameObject.SetActive(false);
        isGameStarted = true;
        HouseDoor.SetBool("open", true);
        StartCoroutine(coroutineShowAdditionalInfo());
    }

    private IEnumerator surgeonCoroutine = null;

    // Starts the monolog - used by the Surgeon
    public void showSurgeonMonolog()
    {
        surgeonCoroutine = coroutineShowSurgeonMonolog();
        StartCoroutine(surgeonCoroutine);
    }

    // Stops the monolog - used by the Surgeon
    public void hideSurgeonMonolog()
    {
        if (surgeonCoroutine != null)
            StopCoroutine(surgeonCoroutine);

        textSurgeon1.gameObject.SetActive(false);
        textSurgeon2.gameObject.SetActive(false);
        textSurgeon3.gameObject.SetActive(false);
        textSurgeon4.gameObject.SetActive(false);
        textSurgeon5.gameObject.SetActive(false);
        textSurgeon6.gameObject.SetActive(false);
        imageCodeBig.gameObject.SetActive(false);
    }

    // Shows the No Access info - used by the Hospital Gate
    public void showNoAccess()
    {
        textNoAccess.gameObject.SetActive(true);
    }

    // Hides the No Access info - used by the Hospital Gate
    public void hideNoAccess()
    {
        textNoAccess.gameObject.SetActive(false);
    }

    // Shows and then hides the Access Granted info - used by the Hospital Gate
    // Internally, starts a co-routine to complete the task
    public void showAccessGranted()
    {
        StartCoroutine(coroutineShowAccessGranted());
    }

    // Shows and then hides Cemetery Welcome message - used by the Cemetery
    // Internally, starts a co-routine to complete the task
    public void showCemeteryWelcome()
    {
        StartCoroutine(coroutineShowCemeteryWelcome());
    }

    // Shows ongratulatiuons and activates the lift/moving platform - called by the UpperGate on finding the vaccine
    // Internally, starts a co-routine to complete the task
    public void showCongratulations()
    {
        StartCoroutine(coroutineShowCongratulations());
        MovingPlatform.SetBool("activate", true);
    }

    // Shows ongratulatiuons and activates the lift/moving platform - called by the UpperGate on finding the vaccine
    // Internally, starts a co-routine to complete the task
    public void startFinalSequence()
    {
        StartCoroutine(coroutineStartFinalSequence());
        MovingPlatform.SetBool("boarded", true);
        Monument.SetBool("activate", true);
    }

    // GAME OVER! - called by zombies when very close to the player!
    public void gameOver()
    {
        isGameOver = true;
        textGameOver.gameObject.SetActive(true);
    }

    public void gameWon()
    {
        isGameWon = true;
        textGameWon.gameObject.SetActive(true);
    }

    /// <summary>
    /// Co-Routines
    /// These functions are performed in parallel with the regular gameplay
    /// </summary>
    /// <returns></returns>

    private IEnumerator coroutineShowAdditionalInfo()
    {
        textInstructions.gameObject.SetActive(true);
        yield return new WaitForSeconds(6);
        textInstructions.gameObject.SetActive(false);

        yield return new WaitForSeconds(3);
        textRemember.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        textRemember.gameObject.SetActive(false);

        yield return new WaitForSeconds(1);
        textShift.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        textShift.gameObject.SetActive(false);
    }

    private IEnumerator coroutineShowSurgeonMonolog()
    {
        textSurgeon1.gameObject.SetActive(true);
        yield return new WaitForSeconds(6);
        textSurgeon1.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        textSurgeon2.gameObject.SetActive(true);
        yield return new WaitForSeconds(8);
        textSurgeon2.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        textSurgeon3.gameObject.SetActive(true);
        yield return new WaitForSeconds(7);
        textSurgeon3.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        textSurgeon4.gameObject.SetActive(true);
        yield return new WaitForSeconds(10);
        textSurgeon4.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        textSurgeon5.gameObject.SetActive(true);
        yield return new WaitForSeconds(6);
        imageCodeBig.gameObject.SetActive(true);
        yield return new WaitForSeconds(8);
        textSurgeon5.gameObject.SetActive(false);
        imageCodeBig.gameObject.SetActive(false);

        imageCodeIcon.gameObject.SetActive(true);
        isCodeTaken = true;

        yield return new WaitForSeconds(0.5f);
        textSurgeon6.gameObject.SetActive(true);
        yield return new WaitForSeconds(8);
        textSurgeon6.gameObject.SetActive(false);
    }

    private IEnumerator coroutineShowAccessGranted()
    {
        textAcceessGranted.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        textAcceessGranted.gameObject.SetActive(false);
    }

    private IEnumerator coroutineShowCemeteryWelcome()
    {
        textCemeteryWelcome.gameObject.SetActive(true);
        yield return new WaitForSeconds(15);
        textCemeteryWelcome.gameObject.SetActive(false);
    }

    private IEnumerator coroutineShowCongratulations()
    {
        textCongratulations.gameObject.SetActive(true);
        yield return new WaitForSeconds(6);
        textCongratulations.gameObject.SetActive(false);

        textFinalInstruction.gameObject.SetActive(true);
        yield return new WaitForSeconds(6);
        textFinalInstruction.gameObject.SetActive(false);
    }

    private IEnumerator coroutineStartFinalSequence()
    {
        yield return new WaitForSeconds(17);
        gameWon();
    }


    /// <summary>
    /// GameManager core implementation
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        initGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted && Input.GetMouseButtonDown(0))
            startGame();
    }
}
