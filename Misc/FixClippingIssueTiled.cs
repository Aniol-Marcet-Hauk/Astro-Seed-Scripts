using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixClippingIssueTiled : MonoBehaviour
{
    private Renderer rendererComponent;

    void Start()
    {
        rendererComponent = GetComponent<Renderer>();
    }

    void OnBecameInvisible()
    {
        // This is a debug tool: if this triggers, Unity DID cull it.
        Debug.Log(gameObject.name + " was culled by the camera!");
    }

    // This forces Unity to recalculate the bounding box to be massive every frame
    void Update()
    {
        if (rendererComponent != null)
        {
            rendererComponent.bounds = new Bounds(Vector3.zero, Vector3.one * 10000f);
        }
    }
}

