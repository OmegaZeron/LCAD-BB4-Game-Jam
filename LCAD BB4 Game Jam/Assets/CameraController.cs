using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    [SerializeField] private int focal_point_speed = 5;
    [SerializeField] private int layer_difference = 1;

    [SerializeField] private LayerMask layer5;
    [SerializeField] private LayerMask layer4;
    [SerializeField] private LayerMask layer3;
    [SerializeField] private LayerMask layer2;
    [SerializeField] private LayerMask layer1;
    [SerializeField] private List<GameObject> background;
    [SerializeField] private Transform player;

    void Start()
    {
        layer3.value = focal_point_speed;
        layer2.value = layer3.value - layer_difference;
        layer1.value = layer2.value - layer_difference;
        layer4.value = layer3.value + layer_difference;
        layer5.value = layer4.value + layer_difference;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            foreach (GameObject go in background)
            {
                if (gameObject.transform.position.x > -10)
                go.transform.Translate(Vector2.left * go.layer * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            foreach (GameObject go in background)
            {
                go.transform.Translate(Vector2.right * go.layer * Time.deltaTime);
            }
        }
    }

    
}

