using UnityEngine;

public class GeneratorInteract : MonoBehaviour
{
    public Camera puzzleCamera; // Assign your camera in the inspector.
    public GameObject puzzle; // Assign your puzzle GameObject in the inspector.
    public Animator generatorAnimator; // Assign your generator's animator in the inspector.
    public Vector3 zoomedPosition; // Define a suitable zoomed-in position based on your scene.
    
    private bool isAtGenerator = false;
    private bool isPuzzleActive = false;
    private Vector3 originalCameraPosition;

    private void Start()
    {
        // Store the original camera position.
        originalCameraPosition = puzzleCamera.transform.position;
    }

    private void Update()
    {
        if (isAtGenerator && Input.GetKeyDown(KeyCode.E))
        {
            if (isPuzzleActive)
            {
                // Deactivate the puzzle, move the camera back to its original position.
                puzzleCamera.transform.position = originalCameraPosition;
                puzzle.SetActive(false);
                generatorAnimator.SetTrigger("Cup");
                isPuzzleActive = false;
            }
            else
            {
                // Activate the puzzle, move the camera closer to the generator/puzzle.
                puzzleCamera.transform.position = zoomedPosition;
                puzzle.SetActive(true);
                isPuzzleActive = true;
            }
        }
    }

    // You will need to add logic to set isAtGenerator to true when the player is near the generator.
    // This might be done using OnTriggerStay/OnTriggerEnter and OnTriggerExit with a Collider around the generator.
}

