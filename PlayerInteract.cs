using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private Interactable currentSelection; // Store the current interactable object

    void Start()
    {
        cam = GetComponent<Movement>().playerCam;
    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            var selection = hitInfo.transform.GetComponent<Interactable>();
            if (selection != null)
            {
                selection.outline.enabled = true;
                currentSelection = selection; // Update the current selection
            }
        }
        else if (currentSelection != null)
        {
            // If the raycast is not hitting any interactable object, disable the outline
            currentSelection.outline.enabled = false;
            currentSelection = null;
        }
    }
}

