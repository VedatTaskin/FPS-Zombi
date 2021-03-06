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
        if (currentWeapon!=null)
        {
            SpawnWeapon();
        }        
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

        currentWeapon.SpawnNewWeapon(mainCameraTransform.GetChild(0).GetChild(0),anim);
    }

    public void EquipWeapon(Weapon weaponType)
    {
        if (currentWeapon!=null)
        {
            currentWeapon.Drop();
        }
        currentWeapon = weaponType;
        SpawnWeapon();
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

        //Weapon ?n attack rate'ine g?re bekleme s?resi ayarlan?r;
        yield return new WaitForSeconds(currentWeapon.GetAttackRate);
        isAttacking = false;
    }

    public int GetDamage()
    {
        if (currentWeapon!=null)
        {
            return currentWeapon.GetDamage;
        }
        return 0;
    }
}
