using UnityEngine;

public class Movement2D : MonoBehaviour
{
    //External Variables
    public float baseSpeed;
    public Rigidbody2D body;
    public float speedChange = 2f; 
    public float maxStamina = 15f; 


    //Internal Variables
    private float h;
    private float v;
    private bool isFlipped;
    private float currentStamina;
    private float speed;

    void Start()
    {
        currentStamina = maxStamina;
    }
    void Update()
    {
        //Input
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //Crouch and Sprint as well as the Stamina Code.
        //It doesn't seem like stamina currently works, will see what went wrong in the future
        int input = SprintInput();
        switch (input)
        {
            case 1:
                if (currentStamina > 0)
                {
                    speed = baseSpeed*speedChange;
                    currentStamina -= 1.5f*Time.deltaTime;
                }
                break;
            case 2:
                speed = baseSpeed / speedChange;
                if (currentStamina < maxStamina)
                {
                    currentStamina += 2*Time.deltaTime;
                }
                break;
            default:
                speed = baseSpeed; 
                if (currentStamina < maxStamina)
                {
                    currentStamina += Time.deltaTime;
                }
                break;
        }
        body.velocity = new Vector2(h * speed, v * speed );

        //Directional Character Facing 
        if (h > 0 && !isFlipped)
        {
            Flip();
        }
        else if (h < 0 && isFlipped)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(h * speed, v * speed);
    }

    void Flip()
    {
        isFlipped = !isFlipped;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    int SprintInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            return 1;
        }
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
}
