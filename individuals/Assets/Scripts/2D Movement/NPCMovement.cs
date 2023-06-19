using UnityEngine;


public class NPCMovement : MonoBehaviour
{
    //External Variables
    public float speed = 3f;             
    public float walkingDistance = 10f;
    public string aggresion = "passive";
    public Transform playerTransform;
    public float detectionRange = 20f;

    //Internal Variables
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool walkingForward = true;
    private bool isFlipped;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.left * walkingDistance;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (aggresion == "passive"){
            //This Code Handles Normal Walk Patterns
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
            }
            //This Code Handles Enemy Detection 
            if (distance <= detectionRange)
            {
                aggresion = "hostile";
            }
        }
        if (aggresion == "hostile"){
            //This Code Handles Chase
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            if (distance >= detectionRange)
            {
                aggresion = "passive";
            }
        }
    }

    
}
