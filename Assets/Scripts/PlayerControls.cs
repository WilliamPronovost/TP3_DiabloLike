using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private NavMeshAgent m_playerAgent;

    [SerializeField] private InputActionAsset m_inputActions;
    private InputAction m_moveAction;
    private InputAction m_shootAction;

    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private Transform m_bulletCollection;
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
                SpawnBullet();
            }
        }
    }
    private Transform SpawnBullet()
    {
        GameObject newBullet = Instantiate(m_bulletPrefab, transform.position, Quaternion.identity, m_bulletCollection);
        return newBullet.transform;
    }

}
