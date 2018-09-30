using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy")]
[System.Serializable]
public class Enemy : ScriptableObject
{
    [SerializeField] private string m_name;
    [SerializeField] private int m_health;
    [SerializeField] private int m_damage;
    [SerializeField] private int m_speed;
    [SerializeField] private List<Ingredient> m_ingredient;
}
