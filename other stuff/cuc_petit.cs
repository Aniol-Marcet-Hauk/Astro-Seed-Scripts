using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuc_petit : MonoBehaviour
{
    public LayerMask LM;
    public bool isHappening = false;
    public Animator an;
    [SerializeField] private GameObject dot;

    public float timeBeforeStartAn = 0;

    private Rigidbody2D stuck;

    private void Start()
    {
        if (an == null)
        {
            an = gameObject.GetComponent<Animator>();
        }

        if (an == null)
        {
            an = gameObject.GetComponentInParent<Animator>();
        }

        an.enabled = false;
        Invoke("EnableAn", timeBeforeStartAn);
    }

    private void EnableAn()
    {
        if (an != null)
        {
            an.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Check if attack is already happening to prevent spamming
        if (isHappening) return;

        // 2. Check if the object has the "Player" tag AND matches LayerMask
        if (collision.CompareTag("Player") && ((1 << collision.gameObject.layer) & LM) != 0)
        {

            Debug.Log("cmon");
            LineColliderSync lcs = collision.GetComponent<LineColliderSync>();
            if(lcs != null)
            {
                stuck = lcs.correspondingArm;
            }
            // 3. Find the Rigidbody2D we want to freeze
            else if (collision.GetComponent<script_to_separate_arm>() != null)
            {
                stuck = collision.transform.parent.GetComponent<Rigidbody2D>();
            }
            else
            {
                stuck = collision.GetComponent<Rigidbody2D>();
            }

            // Safety check: if there is no Rigidbody, don't crash the game
            if (stuck != null)
            {
                // 4. Position the dot at the closest point of contact
                dot.transform.position = collision.ClosestPoint(transform.position);

                // 5. Trigger the attack
                StartCoroutine(Attack());
            }
        }
    }

    public IEnumerator Attack()
    {
        Debug.Log("a");
        isHappening = true;
        an.speed = 0;

        // Animation pain with a dot appearing there
        dot.SetActive(true);
        dot.transform.rotation = Quaternion.Euler(dot.transform.rotation.x, dot.transform.rotation.y, Random.Range(0f, 360f));
        // Freeze the player
        stuck.bodyType = RigidbodyType2D.Static;

        yield return new WaitForSeconds(.7f);

        // Unfreeze and resume
        dot.SetActive(false);
        an.speed = 2f;
        stuck.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(1f);
        isHappening = false;
        an.speed = 1f;
    }
}
