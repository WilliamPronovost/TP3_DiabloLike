using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject m_pressE;
    [SerializeField] GameObject m_wallToPull;
    private float m_pullTimer;
	private float m_pullDelay;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        m_pressE.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other)
	{
        PlayerControls player = other.GetComponent<PlayerControls>();
		if (player != null)
        {
		    m_pressE.SetActive(true);
            player.SetInteractable(this);
        }
	}
	private void OnTriggerExit(Collider other)
	{

		PlayerControls player = other.GetComponent<PlayerControls>();
		if (player != null)
		{
			m_pressE.SetActive(false);
			player.SetInteractable(null);
		}
	}
    public void Interact()
    {
        Vector3 wallPulled = new Vector3(0, 2, 0);
		m_pullTimer += Time.deltaTime;
		if (m_pullTimer < m_pullDelay)
		{
			float timer = m_pullTimer / m_pullDelay;
			Vector3 wallMovement = Vector3.Lerp(m_wallToPull.transform.position, wallPulled, timer);
		}
	}
}
