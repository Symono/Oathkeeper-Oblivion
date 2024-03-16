using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Movement movementScript;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        movementScript.MovePlayer(horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movementScript.Jump();
        }
    }
}

