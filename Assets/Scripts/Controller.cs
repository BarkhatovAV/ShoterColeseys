using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _player;

    private void Update()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");

        _player.SetInput(inputH, inputV);

        SendMove();
    }

    private void SendMove()
    {
        _player.GetMoveInfo(out Vector3 position);
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            {"x",  position.x},
            {"y",  position.z}
        };
        MultiplayerManager.Instance.SendMessage("move", data);
    }
}
