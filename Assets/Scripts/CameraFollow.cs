using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // The player to follow 
    [Range(2f, 10f)]
    public float smoothSpeed;   // Speed of the camera follow
    
    public Vector3 offset;   // Offset from the player
    public Vector3 minPosition, maxPosition;

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = player.position + offset;
        
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);

        float clampX = Mathf.Clamp(smoothPosition.x, minPosition.x, maxPosition.x);
        float clampY = Mathf.Clamp(smoothPosition.y, minPosition.y, maxPosition.y);
        Vector3 clampedPosition = new Vector3(clampX, clampY, smoothPosition.z);
        transform.position = clampedPosition;
    }

}
