using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private float BALL_VELOCITY_MIN_AXIS_VALUE = 0.5f;
    [SerializeField]
    private float _initSpeed = 5;
    [SerializeField]
    private float _minSpeed= 4;
    [SerializeField]
    private float _maxSpeed= 7;

    private Rigidbody2D _rb;
    private Collider2D _collider;
    

    void FixedUpdate()
    {
        CheckVelocity();
    }

    public void Init()
    {
        
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        
        _collider.enabled = true;
        _rb.velocity= Random.insideUnitCircle.normalized * _initSpeed;

    }

    private void CheckVelocity()
    {
        Vector2 velocity =_rb.velocity;
        float currentSpeed = velocity.magnitude;

        if(currentSpeed<_minSpeed)
        {
            velocity = velocity.normalized* _minSpeed;
        }
        else if (currentSpeed>_maxSpeed)
        {
            velocity = velocity.normalized* _maxSpeed;
        }

        if(Mathf.Abs(_rb.velocity.x) < BALL_VELOCITY_MIN_AXIS_VALUE) 
        {
            velocity.x += Mathf.Sign(velocity.x) * BALL_VELOCITY_MIN_AXIS_VALUE*Time.deltaTime;
        }
        else if (Mathf.Abs(_rb.velocity.y) < BALL_VELOCITY_MIN_AXIS_VALUE)
        {   
            velocity.y += Mathf.Sign(velocity.y) * BALL_VELOCITY_MIN_AXIS_VALUE* Time.deltaTime;
        }

        _rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        BlockTile blockTileHit;
        if(!other.collider.TryGetComponent(out blockTileHit))
        {
            return;
        }

        ContactPoint2D contactPoint = other.contacts[0];
        blockTileHit.OnHitCollision(contactPoint);
    }

    public void Hide()
    {
        _collider.enabled = false;
        gameObject.SetActive(false);
    }
}