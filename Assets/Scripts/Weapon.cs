using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Weapon",menuName ="Weapons/New Weapon",order =0)]
public class Weapon : ScriptableObject
{
    [Header("Settings")]
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] int damage;
    [SerializeField] float attackRate;
    [SerializeField] Vector3 positionOffset = Vector3.zero;
    [SerializeField] Vector3 scaleOffset = Vector3.zero;

    private GameObject weaponClone;

    public GameObject GetWepaonPrefab { get { return weaponPrefab; } }
    public int GetDamage { get { return damage; } }
    public float GetAttackRate { get { return attackRate; } }


    public void SpawnNewWeapon(Transform parent)
    {
        weaponClone = Instantiate(weaponPrefab, Vector3.zero, Quaternion.identity, parent);
        weaponClone.transform.position = parent.position;
        weaponClone.transform.rotation = parent.rotation;
        weaponClone.transform.localScale = weaponClone.transform.localScale + scaleOffset;
        weaponClone.transform.localPosition = Vector3.zero + positionOffset;

    }

}
