using System.Collections;
using System.Collections.Generic;
using Scripts.Enemy;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public PlayerData playerData;
    public float meteorDamage;
    public float fallSpeed = 50f;
    public GameObject impactEffect;
    public GameObject impactEffectInstance;
    public float meteorFallStartHeight = 30f;
    private Rigidbody rb;
    private ObjectPooler<Meteor> pooler;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 spawnPosition, ObjectPooler<Meteor> pooler)
    {
        this.pooler = pooler;
        transform.position = spawnPosition;
        meteorDamage = playerData.meteorSkillDamage;

        // Rigidbody'yi sıfırla ve doğru hızda düşür
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = false;
            rb.velocity = Vector3.down * fallSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            // Alan içindeki tüm düşmanları bul ve hasar ver
            float radius = gameObject.GetComponent<SphereCollider>().radius;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var hit in hitColliders)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Enemy enemy = hit.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(meteorDamage);
                    }
                }
            }

            impactEffectInstance = Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(impactEffectInstance, 1f);
            pooler.Release(this); // Pool'a geri gönder
        }
    }

    private void OnDisable()
    {
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }
}