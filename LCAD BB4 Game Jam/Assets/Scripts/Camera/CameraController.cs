using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    [SerializeField] private int focal_point_speed = 15;
    [SerializeField] private int layer_difference = 2;
    [SerializeField] private PlayerController pc;

    [SerializeField] private LayerMask layerMask5;
    [SerializeField] private LayerMask layerMask4;
    [SerializeField] private LayerMask layerMask3;
    [SerializeField] private LayerMask layerMask2;
    [SerializeField] private LayerMask layerMask1;
    [SerializeField] private GameObject[] layerParallax1;
    [SerializeField] private GameObject[] layerParallax2;
    [SerializeField] private GameObject[] layerParallax3;
    [SerializeField] private GameObject[] layerParallax4;
    [SerializeField] private GameObject[] layerParallax5;

    private void Awake()
    {
        layerParallax5 = FindGameObjectsInLayer(16);
        layerParallax4 = FindGameObjectsInLayer(15);
        layerParallax3 = FindGameObjectsInLayer(14);
        layerParallax2 = FindGameObjectsInLayer(13);
        layerParallax1 = FindGameObjectsInLayer(12);
    }

    void Start()
    {
        layerMask5.value = focal_point_speed;
        layerMask4.value = layerMask5.value - layer_difference;
        layerMask3.value = layerMask4.value - layer_difference;
        layerMask2.value = layerMask3.value - layer_difference;
        layerMask1.value = layerMask2.value - layer_difference;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            foreach (GameObject go in layerParallax1)
            {
                if (gameObject.transform.position.x >= -6 && pc.Sprinting)
                    go.transform.Translate(Vector2.left * (go.layer + 5.0f) * Time.deltaTime);
                else if (gameObject.transform.position.x >= -6)
                    go.transform.Translate(Vector2.left * go.layer * Time.deltaTime);
            }
            foreach (GameObject go in layerParallax2)
            {
                if (gameObject.transform.position.x >= -6 && pc.Sprinting)
                    go.transform.Translate(Vector2.left * (go.layer + 5.0f) * Time.deltaTime);
                else if (gameObject.transform.position.x >= -6)
                    go.transform.Translate(Vector2.left * go.layer * Time.deltaTime);
            }
            foreach (GameObject go in layerParallax3)
            {
                if (gameObject.transform.position.x >= -6 && pc.Sprinting)
                    go.transform.Translate(Vector2.left * (go.layer + 5.0f) * Time.deltaTime);
                else if (gameObject.transform.position.x >= -6)
                    go.transform.Translate(Vector2.left * go.layer * Time.deltaTime);
            }
            foreach (GameObject go in layerParallax4)
            {
                if (gameObject.transform.position.x >= -6 && pc.Sprinting)
                    go.transform.Translate(Vector2.left * (go.layer + 5.0f) * Time.deltaTime);
                else if (gameObject.transform.position.x >= -6)
                    go.transform.Translate(Vector2.left * go.layer * Time.deltaTime);
            }
            foreach (GameObject go in layerParallax5)
            {
                if (gameObject.transform.position.x >= -6 && pc.Sprinting)
                    go.transform.Translate(Vector2.left * (go.layer + 5.0f) * Time.deltaTime);
                else if (gameObject.transform.position.x >= -6)
                    go.transform.Translate(Vector2.left * go.layer * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            foreach (GameObject go in layerParallax1)
            {
                go.transform.Translate(Vector2.right * go.layer * Time.deltaTime);
            }
            foreach (GameObject go in layerParallax2)
            {
                go.transform.Translate(Vector2.right * go.layer * Time.deltaTime);
            }
            foreach (GameObject go in layerParallax3)
            {
                go.transform.Translate(Vector2.right * go.layer * Time.deltaTime);
            }
            foreach (GameObject go in layerParallax4)
            {
                go.transform.Translate(Vector2.right * go.layer * Time.deltaTime);
            }
            foreach (GameObject go in layerParallax5)
            {
                go.transform.Translate(Vector2.right * go.layer * Time.deltaTime);
            }
        }
    }

    GameObject[] FindGameObjectsInLayer(int layer)
    {
        var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var goList = new System.Collections.Generic.List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();
    }
}

