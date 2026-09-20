using UnityEngine;

//This is mostly like this for "legacy reasons"
public class moveLeftHand : moveHand
{
    public bool String = false;
    public float distance = 3;
    public float distanceWhenHolding;
    public float distanceWhenHoldingX = 10f;

    public float lowerDistanceWH = 75;
    public moveRightHand mr;
    public int ArmCounter;

    public float OnlyOneXDirectionForChapter4RotationThingy = 0;
    public float yAxisMulltiplierForLastScene = 1f;

    protected override void OnBeforeMovement(Vector3 inputAxis)
    {
        if (String == true && mr != null)
        {
            mr.distanceWhenHolding = mr.holding == true && holding == true ? lowerDistanceWH : distanceWhenHolding;
        }
    }

    protected override Vector3 ReadInputAxis()
    {
        return new Vector3(inputManager.leftHand.x, -inputManager.leftHand.y, 0);
    }

    protected override float UnheldDistance => distance;

    protected override float HoldingDistance => String == true && mr != null && mr.holding == true && holding == true ? lowerDistanceWH : distanceWhenHolding;

    protected override float HoldingDistanceX => distanceWhenHoldingX;

    protected override float HoldingDistanceYMultiplier => yAxisMulltiplierForLastScene;

    protected override float UnheldXSign => 1f;

    protected override float UnheldYSign => -1f;

    protected override float HeldXSign => -1f;

    protected override float HeldYSign => 1f;

    protected override bool OtherHandHolding => mr != null && mr.holding == true;

    protected override void SetOtherHandHolding(bool isHolding)
    {
        if (mr != null)
        {
            mr.leftHolding = isHolding;
        }
    }
   
}
