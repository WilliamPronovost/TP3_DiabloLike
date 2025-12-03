using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    private NavMeshAgent m_playerAgent;

    [Header("Player Actions")]
    [SerializeField] private InputActionAsset m_inputActions;
    private InputAction m_moveAction;
    private InputAction m_shootAction;

    [Header("Player Weapon")]
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private Transform m_bulletCollection;

    [Header("Player Death")]
    [SerializeField] private RawImage m_deathScreen;
    private float m_deathTimer;
    private float m_deathDelay = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerAgent = GetComponent<NavMeshAgent>();

        m_moveAction = m_inputActions.FindAction("Move");
        m_shootAction = m_inputActions.FindAction("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        Shooting();
    }
    private void Moving()
    {
        if (m_moveAction.WasPressedThisFrame())
        {
            Ray rayFromCamera = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(rayFromCamera, out RaycastHit hitInfo))
            {
                m_playerAgent.SetDestination(hitInfo.point);
            }
        }
    }
    private void Shooting()
    {
        if (m_shootAction.WasPressedThisFrame())
        {
            Ray rayFromCamera = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(rayFromCamera, out RaycastHit hitInfo))
            {
                Vector3 direction = (hitInfo.point - transform.position).normalized;
                direction.y = 0;
                Quaternion rotation = Quaternion.LookRotation(direction);
                GameObject newBullet = Instantiate(m_bulletPrefab, transform.position, rotation, m_bulletCollection);
            }
        }
    }
    public void PlayerDeath()
    {
        m_deathTimer += Time.deltaTime;
        if(m_deathTimer < m_deathDelay)
        {
            float timer = m_deathTimer / m_deathDelay;
            float alpha = Mathf.Lerp(0, 1, timer);
            Color color = m_deathScreen.color;
            color.a = alpha;
            m_deathScreen.color = color;
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
       
}
