using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] Weapon currentWeapon;

    private Transform mainCameraTransform;

    void Awake()
    {
        mainCameraTransform = GameObject.FindWithTag("CameraPoint").transform;
        SpawnWeapon();
    }


    void SpawnWeapon()
    {
        if (currentWeapon==null)
        {
            return;
        }

        currentWeapon.SpawnNewWeapon(mainCameraTransform.GetChild(0).GetChild(0));
    }
}
