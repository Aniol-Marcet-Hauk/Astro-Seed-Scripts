using UnityEngine;

public class moveRightHand : moveHand
{
    public float distance = 1.8f;
    public float distanceWhenHolding;
    public float distanceWhenHoldingX = 10f;
    public bool leftHolding;

    protected override Vector3 ReadInputAxis()
    {
        return new Vector3(inputManager.rightHand.x, inputManager.rightHand.y, 0);
    }

    protected override float UnheldDistance => distance;

    protected override float HoldingDistance => distanceWhenHolding;

    protected override float HoldingDistanceX => distanceWhenHoldingX;

    protected override float UnheldXSign => 1f;

    protected override float UnheldYSign => 1f;

    protected override float HeldXSign => -1f;

    protected override float HeldYSign => -1f;

    protected override bool OtherHandHolding => leftHolding;
}
