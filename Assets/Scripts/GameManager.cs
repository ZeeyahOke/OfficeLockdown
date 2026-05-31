using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI messageText;

    [Header("Timer")]
    public float timeLimit = 180f;

    [Header("Audio")]
    public AudioClip solveSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    private int puzzlesSolved = 0;
    private float timeRemaining;
    private bool gameOver = false;
    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timeRemaining = timeLimit;
        audioSource = gameObject.AddComponent<AudioSource>();
        messageText.text = "";
        UpdateProgressUI();
    }

    void Update()
    {
        if (gameOver) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            Lose();
        }

        UpdateTimerUI();
    }

    public bool IsSolved(int step)
    {
        return puzzlesSolved >= step;
    }

    public bool CanAttempt(int step)
    {
        if (gameOver) return false;
        return puzzlesSolved == step - 1;
    }

    public void SolvePuzzle(int step)
    {
        if (puzzlesSolved >= step) return;

        puzzlesSolved = step;
        UpdateProgressUI();

        if (solveSound != null)
            audioSource.PlayOneShot(solveSound);

        if (puzzlesSolved >= 5)
            Win();
    }

    void UpdateProgressUI()
    {
        progressText.text = "Progress: " + puzzlesSolved + " / 5";
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = minutes + ":" + seconds.ToString("00");
    }

    void Win()
    {
        gameOver = true;
        messageText.text = "You Escaped!";
        if (winSound != null)
            audioSource.PlayOneShot(winSound);
        Cursor.lockState = CursorLockMode.None;
    }

    void Lose()
    {
        gameOver = true;
        messageText.text = "Time's Up!";
        if (loseSound != null)
            audioSource.PlayOneShot(loseSound);
        Cursor.lockState = CursorLockMode.None;
    }
}
