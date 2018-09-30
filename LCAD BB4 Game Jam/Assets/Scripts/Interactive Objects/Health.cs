using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int m_hitPoints = 100;
    private int m_maxHitPoints;
    [SerializeField] private float m_deathTime = 3.0f;
    WaitForSeconds m_waitforseconds;

    private void Start()
    {
        m_maxHitPoints = m_hitPoints;
        m_waitforseconds = new WaitForSeconds(m_deathTime);
    }

    public int HitPoints
    {
        get
        {
            return m_hitPoints;
        }
        set
        {
            m_hitPoints += value;

            if (m_hitPoints < 0)
            {
                StartCoroutine(Death());
            }
            else if (m_hitPoints > m_maxHitPoints)
            {
                m_hitPoints = m_maxHitPoints;
            }
        }
    }

    IEnumerator Death()
    {
        yield return m_waitforseconds;
        Destroy(gameObject);
    }
}
