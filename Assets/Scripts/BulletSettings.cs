using UnityEngine;

public class BulletSettings : MonoBehaviour
{
    [SerializeField] private float m_bulletSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, m_bulletSpeed * Time.deltaTime);
    }
}
