using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    private Collider2D m_collider;
    private Rigidbody2D m_rb;
    private SpriteRenderer sr;
    [SerializeField] private KeyCode m_right;
    [SerializeField] private KeyCode m_left;
    [SerializeField] private KeyCode m_sprint;
    [SerializeField] private KeyCode m_jump;
    [SerializeField] private KeyCode m_up;
    [SerializeField] private KeyCode m_down;
    [SerializeField] private KeyCode m_crouch;

    [SerializeField] private float m_speed = 10.0f;
    [SerializeField] private float m_sprintSpeed = 15;
    [SerializeField] private float m_crouchSpeed = 5;
    [SerializeField] private int damage = 1;
    public bool Sprinting { get; private set; }
    public bool Crouching { get; private set; }

    [SerializeField] private float m_jumpForce = 4;
    [SerializeField] private List<Transform> m_groundCheck;
    [SerializeField] private LayerMask ground;

    private Transform m_climbPoint;
    private bool m_canClimb;
    private bool m_climbing;
    [SerializeField] private float m_climbSpeed = 2;

    private Animator m_anim;

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_collider = GetComponent<BoxCollider2D>();
        m_rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        m_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
	
	void Update()
    {
        Movement();
        Jump();
        Climb();
        Attack();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ingredient")
        {
            Pickup ing = collision.gameObject.GetComponent<Pickup>();
            IngredientManager.Instance.AddIngredient(ing);
            ing.GetComponent<Renderer>().enabled = false;
            ing.GetComponent<Collider2D>().enabled = false;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Climbable")
        {
            m_canClimb = true;
            m_climbPoint = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_canClimb = false;
        m_climbPoint = null;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(m_jump))
        {
            m_anim.SetBool("jumping", false);
            bool grounded = false;
            foreach (Transform point in m_groundCheck)
            {
                if (Physics2D.Linecast(point.position, point.position + Vector3.down * .05f, ground))
                {
                    grounded = true;
                }
            }
            if (grounded)
            {
                m_anim.SetBool("jumping", true);
                m_rb.velocity += Vector2.up * m_jumpForce;
            }
            else
            {
                m_anim.SetBool("jumping", false);
            }
        }
    }

    private void Movement()
    {
        Crouching = Input.GetKey(m_crouch);
        Sprinting = Input.GetKey(m_sprint);
        float spd = 0;
        if (Crouching)
        {
            m_anim.SetBool("walking", true);
            spd = m_crouchSpeed;
        }
        else if (Sprinting)
        {
            m_anim.SetBool("walking", true);
            spd = m_sprintSpeed;
        }
        else
        {
            m_anim.SetBool("walking", true);
            spd = m_speed;
        }

        if (Input.GetKey(m_right))
        {
            m_anim.SetBool("walking", true);
            sr.flipX = true;
            m_rb.velocity += Vector2.right * spd * Time.deltaTime;
        }
        else if (Input.GetKey(m_left))
        {
            m_anim.SetBool("walking", true);
            sr.flipX = false;
            m_rb.velocity += Vector2.left * spd * Time.deltaTime;
        }
        else
        {
            m_anim.SetBool("walking", false);
        }
    }

    private void Climb()
    {
        if (m_canClimb && !m_climbing && (Input.GetKey(m_up) || Input.GetKey(m_down)))
        {
            transform.position = new Vector3(m_climbPoint.position.x, transform.position.y, 0);
            m_rb.gravityScale = 0;
            m_rb.velocity = Vector3.zero;
            m_climbing = true;
        }
        else if (m_canClimb && m_climbing)
        {
            transform.position += Vector3.up * Input.GetAxis("Vertical") * m_climbSpeed * Time.deltaTime;
        }
        if (Input.GetKey(m_right) || Input.GetKey(m_left))
        {
            m_rb.gravityScale = 1;
            m_climbing = false;
        }
    }

    private void Attack()
    {
        Debug.DrawLine(transform.position, transform.position + (sr.flipX ? Vector3.right : Vector3.left) * 2.5f, Color.red);
        if (Input.GetButtonDown("Fire1"))
        {
            m_anim.SetBool("attack", true);
            m_anim.SetFloat("attackNum", Random.Range(0,2));

            RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, transform.position + (sr.flipX ? Vector3.right : Vector3.left) * 2.5f);
            foreach (RaycastHit2D hit in hits)
            {
                EnemyAI enemy = hit.collider.gameObject.GetComponent<EnemyAI>();
                if (enemy)
                {
                    enemy.TakeDamage(damage, Crouching ? true : false);
                }
            }
        }
    }

    public void TakeDamage()
    {
        IngredientManager.Instance.RemoveIngredientRand();
    }
}
