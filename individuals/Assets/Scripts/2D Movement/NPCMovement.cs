using UnityEngine;


public class NPCMovement : MonoBehaviour
{
    //External Variables
    public float speed = 3f;             
    public float walkingDistance = 10f;
    public string aggresion = "readied";
    public Transform playerTransform;
    public float detectionRange = 20f;
    

    //Internal Variables
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool walkingForward = true;
    private bool isFlipped;

    private void Start()
    {
        if (aggresion == "readied" || "passive" || "hostile" || "beserk"){
            startPosition = transform.position;
            targetPosition = startPosition + Vector3.left * walkingDistance;
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (aggresion == "readied"){
            //This Code Handles Normal Walk Patterns
            StandardWalk();
            aggresion = Detection(distance, detectionRange);
        }
        if (aggresion == "passive"){
            //This Code is For Units that are Passive
            StandardWalk();
        }
        if (aggresion == "hostile"){
            //This Code When Aggresion Occurs
            Chase(1);
            aggresion = Detection(distance, detectionRange);
        }
        if (aggresion == "beserk"){
            Chase(2);
            aggresion = Detection(distance, detectionRange * 2);
        }

    }

    //Internal Functions

    void Chase(int anger){
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * speed * anger * Time.deltaTime;
    }

    void StandardWalk()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
        {
            walkingForward =! walkingForward;
            if (walkingForward){
                targetPosition = startPosition + Vector3.left * walkingDistance;
            }
            if (!walkingForward){
                targetPosition = startPosition;
            }
            Flip();
        }
    }
    void Flip()
    {
        isFlipped = !isFlipped;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    string Detection(float distance, float detectionRange)
    {
        if (distance >= detectionRange)
        {
            return "readied";
        }
        else 
        {
            return "hostile";
        }
    }
}
