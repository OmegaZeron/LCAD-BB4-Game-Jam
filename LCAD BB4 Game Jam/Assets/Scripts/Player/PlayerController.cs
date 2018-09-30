using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    private Collider2D m_collider;
    private Rigidbody2D m_rb;

    [SerializeField] private float m_jumpForce = 4;
    [SerializeField] private List<Transform> m_groundCheck;
    [SerializeField] private LayerMask ground;
    [SerializeField] private KeyCode m_jump;
    [SerializeField] private KeyCode m_up;

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider2D>();
        m_rb = GetComponent<Rigidbody2D>();
        m_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Start()
    {
		
	}
	
	void Update()
    {
        Jump();
	}

    private void Jump()
    {
        if (Input.GetKeyDown(m_jump))
        {
            bool grounded = false;
            foreach (Transform point in m_groundCheck)
            {
                RaycastHit2D hit = Physics2D.Linecast(point.position, point.position + Vector3.down * .05f, ground);
                if (hit)
                {
                    grounded = true;
                }
            }
            if (grounded)
            {
                m_rb.velocity += Vector2.up * m_jumpForce;
            }
        }
    }
}
