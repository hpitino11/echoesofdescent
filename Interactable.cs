using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Outline outline;

    void Start()
    {
        // Ensure the outline is disabled initially
        if (outline != null)
        {
            outline.enabled = false;
        }
    }

    public void Interact()
    {
        // Implement interaction logic here
    }
}
