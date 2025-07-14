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
            if (GameManager.instance.isPlayerDead)
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                this.enabled = false;
                this.GetComponent<EnemyDetector>().enabled = false;
                return;
            }

            // Skill seçiliyse input alma, ama animasyonu kontrol etmeye devam et
            if (GameManager.instance.isSkillSelected || SkillManager.instance.inputConsumedThisFrame)
            {
                // Hedefe ulaştıysa idle'a dön
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    anim.SetBool("isRunning", false);
                }
                return;
            }

            if (agent != null)
            {
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        agent.SetDestination(hit.point);
                        anim.SetBool("isRunning", true);
                    }
                }
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    anim.SetBool("isRunning", false); // Idle'a geç
                }
            }
        }
    }
}