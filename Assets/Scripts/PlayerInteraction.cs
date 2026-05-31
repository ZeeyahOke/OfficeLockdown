using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float reachDistance = 5f;
    public Color highlightColor = Color.yellow;

    private ClickableObject currentTarget;

    void Update()
    {
        // Raycast from center of screen
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            ClickableObject clickable = hit.collider.GetComponent<ClickableObject>();

            if (clickable != null)
            {
                // New target - highlight it
                if (currentTarget != clickable)
                {
                    ClearHighlight();
                    currentTarget = clickable;
                    currentTarget.Highlight(highlightColor);
                }

                // Click to interact
                if (Input.GetMouseButtonDown(0))
                {
                    currentTarget.Interact();
                }
            }
            else
            {
                ClearHighlight();
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    void ClearHighlight()
    {
        if (currentTarget != null)
        {
            currentTarget.RemoveHighlight();
            currentTarget = null;
        }
    }
}
