using Colyseus.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyCharacter _character;
    [SerializeField] private EnemyGun _gun;
    [SerializeField] private CrouchController _crouchController;

    private List<float> _receiveTimeInterval = new List<float> { 0, 0, 0, 0, 0 };
    private float _lastReceiveTime = 0f;

    private float AverageInterval
    {
        get
        {
            int receiveTimeIntervalCount = _receiveTimeInterval.Count;
            float sum = 0;
            for (int i = 0; i < receiveTimeIntervalCount; i++)
            {
                sum += _receiveTimeInterval[i];
            }

            return sum / receiveTimeIntervalCount;
        }
    }
    private Player _player;

    public void Init(Player player)
    {
        _player = player;
        _character.SetSpeed(player.speed);
        _character.SetMaxHP(player.hp);
        player.OnChange += OnChange;
    }

    public void Shoot(in ShootInfo shootInfo)
    {
        Vector3 position = new Vector3(shootInfo.pX, shootInfo.pY, shootInfo.pZ);
        Vector3 velocity = new Vector3(shootInfo.dX, shootInfo.dY, shootInfo.dZ);

        _gun.Shoot(position, velocity);
    }

    public void Destroy()
    {
        _player.OnChange -= OnChange;
        Destroy(gameObject);
    }

    public void Crouch()
    {
        _crouchController.SetInputCrouch();
    }

    private void SaveReceiveTime()
    {
        float interval = Time.time - _lastReceiveTime;
        _lastReceiveTime = Time.time;

        _receiveTimeInterval.Add(interval);
        _receiveTimeInterval.Remove(0);
    }

    public void OnChange(List<DataChange> changes)
    {
        SaveReceiveTime();

        //Vector3 position = transform.position;
        Vector3 position = _character.targetPosition;
        Vector3 velocity = _character.Velocity;

        foreach (var dataChange in changes)
        {
            switch (dataChange.Field)
            {
                case "pX":
                    position.x = (float)dataChange.Value;
                    break;
                case "pY":
                    position.y = (float)dataChange.Value;
                    break;
                case "pZ":
                    position.z = (float)dataChange.Value;
                    break;
                case "vX":
                    velocity.x = (float)dataChange.Value;
                    break;
                case "vY":
                    velocity.y = (float)dataChange.Value;
                    break;
                case "vZ":
                    velocity.z = (float)dataChange.Value;
                    break;
                case "rX":
                    _character.SetRotateX((float)dataChange.Value, AverageInterval);
                    break;
                case "rY":
                    _character.SetRotateY((float)dataChange.Value, AverageInterval);
                    break;
                default:
                    Debug.LogWarning("�� �������������� ��������� ����" + dataChange.Field);
                    break;
            }
        }

        _character.SetMovement(position, velocity, AverageInterval);
    }
}
