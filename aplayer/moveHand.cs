using Cinemachine.Utility;
using UnityEngine;

public abstract class moveHand : MonoBehaviour
{
    protected Transform trans;
    public Transform elbow;
    public Rigidbody2D rb;
    public Rigidbody2D elbowH;
    public bool holding = false;

    [Space]
    public HandAudio audio;
    public float humMultiplier = 1f;
    public float humMultiplierWhenHolding = 0.5f;

    [Space]
    public Collider2D handColl, handColl2;

    protected float fixNotMovingOneArmBug = 0f;
    protected InputManager inputManager;

    protected virtual void Start()
    {
        trans = transform;
        inputManager = InputManager.instance;
    }

    protected virtual void FixedUpdate()
    {
        Vector3 inputAxis = ReadInputAxis();
        if (inputAxis.magnitude > 1f)
        {
            inputAxis.Normalize();
        }

        SetOtherHandHolding(holding);
        OnBeforeMovement(inputAxis);

        if (holding == false)
        {
            if (inputAxis.magnitude == 0f && OtherHandHolding)
            {
                if (fixNotMovingOneArmBug < 1f)
                {
                    fixNotMovingOneArmBug += Time.fixedDeltaTime;
                }
                else
                {
                    rb.position = elbow.position;
                }
            }
            else
            {
                fixNotMovingOneArmBug = 0f;
            }

            Vector3 vec = CalculateUnheldPosition(inputAxis);
            rb.MovePosition(vec);
            float mag = new Vector2(vec.x - transform.position.x, vec.y - transform.position.y).magnitude;
            audio.SetHumPitchBlend(mag * humMultiplier);
        }
        else
        {
            Vector3 gravityNormalized = Physics2D.gravity.Abs();
            gravityNormalized.Normalize();

            Vector3 pos = CalculateHeldPosition(inputAxis, gravityNormalized);
            elbowH.MovePosition(pos);

            float multiplyY = -0.47f * gravityNormalized.y + 1f;
            float multiplyX = -0.47f * gravityNormalized.x + 1f;
            float mag = new Vector2((pos.x - elbowH.position.x) * multiplyX, (pos.y - elbowH.position.y) * multiplyY).magnitude;
            audio.SetHumPitchBlend(mag * humMultiplierWhenHolding);
        }
    }

    protected virtual void OnBeforeMovement(Vector3 inputAxis)
    {
    }

    protected abstract Vector3 ReadInputAxis();

    protected abstract float UnheldDistance { get; }

    protected abstract float HoldingDistance { get; }

    protected virtual float HoldingDistanceX => 10f;

    protected virtual float HoldingDistanceYMultiplier => 1f;

    protected abstract float UnheldXSign { get; }

    protected abstract float UnheldYSign { get; }

    protected abstract float HeldXSign { get; }

    protected abstract float HeldYSign { get; }

    protected virtual bool OtherHandHolding => false;

    protected virtual void SetOtherHandHolding(bool isHolding)
    {
    }

    protected virtual Vector3 CalculateUnheldPosition(Vector3 inputAxis)
    {
        return new Vector3(
            elbow.position.x + inputAxis.x * UnheldDistance * UnheldXSign * Time.fixedDeltaTime,
            elbow.position.y + inputAxis.y * UnheldDistance * UnheldYSign * Time.fixedDeltaTime,
            elbow.position.z);
    }

    protected virtual Vector3 CalculateHeldPosition(Vector3 inputAxis, Vector3 gravityNormalized)
    {
        float distX = UnheldDistance + (HoldingDistance - UnheldDistance) * gravityNormalized.x + HoldingDistanceX * (1 - gravityNormalized.x);
        float distY = UnheldDistance + (HoldingDistance - UnheldDistance) * gravityNormalized.y + HoldingDistanceX * (1 - gravityNormalized.y) * HoldingDistanceYMultiplier;

        return new Vector3(
            elbow.position.x - inputAxis.x * distX * HeldXSign * Time.fixedDeltaTime,
            elbow.position.y - inputAxis.y * distY * HeldYSign * Time.fixedDeltaTime,
            elbow.position.z);
    }
}