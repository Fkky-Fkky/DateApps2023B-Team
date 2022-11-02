using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Camera))]
public class ChaseCamera : MonoBehaviour
{
    #region
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private List<Transform> targets;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float smoothTime = 0.5f;

    [SerializeField]
    private float minZoom = 40;

    [SerializeField]
    private float maxZoom = 10;

    [SerializeField]
    private float zoomLimit = 50;

    private Vector3 velocity;
    #endregion

    private void Reset()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (targets.Count == 0) return;

        Move();
        Zoom();
    }

    private void Zoom()
    {
        var newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimit);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    private void Move()
    {
        var centerPoint = GetCenterPoint();
        var newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity,smoothTime);
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i= 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1) return targets[0].position;
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }

}
