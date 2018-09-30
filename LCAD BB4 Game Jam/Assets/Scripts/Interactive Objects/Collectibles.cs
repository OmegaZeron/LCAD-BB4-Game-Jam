using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "Ingredient")]
public class Collectibles : ScriptableObject
{
    public string ingredientName;

    public int hot;
    public int cold;
    public int wet;
    public int dry;
    public int toxic;
}
