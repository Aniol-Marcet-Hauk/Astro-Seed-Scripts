using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLineRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float x, y, a, b;
    [SerializeField] private LineRenderer lr;
    private float X,Y;
    public float speedAppear = 0.3f;
    EdgeCollider2D edgeCollider;

    void Start()
    {
        edgeCollider = gameObject.GetComponent<EdgeCollider2D>();
        var points = new Vector3[201];
        for (float alpha = 0; alpha < 201; alpha++)
        {
            X = x + (a * Mathf.Cos(alpha * 0.028f));
            Y = y + (b * Mathf.Sin(alpha * 0.028f));
            points[(int)alpha] = new Vector3(X, Y, 0);
        }
        lr.SetPositions(points);
    }

    void Update()
    {
        UpdateCollider(); // call only when line changes if possible
    }

    void UpdateCollider()
    {
        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);

        Vector2[] points = new Vector2[positions.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            points[i] = new Vector2(positions[i].x, positions[i].y);
        }

        edgeCollider.points = points;
    }
   
    private void OnEnable()
    {
        Color c1 = new Color(1, 1, 1, 0);
        Color c2 = new Color(1, 1, 1, 1);
        
        lr.DOColor(new Color2(c1,c1), new Color2(c2,c2),speedAppear);

    }
   /* IEnumerator changeWidth()
    {
        float time = Time.time ;
        float t = 0;
        while (time+0.2f>Time.time)
        {
            lr.widthMultiplier = Mathf.Lerp(0, .5f, t);
            t += Time.deltaTime/0.2f;
            yield return null;
        }
    }  */
}
