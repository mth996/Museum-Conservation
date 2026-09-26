using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonCanvasController : MonoBehaviour
{
    public Canvas[] canvases; // Array of canvases to activate/deactivate
    public Button[] buttons; // Array of main buttons
    public Button[] backButtons; // Array of back buttons for each canvas

    public GameObject targetObject; // Object to rotate
    public Vector2[] buttonRotations; // Rotations for each button [X, Y]
    public float rotationDuration = 0.5f; // Duration for smooth rotation
    
    public Canvas descriptionCanvas; // Canvas to deactivate when any main canvas is active

    private Coroutine rotationCoroutine; // Store the running rotation coroutine

   private void Start()
    {
        // Assign main button click events
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Prevent closure issue
            buttons[i].onClick.AddListener(() => ActivateCanvasAndRotate(index));
        }

        // Assign back button click events
        for (int i = 0; i < backButtons.Length; i++)
        {
            backButtons[i].onClick.AddListener(ResetRotationAndDeactivateAll);
        }
    }

    private void ActivateCanvasAndRotate(int index)
    {
        // Deactivate all canvases
        foreach (Canvas canvas in canvases)
        {
            canvas.gameObject.SetActive(false);
        }

        // Activate selected canvas
        canvases[index].gameObject.SetActive(true);

        // Deactivate descriptionCanvas if any main canvas is active
        descriptionCanvas.gameObject.SetActive(false);

        // Start smooth rotation to the target rotation
        Vector3 targetRotation = new Vector3(buttonRotations[index].x, buttonRotations[index].y, 0);
        StartRotation(targetRotation);
    }

    private void StartRotation(Vector3 targetRotation)
    {
        // Stop any ongoing rotation coroutine
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
        }

        // Start a new rotation coroutine
        rotationCoroutine = StartCoroutine(SmoothRotate(targetRotation));
    }

    private IEnumerator SmoothRotate(Vector3 targetRotation)
    {
        Quaternion startRotation = targetObject.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(targetRotation);
        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            // Lerp between start and end rotations
            targetObject.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is set precisely
        targetObject.transform.rotation = endRotation;
    }

    private void ResetRotationAndDeactivateAll()
    {
        // Reset rotation smoothly to (0, 0, 0)
        StartRotation(Vector3.zero);

        // Deactivate all canvases in the array
        foreach (Canvas canvas in canvases)
        {
            canvas.gameObject.SetActive(false);
        }

        // Reactivate descriptionCanvas when all main canvases are deactivated
        descriptionCanvas.gameObject.SetActive(true);
    }
}