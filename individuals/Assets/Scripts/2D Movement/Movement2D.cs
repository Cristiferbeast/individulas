using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        body.velocity = new Vector2(h * speed, v * speed);
    }
}