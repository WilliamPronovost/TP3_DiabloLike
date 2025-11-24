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
        transform.Translate(m_bulletSpeed, 0, 0);
    }
}
