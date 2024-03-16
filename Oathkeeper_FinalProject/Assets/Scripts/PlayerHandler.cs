using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Movement movementScript;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        movementScript.MovePlayer(horizontalInput);

        // Only allow jumping if the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && movementScript.IsGrounded())
        {
            movementScript.Jump();
        }
    }
}
