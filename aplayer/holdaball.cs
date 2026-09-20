using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holdaball : MonoBehaviour
{
    public PhysicsMaterial2D phys;
    public float whatFriction;
    public Transform trans;
    public float distance = 3;
    public float distanceWhenOtherHold = 3;
    public bool right;
    [SerializeField] public moveLeftHand ml;
    [SerializeField] public moveRightHand mr;
    public Vector3 x;
    private Rigidbody2D rb;
    public bool h;
    private bool particleTime = false;
    private HingeJoint2D hinge;
    public bool electrecuted = false;
    private Animator an;
    private Transform collMovaball;
    private bool ismoving;
    private Collider2D collIsnull;
    public holdaball otherHand;
    public ParticleSystem pS, ps2;
    [SerializeField] private HandAudio handAudio;

    [Space]
    public bool Mpurplehold = false;
    public bool blueBreakyThing = false;
    [HideInInspector] public bool Turtle = false;
    private Vector3 lastPos;

    private InputManager inputManager;
    private GameManager gameManager;

    public Color colorHand;
    [Space]
    public SpriteRenderer handSprite;

    private void Start()
    {
        gameManager = GameManager.gameManager;
        Cursor.visible = false;
        rb = trans.GetComponent<Rigidbody2D>();
        h = false;
        hinge = trans.GetComponent<HingeJoint2D>();
        electrecuted = false;
        an = gameObject.GetComponent<Animator>();

        if (right == true)
        {
            ml.distance = distance;
            if (InputManager.instance.playerMode != 2)
            {
                colorHand = gameManager.colorRight;
            }
            else
            {
                colorHand = gameManager.colorLeft;
            }
        }
        else
        {
            mr.distance = distance;
            if (InputManager.instance.playerMode != 2)
            {
                colorHand = gameManager.colorLeft;
            }
            else
            {
                colorHand = gameManager.colorRight;
            }
        }

        handSprite.color = colorHand;
        phys.friction = whatFriction;
        lastPos = trans.position;
        inputManager = InputManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ShouldIgnoreTriggerCollision())
        {
            return;
        }

        Grab(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (ShouldIgnoreStayCollision())
        {
            return;
        }

        Grab(collision);
    }

    public void Grab(Collider2D collision)
    {
        if (collision.tag == "holdable")
        {
            HandleHoldableCollision(collision);
        }

        if (collision.tag == "holdableanypos")
        {
            HandleHoldableAnyPosCollision(collision);
        }

        if (collision.tag == "items")
        {
            HandleItemCollision(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "holdable" && ismoving == false && h == true)
        {
            ConsistentMovement cons = collision.GetComponent<ConsistentMovement>();
            if (cons != null || collision.GetComponent<IfToFarLeave>())
            {
                ReleaseHoldableFromExit(cons);
            }
        }
    }

    private void Update()
    {
        if (inputManager == null)
        {
            inputManager = InputManager.instance;
        }

        if (IsTriggerPressed())
        {
            an.SetBool("isholding", true);
            handAudio.PlayActivate();

            if (h == true && particleTime == false)
            {
                Invoke(nameof(PlayParticle), 0.05f);
                particleTime = true;
                handAudio.PlayGrab();

                gameManager.ShakeHands(1, 0.15f, 0.05f);
                if (collMovaball != null && collMovaball.TryGetComponent<IAudioBall>(out var ballSound))
                {
                    handAudio.PlayRandomizedPitch(ballSound.GetRockSound(), ballSound.GetPitchRange().x, ballSound.GetPitchRange().y);
                }
            }
        }
        else
        {
            particleTime = false;
            an.SetBool("isholding", false);
            handAudio.PlayDeactivate();
        }

        if (collMovaball != null)
        {
            SyncHeldItemTriggerState();

            if (ShouldForceRelease())
            {
                ReleaseCurrentHeldObject();
            }
        }

        if (h == false)
        {
            Mpurplehold = false;
            particleTime = false;
        }
    }

    private void PlayParticle()
    {
        pS.Play();
        ps2.Play();
    }

    public void applyColorWhenHold()
    {
        handSprite.color = colorHand;
    }

    public void applyColorWhenNotHold()
    {
        handSprite.color = Color.white;
    }

    private bool ShouldIgnoreTriggerCollision()
    {
        return electrecuted == true || h == true;
    }

    private bool ShouldIgnoreStayCollision()
    {
        return electrecuted == true || h == true || Mpurplehold == true;
    }

    private bool IsTriggerPressed()
    {
        return (inputManager.rightTriggerPressed && right == true) || (inputManager.leftTriggerPressed && right == false);
    }

    private bool ShouldForceRelease()
    {
        return (!inputManager.rightTriggerPressed && right == true)
            || (!inputManager.leftTriggerPressed && right == false)
            || collIsnull.isActiveAndEnabled == false
            || electrecuted == true
            || Mpurplehold == true
            || collMovaball.gameObject.activeSelf == false;
    }

    private void HandleHoldableCollision(Collider2D collision)
    {
        if (IsTriggerPressed())
        {
            if (h == false)
            {
                BeginHoldableGrab(collision);
            }
            else if (collision.GetComponent<ConsistentMovement>() != null && ismoving == false && h == true)
            {
                ReleaseHoldableFromStay();
            }
        }
        else if (collision.GetComponent<ConsistentMovement>() != null && ismoving == false && h == true)
        {
            ReleaseHoldableFromStay();
        }
    }

    private void BeginHoldableGrab(Collider2D collision)
    {
        levelMoverBall lvMball = collision.GetComponent<levelMoverBall>();
        if (lvMball != null && blueBreakyThing == false)
        {
            blueBreakyThing = true;
            ml.ArmCounter += 1;
        }

        ml.String = collision.GetComponent<lowerhandstrength>() != null;

        trans.position = collision.transform.position;
        hinge.enabled = true;
        hinge.connectedBody = collision.attachedRigidbody;
        hinge.connectedAnchor = Vector3.zero;

        if (right == true)
        {
            mr.holding = true;
            ml.distance = distanceWhenOtherHold;
        }
        else
        {
            ml.holding = true;
            mr.distance = distanceWhenOtherHold;
        }

        phys.friction = 15;

        destroywhenhit hit = collision.GetComponent<destroywhenhit>();
        if (hit != null)
        {
            hit.hit = true;
        }

        ConsistentMovement ConsMove = collision.GetComponent<ConsistentMovement>();
        if (ConsMove != null && ConsMove.consistent == false)
        {
            ConsMove.Ifholding = true;
            otherHand.Turtle = true;
        }

        if (ConsMove == null)
        {
            ismoving = true;
        }

        collIsnull = collision;
        h = true;
        collMovaball = collision.transform;
    }

    private void ReleaseHoldableFromStay()
    {
        hinge.enabled = false;
        hinge.connectedBody = null;
        rb.bodyType = RigidbodyType2D.Dynamic;

        if (right == true)
        {
            mr.holding = false;
            ml.distance = distance;

            if (ml.holding != true)
            {
                phys.friction = whatFriction;
            }
        }
        else
        {
            ml.holding = false;
            mr.distance = distance;

            if (mr.holding != true)
            {
                phys.friction = whatFriction;
            }
        }

        ConsistentMovement ConsMove = collMovaball.GetComponent<ConsistentMovement>();
        if (ConsMove.consistent == false)
        {
            if (Turtle == false)
            {
                ConsMove.Ifholding = false;
            }

            otherHand.Turtle = false;
        }

        collMovaball = null;
        h = false;
        ismoving = false;
        particleTime = false;
    }

    private void HandleHoldableAnyPosCollision(Collider2D collision)
    {
        if (IsTriggerPressed() && h == false)
        {
            hinge.enabled = true;
            hinge.connectedBody = collision.attachedRigidbody;
            x = collision.transform.InverseTransformPoint(trans.position);
            hinge.connectedAnchor = x;

            if (right == true)
            {
                mr.holding = true;
                ml.distance = distanceWhenOtherHold;
            }
            else
            {
                ml.holding = true;
                mr.distance = distanceWhenOtherHold;
            }

            phys.friction = 15;

            collMovaball = collision.transform;
            collIsnull = collision;
            h = true;
            ismoving = true;
        }
    }

    private void HandleItemCollision(Collider2D collision)
    {
        if (IsTriggerPressed() && h == false)
        {
            if (right == true)
            {
                mr.holding = false;
            }
            else
            {
                ml.holding = false;
            }

            collMovaball = collision.transform;
            trans.position = collision.transform.position;

            if (collision.GetComponent<Onetriggerisequal>() != null || collision.GetComponent<MovableHold>() != null)
            {
                Rigidbody2D rbMover = collision.transform.GetComponent<Rigidbody2D>();
                rbMover.gravityScale = 4f;
                rbMover.mass = 4f;
                collision.isTrigger = true;
            }

            levelMoverBall lvMball = collision.GetComponent<levelMoverBall>();
            if (lvMball != null && blueBreakyThing == false)
            {
                ml.ArmCounter += 1;
                blueBreakyThing = true;
            }

            hinge.enabled = true;
            hinge.connectedBody = collision.attachedRigidbody;
            hinge.connectedAnchor = Vector3.zero;
            h = true;

            ismoving = true;
            collIsnull = collision;
            collIsnull.isTrigger = true;
        }
    }

    private void ReleaseCurrentHeldObject()
    {
        if (collMovaball.tag == "holdable")
        {
            ReleaseHoldableFromUpdate();
        }
        else if (collMovaball.tag == "items")
        {
            ReleaseItemFromUpdate();
        }
        else if (collMovaball.tag == "holdableanypos")
        {
            ReleaseHoldableAnyPosFromUpdate();
        }
    }

    private void ReleaseHoldableFromUpdate()
    {
        collIsnull = null;
        hinge.enabled = false;
        hinge.connectedBody = null;

        levelMoverBall lvMover = collMovaball.GetComponent<levelMoverBall>();
        if (lvMover != null)
        {
            ml.ArmCounter -= 1;
            blueBreakyThing = false;
        }

        rb.bodyType = RigidbodyType2D.Dynamic;
        if (right == true)
        {
            mr.holding = false;
            ml.distance = distance;

            if (ml.holding != true)
            {
                phys.friction = whatFriction;
            }
        }
        else
        {
            ml.holding = false;
            mr.distance = distance;

            if (mr.holding != true)
            {
                phys.friction = whatFriction;
            }
        }

        ConsistentMovement ConsMove = collMovaball.GetComponent<ConsistentMovement>();
        if (ConsMove != null && ConsMove.consistent == false)
        {
            if (Turtle == false)
            {
                ConsMove.Ifholding = false;
            }

            otherHand.Turtle = false;
        }

        collMovaball = null;
        particleTime = false;
        h = false;
        ismoving = false;
    }

    private void ReleaseItemFromUpdate()
    {
        hinge.enabled = false;
        hinge.connectedBody = null;

        if (collIsnull.GetComponent<Onetriggerisequal>() != null || collIsnull.GetComponent<MovableHold>() != null)
        {
            collIsnull.isTrigger = false;
        }

        levelMoverBall lvMover = collIsnull.GetComponent<levelMoverBall>();
        if (lvMover != null)
        {
            ml.ArmCounter -= 1;
            blueBreakyThing = false;
        }

        if (right == true)
        {
            mr.holding = false;
        }
        else
        {
            ml.holding = false;
        }

        collMovaball = null;
        h = false;
        particleTime = false;
        ismoving = false;
        collIsnull = null;
        Mpurplehold = false;
    }

    private void ReleaseHoldableAnyPosFromUpdate()
    {
        hinge.enabled = false;
        hinge.connectedBody = null;

        if (right == true)
        {
            mr.holding = false;
            ml.distance = distance;

            if (ml.holding != true)
            {
                phys.friction = whatFriction;
            }
        }
        else
        {
            ml.holding = false;
            mr.distance = distance;

            if (mr.holding != true)
            {
                phys.friction = whatFriction;
            }
        }

        collMovaball = null;
        h = false;
        particleTime = false;
        ismoving = false;
        collIsnull = null;
    }

    private void ReleaseHoldableFromExit(ConsistentMovement cons)
    {
        hinge.enabled = false;
        hinge.connectedBody = null;

        rb.bodyType = RigidbodyType2D.Dynamic;
        if (right == true)
        {
            mr.holding = false;
            ml.distance = distance;

            if (ml.holding != true)
            {
                phys.friction = whatFriction;
            }
        }
        else
        {
            ml.holding = false;
            mr.distance = distance;

            if (mr.holding != true)
            {
                phys.friction = whatFriction;
            }
        }

        if (cons != null)
        {
            cons.Ifholding = false;
            otherHand.Turtle = false;
        }

        collIsnull = null;
        particleTime = false;
        h = false;
    }

    private void SyncHeldItemTriggerState()
    {
        if (collMovaball.tag == "items")
        {
            if (collIsnull.GetComponent<Onetriggerisequal>() != null || collIsnull.GetComponent<MovableHold>() != null)
            {
                collIsnull.isTrigger = true;
            }
        }
    }
}