using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy")]
[System.Serializable]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public int health;
    public int damage;
    public int speed;
    public List<Collectibles> Ingredients { get; private set; }

    public enum Movement
    {
        Idle,
        Patrol
    }
    public Movement movement;

    public enum Combat
    {
        Dig,
        Run,
        Fight
    }
    public Combat combat;
}
