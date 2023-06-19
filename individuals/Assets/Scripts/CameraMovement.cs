using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // The object to follow
    public float speed = 5f; // The speed at which the camera moves
    private Vector3 offset; // The initial distance between the camera and the target
    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }

}

