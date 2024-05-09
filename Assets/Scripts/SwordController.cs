using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private CanUseLikeWeapon _weapon;
    [SerializeField] private BoxCollider _swordCollider;

    private void Start()
    {
        _weapon = GetComponent<CanUseLikeWeapon>();
        _swordCollider = GetComponentInChildren<BoxCollider>();
    }



    public void GetNewComponentofWeapon()
    {
        _swordCollider = GetComponentInChildren<BoxCollider>();
        _weapon = GetComponent<CanUseLikeWeapon>();
    }

    public void EnableSwordCollision()
    {
        _swordCollider.enabled = true;
    }
    public void DisableSwordCollision()
    {
        _swordCollider.enabled = false;
    }

}
