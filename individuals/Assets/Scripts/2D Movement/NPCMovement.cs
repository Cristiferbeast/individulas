using UnityEngine;


public class NPCMovement : MonoBehaviour
{
    //External Variables
    public float speed = 3f;             
    public float walkingDistance = 10f;
    public GameObject stopObject;  

    //Internal Variables
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool walkingForward = true;
    private bool isFlipped;
    private bool interrupted; 

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.left * walkingDistance;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
        {
            walkingForward =! walkingForward;
            if (walkingForward){
                targetPosition = startPosition + Vector3.left * walkingDistance;
                Flip();
            }
            if (!walkingForward){
                targetPosition = startPosition;
                Flip();
            }
        }
    }

    //Internal Functions
    void Flip()
    {
        isFlipped = !isFlipped;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
