using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineController : MonoBehaviour
{
    private LineRenderer lr;

    [SerializeField] private Transform[] points;
    [SerializeField, Range(1, 100)] private int segmentsPerCurve = 10;

    private void Start()
    {                                                           
        lr = gameObject.GetComponent<LineRenderer>();
    }

    private void Update()
    {
         DrawLine();
    }

    private void DrawLine()
    {
        if (points == null || points.Length < 2)
        {
            lr.positionCount = 0;
            return;
        }

        // Total sampled points: segmentsPerCurve per segment, but avoid duplicate points between segments.
        int segmentCount = points.Length - 1;
        int totalSamples = segmentCount * segmentsPerCurve + 1;
        Vector3[] sampledPositions = new Vector3[totalSamples];

        int dstIndex = 0;
        for (int i = 0; i < segmentCount; i++)
        {
   
            Vector3 p0 = (i - 1 >= 0) ? points[i - 1].position : points[i].position;
            Vector3 p1 = points[i].position;
            Vector3 p2 = points[i + 1].position;
            Vector3 p3 = (i + 2 < points.Length) ? points[i + 2].position : points[i + 1].position;
            Vector3 c1 = p1 + (p2 - p0) / 6f;
            Vector3 c2 = p2 - (p3 - p1) / 6f;

        
            for (int s = 0; s < segmentsPerCurve; s++)
            {
                float t = s / (float)segmentsPerCurve;
                sampledPositions[dstIndex++] = GetCubicBezierPoint(p1, c1, c2, p2, t);
            }
        }

        // Add final anchor point
        sampledPositions[dstIndex] = points[points.Length - 1].position;

        lr.positionCount = sampledPositions.Length;
        lr.SetPositions(sampledPositions);
    }

    private static Vector3 GetCubicBezierPoint(Vector3 p0, Vector3 c1, Vector3 c2, Vector3 p3, float t)
    {
        // Standard cubic Bezier: B(t) = (1-t)^3 * p0 + 3(1-t)^2 t * c1 + 3(1-t) t^2 * c2 + t^3 * p3
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        return uuu * p0
             + 3f * uu * t * c1
             + 3f * u * tt * c2
             + ttt * p3;
    }

    /* Optional public setter if you need to assign points at runtime:
    public void setUpLine(Transform[] points)
    {
        this.points = points;
    }
    */
}
