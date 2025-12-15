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
    private InputAction m_interact;

    [Header("Player Weapon")]
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private Transform m_bulletCollection;

    [Header("Player Death")]
    [SerializeField] private RawImage m_deathScreen;
    private float m_deathTimer;
    private float m_deathDelay = 1.0f;
    private bool m_isAlive = true;

    [Header("Audio")]
    [SerializeField] private AudioSource m_musicSource;
    [SerializeField] private AudioSource m_soundsSource;
	[SerializeField] private AudioClip m_gameSoundtrackSFX;
	[SerializeField] private AudioClip m_shotSoundSFX;
    [SerializeField] private AudioClip m_deathTuneSFX;

    private Interactable m_currentInteractable = null;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerAgent = GetComponent<NavMeshAgent>();

        m_moveAction = m_inputActions.FindAction("Move");
        m_shootAction = m_inputActions.FindAction("Shoot");
        m_interact = m_inputActions.FindAction("Interact");

        m_musicSource = GetComponent<AudioSource>();
        m_soundsSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		m_musicSource.clip = m_gameSoundtrackSFX;
		m_musicSource.Play();

        Moving();
        Shooting();

        if (!m_isAlive)
        {
            Dying();
            return;
        }

        if (m_interact.WasPressedThisFrame() && m_currentInteractable != null)
        {
            m_currentInteractable.Interact();
        }
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
                GameObject newBullet = Instantiate(m_bulletPrefab, transform.position + Vector3.up * 0.5f, rotation, m_bulletCollection);
				m_soundsSource.clip = m_shotSoundSFX;
				m_musicSource.Play();
			}
        }
    }
    public void PlayerDeath()
    {
        m_isAlive = false;
    }
    private void Dying()
    {
		m_deathTimer += Time.deltaTime;
		if (m_deathTimer < m_deathDelay)
		{
			float timer = m_deathTimer / m_deathDelay;
			float alpha = Mathf.Lerp(0, 1, timer);
			Color color = m_deathScreen.color;
			color.a = alpha;
			m_deathScreen.color = color;
			m_soundsSource.clip = m_deathTuneSFX;
			m_musicSource.Play();
		}
		else
		{
			SceneManager.LoadScene(1);
		}
	}
    public void SetInteractable(Interactable interactable)
    {
        m_currentInteractable = interactable;
    }
}
