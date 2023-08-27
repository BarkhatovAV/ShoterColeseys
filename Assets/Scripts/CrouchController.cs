using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchController : MonoBehaviour
{
    private const string _crouchName = "isCrouch";

    [SerializeField] private Animator _squatAnumator;

    private bool _isCrouch = false;

    public void SetInputCrouch()
    {
        _isCrouch = !_isCrouch;
        _squatAnumator.SetBool(_crouchName, _isCrouch);
    }
}
