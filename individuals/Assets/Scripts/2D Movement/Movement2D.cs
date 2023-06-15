using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body;
    float h;
    float v;

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(h * speed, v * speed);  
    }
}