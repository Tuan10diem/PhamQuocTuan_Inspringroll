using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWool : MonoBehaviour
{
    public Transform target;  // Reference to the player's transform
    public float smoothSpeed = 0.125f;  // Smoothing factor for camera movement
    public Vector3 offset;  // Offset from the player's position

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(0,desiredPosition.y,-10), smoothSpeed);
            transform.position = smoothedPosition;

        }
    }
}
