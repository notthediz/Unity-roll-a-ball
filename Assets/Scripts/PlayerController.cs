using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player

    private Rigidbody rb;

    // Count collectibles

    private int count;
    
    // Movement along X and Y axis

    private float movementX;
    private float movementY;

    // Speed at which the player moves

    public float speed = 0;

    public TextMeshProUGUI countText;

    public GameObject winTextObject;

    // Start is called once before the first frame update
    void Start()
    {
 
        // Get and store the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();

        count = 0;

        SetCountText();

        winTextObject.SetActive(false);

    }

    // This function is called when a move input is detected
    void OnMove(InputValue MovementValue)
    {
        // Convert the input value into a Vector2 for movement
        Vector2 movementVector = MovementValue.Get<Vector2>();
         
        //Store the X and Y components of movement
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 8)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    // FixedUpdate is called once per fixed frame-rate frame
    void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the RigidBody to move the player
        rb.AddForce(movement * speed);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            Destroy(gameObject);
            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";

        }
    }

}
