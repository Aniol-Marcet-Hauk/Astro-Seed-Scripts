using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpiderDestroys : MonoBehaviour
{
    [SerializeField] private float strength;
    private bool breaks = false;
    [SerializeField] private float shakeMagnitude = 0.025f;
    [SerializeField] private float shakeInterval = 0.09f; // Increase for slower shake
    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;

    private void Start()
    {
        originalPosition = transform.localPosition;
        //shakeCoroutine = StartCoroutine(ShakeUntilBreaks());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // You can set breaks = true here if needed
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boss2Breaks")
        {
            breaks = true;
            if (shakeCoroutine != null)
            {
                StopCoroutine(shakeCoroutine);
                transform.localPosition = originalPosition;
            }

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            Rigidbody2D[] _rbs = GetComponentsInChildren<Rigidbody2D>(true);
            Vector2 _contactPoint = collision.transform.position;
            foreach (Rigidbody2D _rb in _rbs)
            {
                _rb.gameObject.SetActive(true);
                Vector2 _v = _rb.transform.position;
                _v += _rb.GetComponent<Collider2D>().offset - _contactPoint;
                _v = _v.normalized * strength / _v.magnitude;
                _rb.AddForce(_v);
            }
        }
    }


    private IEnumerator ShakeUntilBreaks()
    {
        while (!breaks)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.localPosition = originalPosition + new Vector3(x, y, 0);
            yield return new WaitForSeconds(shakeInterval);
        }
        transform.localPosition = originalPosition;
    }
}
