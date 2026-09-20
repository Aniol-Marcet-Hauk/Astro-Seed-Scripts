using Cinemachine;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteAlways]
public class CameraSizeCalculatorPixelPerfect : MonoBehaviour
{
    public PixelPerfectCamera pixelPerfect;
    public CinemachineVirtualCamera vcam; // Drag your Virtual Camera here

    void LateUpdate()
    {
        if (pixelPerfect == null || vcam == null) return;

        // 1. Get Distance from Camera to Z=0
        // Cinemachine typically moves the Unity Camera, so we check that position
        float distance = Mathf.Abs(vcam.transform.position.z);

        if (distance > 0.01f && pixelPerfect.assetsPPU > 0)
        {
            // 2. Calculate the "Target Orthographic Size"
            // Height in units = RefResolution / PPU
            // OrthoSize is half of that height
            float orthoSize = (float)pixelPerfect.refResolutionY / (2f * pixelPerfect.assetsPPU);

            // 3. Convert OrthoSize to Perspective FOV
            // Formula: 2 * atan(orthoSize / distance)
            float fovRad = 2f * Mathf.Atan(orthoSize / distance);
            float targetFOV = fovRad * Mathf.Rad2Deg;

            // 4. Apply specifically to the Cinemachine Lens
            LensSettings lens = vcam.m_Lens;
            lens.FieldOfView = targetFOV;
            vcam.m_Lens = lens;
        }
    }
}
