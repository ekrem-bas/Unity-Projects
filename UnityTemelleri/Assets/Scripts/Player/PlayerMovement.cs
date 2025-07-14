using System.Collections;
using System.Collections.Generic;
using Scripts.Enemy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        // Oyuncuyu hareket ettirirken kullanilacak olan NavMeshAgent
        [SerializeField] private NavMeshAgent agent;
        private Camera cam;
        public PlayerData playerData;
        private float speed; // Oyuncunun hareket hızı
        Animator anim;

        void Awake()
        {
            anim = GetComponent<Animator>(); // Animator bileşenini al
            agent = GetComponent<NavMeshAgent>();
            speed = playerData.speed; // PlayerData'dan hızı al
        }

        void Start()
        {
            cam = Camera.main; // Ana kamerayı al
            agent.speed = speed; // NavMeshAgent'in hızını ayarla
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance.isSkillSelected) // Eğer bir skill tıklanmışsa veya input consume edildiyse
            {
                return; // Hiçbir hareket işlemi yapma
            }

            if (SkillManager.instance.inputConsumedThisFrame)
            {
                return; // Eğer bu frame'de input consume edildiyse hareket etme
            }
            
            if (GameManager.instance.isPlayerDead) // Eğer oyuncu ölmüşse
            {
                agent.isStopped = true; // NavMeshAgent'i durdur
                agent.velocity = Vector3.zero; // Hızını sıfırla
                this.enabled = false; // Bu scripti devre dışı bırak
                this.GetComponent<EnemyDetector>().enabled = false; // EnemyDetector scriptini de devre dışı bırak
                return; // Hiçbir şey yapma
            }
            else
            {
                if (agent != null)
                {
                    // Fare sol tusuna basildiginda ve UI elemanlari uzerinde degilse
                    if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                    {
                        // Fare sol tusuna basildiginda fare konumunu al
                        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                        // RaycastHit yapisi ile raycast sonucunu tut
                        RaycastHit hit;
                        // Raycast ile fare konumundaki objeyi kontrol et
                        // GroundLayerMask ile sadece "Ground" katmanındaki objelere bak
                        if (Physics.Raycast(ray, out hit))
                        {
                            // NavMeshAgent'i hedef konuma hareket ettir
                            agent.SetDestination(hit.point);
                            anim.SetBool("isRunning", true); // Koşma animasyonunu başlat
                        }
                    }
                    // Hedefe ulaştıysa idle'a dön
                    if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                    {
                        anim.SetBool("isRunning", false); // Idle'a geç
                    }
                }
            }
        }
    }
}