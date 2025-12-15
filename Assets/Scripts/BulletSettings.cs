using UnityEngine;

public class BulletSettings : MonoBehaviour
{
    [SerializeField] private float m_bulletSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, m_bulletSpeed * Time.deltaTime);
    }
	private void OnCollisionEnter(Collision collision)
	{
		EnemySettings enemy = collision.gameObject.GetComponent<EnemySettings>();
        if (enemy != null) {
            enemy.EnemyDeath();
		}
		Destroy(gameObject);
	}
}
