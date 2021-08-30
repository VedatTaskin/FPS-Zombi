using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Hit : MonoBehaviour
{
    private Transform owner;
    private int damage;
    private Collider hitCollider;
    private Rigidbody rb;
    private Animator anim;

    public GameObject BloodPrefab;


    private void Awake()
    {
        rb=GetComponent<Rigidbody>();
        owner = transform.root;
        hitCollider = GetComponent<BoxCollider>();
        hitCollider.isTrigger = true;
        rb.useGravity = false;
        rb.isKinematic = true;
        hitCollider.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (owner.CompareTag("Player"))
        {
          damage=  owner.GetComponent<AttackController>().GetDamage();
          anim = GetComponentInParent<Transform>().GetComponentInParent<Animator>();    

        }

        
        else if (owner.CompareTag("Enemy"))
        {
          damage=  owner.GetComponent<EnemyController>().GetDamage();
          anim = GetComponentInParent<Animator>();
        }
        else
        {
            enabled = false;
        }
    }

    private void Update()
    {
        if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime>=0.5f && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.55f)
        {
            ControlTheCollider(true);            
        }
        else
        {
            ControlTheCollider(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null && health.gameObject!=owner.gameObject)
        {
            health.GiveDamage(damage);
            Instantiate(BloodPrefab, other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position),Quaternion.identity);
        }
    }

    private void ControlTheCollider(bool open)
    {
        hitCollider.enabled = open;
    }


}
