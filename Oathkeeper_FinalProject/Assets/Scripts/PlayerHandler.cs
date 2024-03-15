using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Movement movementScript;
    // Update is called once per frame
    void Update()
    {
      Vector3 input =Vector3.zero;
      if(Input.GetKey(KeyCode.A)){
        input.x += -1;
      }
      if(Input.GetKey(KeyCode.D)){
        input.x += 1;
      }
      movementScript.MovePlayer(input);  
    }
}