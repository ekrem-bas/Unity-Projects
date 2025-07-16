using System.Collections;
using System.Collections.Generic;
using Scripts.Enemy;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public PlayerData playerData;
    public float beamDamage;
    public float beamStartHeight = 10f;
    public float beamFallSpeed = 30f;
    private Transform targetEnemy; // Takip edilecek düşman

    // Beam spawn edilirken hedef atanacak
    public void SetTarget(Transform enemy)
    {
        targetEnemy = enemy;
        beamDamage = playerData.beamSkillDamage;
        // Beam'i düşmanın kafasının üstünde başlat
        transform.position = enemy.position + Vector3.up * beamStartHeight;
    }

    void Update()
    {
        if (targetEnemy != null)
        {
            // Beam'i düşmanın x-z pozisyonuna hizala ve aşağıya doğru hareket ettir
            Vector3 target = new Vector3(targetEnemy.position.x, transform.position.y, targetEnemy.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, beamFallSpeed * Time.deltaTime);
            transform.position += Vector3.down * beamFallSpeed * Time.deltaTime;
        }
        else
        {
            // Hedef yoksa sadece aşağıya düşsün
            transform.position += Vector3.down * beamFallSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(beamDamage);
            Destroy(gameObject);
        }
    }
}
