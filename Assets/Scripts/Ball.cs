using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float initialVelocity = 4f;
    [SerializeField] private float velocityMultiplier = 1.1f;

    private Rigidbody2D ballRb;
    private TrailRenderer ballTrailRenderer;

    public static event Action OnPlayer1Scored;
    public static event Action OnPlayer2Scored;

    private void Awake()
    {
        ballRb = GetComponent<Rigidbody2D>();
        ballTrailRenderer = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        CorutineEnableTrailAfterDelay();
        Launch();
    }

    private void Launch()
    {
        float xVelocity = UnityEngine.Random.Range(0, 2) == 0 ? -1f : 1f;
        float yVelocity = UnityEngine.Random.Range(0, 2) == 0 ? -1f : 1f;
        ballRb.linearVelocity = new Vector2(xVelocity, yVelocity) * initialVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Paddle"))
        {
            ballRb.linearVelocity *= velocityMultiplier;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CorutineEnableTrailAfterDelay();
        if (collision.gameObject.CompareTag("Goal1"))
        {
            OnPlayer2Scored?.Invoke();
        }
        else if(collision.gameObject.CompareTag("Goal2"))
        {
            OnPlayer1Scored?.Invoke();
        }
    }

    private void CorutineEnableTrailAfterDelay()
    {
        if (ballTrailRenderer != null)
        {
            ballTrailRenderer.enabled = false;
            StartCoroutine(EnableTrailAfterDelay(1f));
        }
    }

    private IEnumerator EnableTrailAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (ballTrailRenderer != null)
        {
            ballTrailRenderer.enabled = true;
        }
    }

    public void Restart()
    {
        transform.position = Vector2.zero;
        CorutineEnableTrailAfterDelay();
        Launch();
    }
}
