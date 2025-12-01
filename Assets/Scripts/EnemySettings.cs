using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemySettings : MonoBehaviour
{
    [SerializeField] private Transform m_player;
    private NavMeshAgent m_enemyAgent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_enemyAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        m_enemyAgent.SetDestination(m_player.position);
    }
    private void OnTriggerEnter(Collider collision)
    {
        BulletSettings bullet = collision.GetComponent<BulletSettings>();
        if (bullet != null)
        {
        Destroy(gameObject);
        }
        PlayerControls player = collision.GetComponent<PlayerControls>();
        if (player != null)
        {
         player.PlayerDeath();
        }   
    }

}
