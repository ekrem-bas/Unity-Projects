using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public GameObject bloodEffectPrefab;
    private Rigidbody rb;
    private float timer;
    private float lifeTime = 7f;
    public ObjectPooler<Projectile> pooler; // Public yap ki Enemy erişebilsin

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 targetPosition, float damage, float speed, ObjectPooler<Projectile> pooler)
    {
        this.damage = damage;
        this.pooler = pooler;
        timer = 0f;

        if (rb != null)
        {
            rb.isKinematic = false;  // Önce kinematic'i kapat
            rb.velocity = Vector3.zero;  // Sonra velocity'yi sıfırla

            // Hedef pozisyona doğru direction hesapla
            Vector3 direction = (targetPosition - transform.position).normalized;
            rb.AddForce(direction * speed, ForceMode.Impulse);
        }

        var trail = GetComponent<TrailRenderer>();
        if (trail != null) trail.Clear();

        gameObject.SetActive(true);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            pooler.Release(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            Vector3 forward = transform.forward;
            Quaternion rotation = Quaternion.LookRotation(-forward);

            if (bloodEffectPrefab != null)
            {
                Instantiate(bloodEffectPrefab, hitPoint, rotation);
            }
            pooler.Release(this); // Destroy yerine pool'a geri gönder
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