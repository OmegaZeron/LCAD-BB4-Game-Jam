﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    private Collider2D m_collider;
    private Rigidbody2D m_rb;
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
    public bool Sprinting { get; private set; }
    public bool Crouching { get; private set; }

    [SerializeField] private float m_jumpForce = 4;
    [SerializeField] private List<Transform> m_groundCheck;
    [SerializeField] private LayerMask ground;

    private Transform m_climbPoint;
    private bool m_canClimb;
    private bool m_climbing;
    [SerializeField] private float m_climbSpeed = 2;

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
        Movement();
        Jump();
        Climb();
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
                m_rb.velocity += Vector2.up * m_jumpForce;
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
            spd = m_crouchSpeed;
        }
        else if (Sprinting)
        {
            spd = m_sprintSpeed;
        }
        else
        {
            spd = m_speed;
        }

        if (Input.GetKey(m_right))
        {
            m_rb.velocity += Vector2.right * spd * Time.deltaTime;
        }
        else if (Input.GetKey(m_left))
        {
            m_rb.velocity += Vector2.left * spd * Time.deltaTime;
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
}
