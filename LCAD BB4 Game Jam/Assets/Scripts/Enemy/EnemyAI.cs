using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    SpriteRenderer sprite;

    public Enemy enemy;
    private int m_hp;
    private int m_damage;
    private int m_speed;
    [SerializeField] private Pickup prefab;
    private List<Collectibles> m_ingredientDrops;

    private bool m_enterCombat = false;
    private bool m_inCombat = false;

    [SerializeField] private List<Transform> m_groundCheck;

    private PlayerController player;

	void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        gameObject.name = enemy.enemyName;
        m_hp = enemy.health;
        m_damage = enemy.damage;
        m_speed = enemy.speed;
        m_ingredientDrops = new List<Collectibles>(enemy.Ingredients);

        StartCoroutine(Move());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(FlashRed());
        }
    }

    private IEnumerator Move()
    {
        if (enemy.movement == Enemy.Movement.Idle)
        {
            while (true)
            {
                if (!m_inCombat)
                {
                    // do stuff?
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
                    transform.position += (amGoingLeft ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0)) * m_speed * Time.deltaTime;
                    foreach (Transform point in m_groundCheck)
                    {
                        if (!Physics2D.Linecast(point.position, point.position + Vector3.down * .05f))
                        {
                            amGoingLeft = !amGoingLeft;
                            sprite.flipX = !sprite.flipX;
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
                if (player)
                {
                    Vector3 heading = (transform.position - player.transform.position).normalized;
                    transform.position += heading * m_speed * Time.deltaTime;
                }
                yield return null;
            }
        }
    }

    public void DigMove()
    {
        // find right distance to move, pop out on top of ground. If no viable distance, destroy self
        transform.position += Vector3.right * 10;
        if (!Physics2D.OverlapCircle(transform.position, .5f))
        {
            // aniamtion
        }
        m_inCombat = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!player)
        {
            player = collision.gameObject.GetComponent<PlayerController>();
        }
        if (player && enemy.combat == Enemy.Combat.Run) // Testing only, will be on damaged later
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
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            m_inCombat = false;
            player = null;
        }
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
        sprite.color = Color.red;
        m_enterCombat = true;
    }

    private IEnumerator FlashRed()
    {
        int time = 5;
        while (time >= 0)
        {
            sprite.color = time % 2 == 0 ? Color.white : Color.red;
            time--;
            yield return new WaitForSeconds(.02f);
        }
    }

    private void Die()
    {
        Debug.Log("Oh no I died!");
        foreach (Collectibles item in m_ingredientDrops)
        {
            // create new Ingredient object, add SO to it and update, drop it
            Pickup ingredient = Instantiate(prefab);
            ingredient.item = item;
            ingredient.Setup();
            float rand = Random.Range(-1f, 1);
            Rigidbody2D rb = ingredient.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(rand, 1, 0);
        }
        // death animation
        // Destroy(gameObject); // probably should be an animation event, and a lerping alpha change
    }
}
