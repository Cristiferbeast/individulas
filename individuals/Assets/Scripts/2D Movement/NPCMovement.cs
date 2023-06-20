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
    private int beserkmeter = 0; 

    

    private void Start()
    {
        if(aggresion == "beserk"){
            beserkmeter = 10;
        }
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.left * walkingDistance;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        switch (aggresion)
        {
            case "readied":
                aggresion = Detection(distance, detectionRange);
                StandardWalk();
                break;
            case "passive":
                StandardWalk();
                break;
            case "hostile":
                Chase(1);
                aggresion = Detection(distance, detectionRange);
                break;
            case "berserk":
                for (int i = 0; i < beserkmeter; i++)
                {
                Chase(2);
                }
                aggresion = Detection(distance, detectionRange * 2);
                break;
            default:
            break;
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
