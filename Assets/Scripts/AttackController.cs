using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] Weapon currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        print("Damage " + currentWeapon.GetDamage);
        print("Attack Rate " + currentWeapon.GetAttackRate);
    }

    // Update is called once per frame
    void Update()
    {
      

    }
}
