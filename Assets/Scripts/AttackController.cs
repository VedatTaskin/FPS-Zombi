using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    [SerializeField] Weapon currentWeapon;

    private Transform mainCameraTransform;

    private Animator anim;

    void Awake()
    {
        mainCameraTransform = GameObject.FindWithTag("CameraPoint").transform;
        anim = mainCameraTransform.GetChild(0).GetComponent<Animator>();
        SpawnWeapon();
    }

    private void Update()
    {
        Attack();
    }


    void SpawnWeapon()
    {
        if (currentWeapon==null)
        {
            return;
        }

        currentWeapon.SpawnNewWeapon(mainCameraTransform.GetChild(0).GetChild(0));
    }

    void Attack()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            anim.SetTrigger("Attack");
        }
    }
}
