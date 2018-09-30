using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy")]
[System.Serializable]
public class Enemy : ScriptableObject
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public int Damage { get; private set; }
    public int Speed { get; private set; }
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
