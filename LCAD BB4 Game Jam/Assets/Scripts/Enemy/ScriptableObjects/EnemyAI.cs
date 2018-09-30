using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Enemy enemy;
    private int m_hp;
    private int m_damage;
    private int m_speed;
    private List<Collectibles> m_ingredientDrops;

    private bool m_enterCombat = false;
    private bool m_inCombat = false;

    [SerializeField] private List<Transform> m_groundCheck;

    private PlayerController player;

	void Start()
    {
        gameObject.name = enemy.Name;
        m_hp = enemy.Health;
        m_damage = enemy.Damage;
        m_speed = enemy.Speed;
        m_ingredientDrops = new List<Collectibles>(enemy.Ingredients);

        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        if (enemy.movement == Enemy.Movement.Idle)
        {
            while (true)
            {
                if (!m_inCombat)
                {
                    // do stuff
                    if (m_enterCombat)
                    {
                        m_enterCombat = false;
                        m_inCombat = true;
                        StartCoroutine(Combat());
                    }
                }
                yield return null;
            }
        }
        else if (enemy.movement == Enemy.Movement.Patrol)
        {
            bool amGoingLeft = true;
            while (true)
            {
                if (!m_inCombat)
                {
                    transform.position += amGoingLeft ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0) * m_speed;
                    foreach (Transform point in m_groundCheck)
                    {
                        if (!Physics2D.Linecast(point.position, point.position + Vector3.down * .05f))
                        {
                            amGoingLeft = !amGoingLeft;
                        }
                    }
                    if (m_enterCombat)
                    {
                        m_enterCombat = false;
                        m_inCombat = true;
                        StartCoroutine(Combat());
                    }
                }
                yield return null;
            }
        }
    }

    private IEnumerator Combat()
    {

        if (enemy.combat == Enemy.Combat.Dig)
        {
            while (m_inCombat)
            {
                // start dig animation
                // animation event on dig end for DigMove() function, check if already running
                yield return null;
            }
        }
        else if (enemy.combat == Enemy.Combat.Fight)
        {
            while (m_inCombat)
            {
                // do stuff
                yield return null;
            }
        }
        else if (enemy.combat == Enemy.Combat.Run)
        {
            while (m_inCombat)
            {
                Vector3 heading = transform.position - player.transform.position;
                transform.position += heading * m_speed;
                yield return null;
            }
        }
    }

    public void DigMove()
    {
        // find right distance to move, pop out on top of ground. If no viable distance, destroy self
        m_inCombat = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemy.combat == Enemy.Combat.Run)
        {
            m_enterCombat = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!player)
        {
            player = collision.gameObject.GetComponent<PlayerController>();
        }
        if (enemy.combat == Enemy.Combat.Dig && player)
        {
            if (!player.Crouching)
            {
                m_enterCombat = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemy.combat != Enemy.Combat.Dig)
        {
            m_inCombat = false;
        }
        player = null;
    }

    public void TakeDamage(int damage, bool crit)
    {
        if (crit && !m_inCombat)
        {
            damage *= 2;
        }
        m_hp -= Mathf.Clamp(damage, 0, m_hp);
        if (m_hp <= 0)
        {
            Die();
        }
        // flash red if crit
        m_enterCombat = true;
    }

    private void Die()
    {
        Debug.Log("Oh no I died!");
        // drop ingredients
        // death animation
        // Destroy(gameObject); // probably should be an animation event, and a lerping alpha change
    }
}
