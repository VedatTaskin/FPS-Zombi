using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    [SerializeField] Weapon currentWeapon;

    private Transform mainCameraTransform;

    private Animator anim;

    private bool isAttacking;

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
        if (Mouse.current.leftButton.isPressed && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        anim.SetTrigger("Attack");
        isAttacking = true;

        //Weapon ýn attack rate'ine göre bekleme süresi ayarlanýr;
        yield return new WaitForSeconds(currentWeapon.GetAttackRate);
        isAttacking = false;
    }
}
