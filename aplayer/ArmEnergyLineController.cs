using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ArmEnergyLineController : MonoBehaviour
{
    private LineRenderer lr;

  
    [SerializeField] private Transform[] points;
    [SerializeField] private int segments = 10;
    [SerializeField] private float rangeOfMove = .2f;

    
    [SerializeField] private float maxMiddleInfluenceDistance = 5f,midInfConstant = 0.1f;

    [Space]
    [SerializeField] private AnimationCurve anCurve;
    [SerializeField] private Gradient gradient;
    [SerializeField] private float minSize = 0.01f, maxSize = 0.1f;

    
    [SerializeField] private bool preserveAlphaKeys = false;

    private GradientAlphaKey[] alphaKeys;
    private float[] keyTimes;

    [Space]
    [SerializeField] private bool right;
    private Color colorHand;

    private void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();

       
        int maxValue = segments + 1;

       
        alphaKeys = new GradientAlphaKey[]
        {
            new GradientAlphaKey(1f, 0f),
            new GradientAlphaKey(0f, .35f),
            new GradientAlphaKey(0f, .65f),
            new GradientAlphaKey(1f, 1f)
        };

        
        var colorKeys = new GradientColorKey[]
        {
            new GradientColorKey(Color.white, 0f)
        }.ToList();

        for (int i = 0; i < maxValue; i++)
        {
            // normalize to 0..1 inclusive
            float t = (maxValue > 1) ? (float)i / (maxValue - 1) : 0f;
            anCurve.AddKey(t, minSize);

            if (i < 7)
                colorKeys.Add(new GradientColorKey(Color.white, t));
        }

      
        GradientAlphaKey[] chosenAlphaKeys = preserveAlphaKeys ? gradient.alphaKeys : alphaKeys;


        gradient.SetKeys(colorKeys.ToArray(), chosenAlphaKeys);
        keyTimes = gradient.colorKeys.Select(k => k.time).ToArray();
        colorHand = right ? GameManager.gameManager.colorRight : GameManager.gameManager.colorLeft;

        lr.colorGradient = gradient;
    }

    private void Update()
    {
        if(points == null || points[0].gameObject.activeInHierarchy==false)
        {
            lr.positionCount = 0;
            return;
        }
        DrawLine();
    }

    private void DrawLine()
    {
      
        if (points == null || points.Length < 2)
        {
            lr.positionCount = 0;
            return;
        }

        Transform startT = points[0];
        Transform endT = points[points.Length - 1];
        if (startT == null || endT == null)
        {
            lr.positionCount = 0;
            return;
        }

        Vector3 p0 = startT.position;
        Vector3 p2 = endT.position;


        Vector3 rawMiddle;
        if (points.Length >= 3 && points[1] != null)
            rawMiddle = points[1].position;  
        else
            rawMiddle = (p0 + p2) * 0.5f;     

  
        float distance = Vector3.Distance(p0, p2);
        float middleWeight = Mathf.Clamp01(midInfConstant+ (distance / maxMiddleInfluenceDistance));

      
        Vector3 straightMid = (p0 + p2) * 0.5f;
        Vector3 effectiveMiddle = Vector3.Lerp(straightMid, rawMiddle, middleWeight);

        int totalSamples = segments + 1;
        Vector3[] sampledPositions = new Vector3[totalSamples];

        for (int s = 0; s < segments; s++)
        {
            float t = s / (float)segments;

         
            Vector3 basePos;
            if (t <= 0.5f)
            {
                float lt = t / 0.5f; 
                basePos = Vector3.Lerp(p0, effectiveMiddle, lt);
            }
            else
            {
                float lt = (t - 0.5f) / 0.5f; 
                basePos = Vector3.Lerp(effectiveMiddle, p2, lt);
            }

            Vector3 r = Random.insideUnitCircle;
            Vector3 pos = basePos + r * rangeOfMove;

            Keyframe key = anCurve.keys[s];
            key.value = Random.Range(minSize, maxSize) * Random.Range(0.5f, 1.1f);
            anCurve.MoveKey(s, key);

            sampledPositions[s] = pos;
        }

        sampledPositions[totalSamples - 1] = p2;
        Keyframe keyF = anCurve.keys[totalSamples - 1];
        keyF.value = Random.Range(minSize, maxSize) * .5f;
        anCurve.MoveKey(totalSamples - 1, keyF);

        lr.positionCount = sampledPositions.Length;
        lr.SetPositions(sampledPositions);

        lr.widthCurve = anCurve;


        if (keyTimes != null && keyTimes.Length > 0)
        {
            var frameColorKeys = new GradientColorKey[keyTimes.Length];
            for (int i = 0; i < keyTimes.Length; i++)
            {
                Color c = Color.Lerp(Color.white, colorHand, Random.Range(0.4f, 1f));
                frameColorKeys[i] = new GradientColorKey(c, keyTimes[i]);
            }

            if (preserveAlphaKeys)
            {
                
                var frameGradient = new Gradient();
                frameGradient.SetKeys(frameColorKeys, gradient.alphaKeys);
                lr.colorGradient = frameGradient;
            }
            else
            {
  
                var perFrameAlpha = new GradientAlphaKey[alphaKeys.Length];
                for (int i = 0; i < alphaKeys.Length; i++)
                {
                    perFrameAlpha[i] = new GradientAlphaKey(alphaKeys[i].alpha, alphaKeys[i].time);
                }

                float jitter = UnityEngine.Random.Range(-0.3f, 0.3f);

                float baseTime = alphaKeys[1].time;
                float newTime = Mathf.Clamp01(baseTime + jitter);
                perFrameAlpha[1] = new GradientAlphaKey(alphaKeys[1].alpha, newTime);

                jitter = UnityEngine.Random.Range(-0.3f, 0.3f);
                baseTime = alphaKeys[2].time;
                newTime = Mathf.Clamp01(baseTime + jitter);
                perFrameAlpha[2] = new GradientAlphaKey(alphaKeys[2].alpha, newTime);

                System.Array.Sort(perFrameAlpha, (a, b) => a.time.CompareTo(b.time));

                var frameGradient = new Gradient();
                frameGradient.SetKeys(frameColorKeys, perFrameAlpha);
                lr.colorGradient = frameGradient;
            }
        }
    }


    public void SetUpLine(Transform start, Transform middle, Transform end)
    {
        points = new[] { start, middle, end };
    }
}