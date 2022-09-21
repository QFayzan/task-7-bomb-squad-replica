using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
   public List<Transform> targets;
   public float smoothTime = 0.5f;
   public float minZoom = 40f;
   public float maxZoom = 10f;
   public float zoomLimiter = 28;
   private Vector3 velocity;
   public Vector3 offset;
   private Camera cam;
void Start()
{
    cam =GetComponent<Camera>();
}
void LateUpdate()
{
    if (targets.Count == 0)
    {
        return;
    }
    Move();
    Zoom();
}
void Zoom()
{
    float newZoom = Mathf.Lerp( maxZoom , minZoom , GetGreatestDistane()/zoomLimiter);
    cam.fieldOfView = Mathf.Lerp (cam.fieldOfView , newZoom , Time.deltaTime);
}
float GetGreatestDistane()
{
    var bounds = new Bounds(targets[0].position,Vector3.zero);
     for (int i = 0 ; i< targets.Count; i++)
    {
        bounds.Encapsulate(targets[i].position);
    }
    return bounds.size.x;
}

Vector3 GetCenterPoint()
{
    if (targets.Count == 1)
    {
        return targets[0].position;
    }
    var bounds = new Bounds(targets[0].position, Vector3.zero);
    for (int i = 0 ; i< targets.Count; i++)
    {
        bounds.Encapsulate(targets[i].position);
    }
    return bounds.center;
}
void Move()
{
    Vector3 centerPoint = GetCenterPoint();

    Vector3 newPosition = centerPoint + offset;

    transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity ,smoothTime);
}
}
