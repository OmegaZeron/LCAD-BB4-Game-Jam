using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    private Collider2D m_collider;
    private Rigidbody2D m_rb;

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider2D>();
        m_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
		
	}
	
	void Update(){
		
	}

    private void Jump()
    {

    }
}
