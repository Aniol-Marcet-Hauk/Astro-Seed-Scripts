using System;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class ScreenPixelGltich : MonoBehaviour
{
    [Header("Glitch Settings")]
    [Range(0f, 1f)] public float glitchChance = 0.3f; // burst strength when a burst occurs
    [Range(0f, 1f)] public float glitchChanceMin = 0.01f; // baseline (non-burst) glitch strength
    [Range(0f, 1f)] public float burstProbability = 0.2f; // probability each interval that a burst occurs
    public float pixelOffset = 1f;
    public bool glitchActive = true;

    [Header("Chunk Glitch")]
    [Range(1, 128)] public int blockSizeMin = 4;
    [Range(1, 128)] public int blockSizeMax = 8;

    [Header("Timing")]                                      
    public float glitchInterval = 0.1f; // Faster intervals usually look better for glitches

    private Material glitchMaterial;

    // Per-interval / burst state (persist across frames so values stay stable for the interval)
    private float lastSteppedSeed = -1f;
    private float currentGlitchChance = 0f;
    private int currentBlockSize = 8;
    private float currentSeed = 0f; // stable per-interval seed in [0,1]

    void OnEnable()
    {
        Shader shader = Shader.Find("Hidden/PixelGlitch");
        if (shader == null)
        {
            Debug.LogError("PixelGlitch shader not found!");
            enabled = false;
            return;
        }
        glitchMaterial = new Material(shader);
        currentGlitchChance = Mathf.Clamp01(glitchChanceMin);
        currentBlockSize = Mathf.Clamp(blockSizeMin, 1, 128);
        // initialize a stable seed
        currentSeed = UnityEngine.Random.value;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (!glitchActive || glitchMaterial == null)
        {
            Graphics.Blit(src, dest);
            return;
        }

        // Use realtimeSinceStartup so randomness appears in Edit mode as well as Play mode.
        float steppedSeed = Mathf.Floor(Time.realtimeSinceStartup / Mathf.Max(0.0001f, glitchInterval));

        // Re-evaluate per-interval logic only when entering a new stepped interval.
        if (!Mathf.Approximately(steppedSeed, lastSteppedSeed))
        {
            lastSteppedSeed = steppedSeed;
            int seedInt = Mathf.FloorToInt(steppedSeed);

            // Ensure valid min/max and clamp to shader-supported range
            int min = Mathf.Clamp(blockSizeMin, 1, 128);
            int max = Mathf.Clamp(blockSizeMax, min, 128);

            // Deterministic per-interval RNG using seedInt so values are stable for the interval
            var rnd = new System.Random(seedInt);

            // Decide burst vs baseline using the new burstProbability
            float roll = (float)rnd.NextDouble();
            if (roll < Mathf.Clamp01(burstProbability))
                currentGlitchChance = Mathf.Clamp01(glitchChance); // burst uses glitchChance (burst strength)
            else
                currentGlitchChance = Mathf.Clamp01(glitchChanceMin); // baseline

            // Pick a block size in [min, max], deterministic for the interval
            currentBlockSize = rnd.Next(min, max + 1);

            // Use a stable, bounded seed (0..1) generated deterministically for this interval.
            currentSeed = (float)rnd.NextDouble();
        }

        glitchMaterial.SetFloat("_BlockSize", currentBlockSize);
        glitchMaterial.SetFloat("_GlitchChance", currentGlitchChance);
        glitchMaterial.SetFloat("_PixelSize", pixelOffset);

        // Pass the bounded per-interval seed to the shader so the block patterns stay stable
        // but do not monotonically increase over time.
        glitchMaterial.SetFloat("_Seed", currentSeed);

        Graphics.Blit(src, dest, glitchMaterial);
    }
}