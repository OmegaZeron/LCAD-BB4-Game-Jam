using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int m_hit_points = 100;
    private int m_max_hit_points;
    [SerializeField] private float m_deathTime = 3.0f;
    WaitForSeconds m_waitforseconds;

    private void Start()
    {
        m_max_hit_points = m_hit_points;
        m_waitforseconds = new WaitForSeconds(m_deathTime);
    }

    public int HitPoints
    {
        get
        {
            return m_hit_points;
        }
        set
        {
            m_hit_points += value;

            if (m_hit_points < 0)
            {
                StartCoroutine(Death());
            }
            else if (m_hit_points > m_max_hit_points)
            {
                m_hit_points = m_max_hit_points;
            }
        }
    }

    IEnumerator Death()
    {
        yield return m_waitforseconds;
        Destroy(gameObject);
    }
}
