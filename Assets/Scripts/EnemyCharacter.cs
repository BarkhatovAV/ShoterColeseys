using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private Transform _head;
    [SerializeField] private Health _health;

    public Vector3 targetPosition { get; private set; } = Vector3.zero;
    private float _velocityMagnitude = 0;
    private Coroutine _coroutineY;
    private Coroutine _coroutineX;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (_velocityMagnitude > 0.1f)
        {
            float maxDistance = _velocityMagnitude * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, maxDistance);
        }
        else
        {
            transform.position = targetPosition;
        }
    }

    public void SetMaxHP(int value)
    {
        MaxHealth = value;
        _health.SetMax(value);
        _health.SetCurrent(value);
    }

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);
    }

    public void SetMovement(in Vector3 position, in Vector3 velocity, in float averageInterval)
    {
        targetPosition = position + (velocity * averageInterval);
        _velocityMagnitude = velocity.magnitude;

        this.Velocity = velocity;
    }

    public void SetSpeed(float value) => Speed = value;

    public void SetRotateX(float value, float averageInterval)
    {
        if (_coroutineX != null)
            StopCoroutine(_coroutineX);

        _coroutineX = StartCoroutine(RotationX());

        IEnumerator RotationX()
        {
            while (_head.localEulerAngles.x != value)
            {
                yield return null;
                _head.localEulerAngles = new Vector3(Mathf.LerpAngle(_head.localEulerAngles.x, value, averageInterval), 0, 0);
            }
        }

        
    }

    public void SetRotateY(float value, float averageInterval)
    {
        print(value);
        if (_coroutineY != null)
            StopCoroutine(_coroutineY);

        _coroutineY = StartCoroutine(RotationY());

        IEnumerator RotationY()
        {
            while (transform.localEulerAngles.y != value)
            {
                yield return null;
                transform.localEulerAngles = new Vector3(0, Mathf.LerpAngle( transform.localEulerAngles.y, value , averageInterval), 0);
            }
        }
        //transform.localEulerAngles = Vector3.Lerp(new Vector3(0, transform.localEulerAngles.y, 0), new Vector3(0, value, 0), averageInterval / 2);
    }
}
