using UnityEngine;
using TMPro;

public class ClickableObject : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public int puzzleStep = 1;

    [Header("Step 1 - Light Switch")]
    public Light roomLight;

    [Header("Step 2 - Computer")]
    public GameObject computerPanel;

    [Header("Step 3 - Drawer")]
    public Vector3 drawerOpenOffset = new Vector3(0f, 0f, -0.3f);

    [Header("Step 3 - Riddle UI")]
    public GameObject riddlePanel;

    [Header("Step 4 - Safe")]
    public TMP_InputField safeInput;
    public string correctAnswer = "clock";
    public GameObject safePanel;

    [Header("Step 5 - Door")]
    public float doorOpenAngle = 90f;

    [Header("Audio")]
    public AudioClip interactSound;

    // Highlight
    private Renderer objectRenderer;
    private Color originalColor;
    private bool isHighlighted = false;
    private bool isSolved = false;
    private AudioSource audioSource;

    void Start()
    {
        objectRenderer = GetComponentInChildren<Renderer>();
        if (objectRenderer != null)
            originalColor = objectRenderer.material.color;

        audioSource = gameObject.AddComponent<AudioSource>();

        // Hide UI panels at start
        if (riddlePanel != null) riddlePanel.SetActive(false);
        if (safePanel != null) safePanel.SetActive(false);
    }

    public void Highlight(Color color)
    {
        if (isSolved) return;
        if (!GameManager.Instance.CanAttempt(puzzleStep)) return;

        if (objectRenderer != null)
        {
            objectRenderer.material.color = color;
            isHighlighted = true;
        }
    }

    public void RemoveHighlight()
    {
        if (objectRenderer != null && isHighlighted)
        {
            objectRenderer.material.color = originalColor;
            isHighlighted = false;
        }
    }

    public void Interact()
    {
        if (isSolved) return;
        if (!GameManager.Instance.CanAttempt(puzzleStep)) return;

        if (interactSound != null)
            audioSource.PlayOneShot(interactSound);

        switch (puzzleStep)
        {
            case 1: SolveLight(); break;
            case 2: SolveComputer(); break;
            case 3: SolveDrawer(); break;
            case 4: OpenSafePanel(); break;
            case 5: SolveDoor(); break;
        }
    }

    // --- Step 1: Turn on the light ---
    void SolveLight()
    {
        if (roomLight != null)
            roomLight.enabled = true;

        isSolved = true;
        RemoveHighlight();
        GameManager.Instance.SolvePuzzle(1);
    }

    // --- Step 2: Turn on computer screen ---
    void SolveComputer()
    {
        if (computerPanel != null)
            computerPanel.SetActive(true);

        isSolved = true;
        RemoveHighlight();
        GameManager.Instance.SolvePuzzle(2);
    }

    // --- Step 3: Open drawer and show riddle ---
    void SolveDrawer()
    {
        transform.position += drawerOpenOffset;

        if (riddlePanel != null)
            riddlePanel.SetActive(true);

        isSolved = true;
        RemoveHighlight();
        GameManager.Instance.SolvePuzzle(3);
    }

    // --- Step 4: Show safe input panel ---
    void OpenSafePanel()
    {
        if (safePanel != null)
        {
            safePanel.SetActive(true);
            safeInput.Select();
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Call this from a UI button next to the input field
    public void CheckSafeAnswer()
    {
        if (safeInput == null) return;

        string answer = safeInput.text.Trim().ToLower();

        if (answer == correctAnswer.ToLower())
        {
            safePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isSolved = true;
            RemoveHighlight();
            GameManager.Instance.SolvePuzzle(4);
        }
        else
        {
            safeInput.text = "";
            safeInput.placeholder.GetComponent<TextMeshProUGUI>().text = "Wrong! Try again...";
        }
    }

    // --- Step 5: Open the door ---
    void SolveDoor()
    {
        transform.Rotate(0f, doorOpenAngle, 0f);

        isSolved = true;
        RemoveHighlight();
        GameManager.Instance.SolvePuzzle(5);
    }
}
