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

    public GameObject GetWepaonPrefab { get { return weaponPrefab; } }
    public int GetDamage { get { return damage; } }
    public float GetAttackRate { get { return attackRate; } }


}
