using UnityEngine;

public class SpinnerController : MonoBehaviour
{
    public float spinSpeed = 10f;
    private Vector3 lastMousePosition;
    private bool isSpinning = false;
    private float currentSpinSpeed = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
            currentSpinSpeed = 0f;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            if (delta.magnitude > 1f) // A threshold to ensure a quick swipe is detected
            {
                currentSpinSpeed = delta.x * spinSpeed;
                isSpinning = true;
            }
            lastMousePosition = Input.mousePosition;
        }

        if (isSpinning)
        {
            transform.Rotate(Vector3.up, currentSpinSpeed * Time.deltaTime);
            currentSpinSpeed = Mathf.Lerp(currentSpinSpeed, 0f, Time.deltaTime * 2f); // Gradually slows down the spin
            if (Mathf.Abs(currentSpinSpeed) < 0.1f)
            {
                isSpinning = false;
            }
        }
    }
}