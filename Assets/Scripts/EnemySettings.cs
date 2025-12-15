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
    private void OnCollisionEnter(Collision collision)
    {
        PlayerControls player = collision.collider.GetComponent<PlayerControls>();
        if (player != null)
        {
            player.PlayerDeath();
        }   
    }
    public void EnemyDeath()
    {
        Destroy(gameObject);
    }

}
