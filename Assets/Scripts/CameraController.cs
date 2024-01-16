using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Player target settings")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 velocity = Vector3.zero;

    [Range(0, 1)]
    [SerializeField] private float smoothTime;
    [SerializeField] private Vector3 positionOffset;

    [Header("Axis Limitation")]
    [SerializeField] private bool cameraLimits = false;
    [SerializeField] private Vector2 xLimit;
    [SerializeField] private Vector2 yLimit;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + positionOffset;

        if(cameraLimits)
        {
            targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y), Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y), -10);
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
