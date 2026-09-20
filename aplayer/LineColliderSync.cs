using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer), typeof(EdgeCollider2D))]
public class LineColliderSync : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private List<Vector2> colliderPoints = new List<Vector2>();

    public Rigidbody2D correspondingArm;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    void LateUpdate()
    {
        UpdateCollider();
    }

    void UpdateCollider()
    {
        colliderPoints.Clear();

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 linePoint = lineRenderer.GetPosition(i);

            //if the LineRenderer is set to World Space, we must convert the points 
            // back to Local Space for the EdgeCollider2D to position them correctly.
            if (lineRenderer.useWorldSpace)
            {
                linePoint = transform.InverseTransformPoint(linePoint);
            }

            colliderPoints.Add(new Vector2(linePoint.x, linePoint.y));
        }

        edgeCollider.SetPoints(colliderPoints);
    }
}