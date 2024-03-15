using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    [SerializeField] float speed = 0f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void MovePlayer(Vector3 direction){
        
        rb.velocity = direction * speed ;
    }
    public void MovePlayerTransform(Vector3 direction){
        transform.position += direction * Time.deltaTime *speed;
    }
}
