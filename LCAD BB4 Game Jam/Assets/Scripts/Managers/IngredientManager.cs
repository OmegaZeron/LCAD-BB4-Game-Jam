using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public static IngredientManager Instance { get; private set; }

    public List<Pickup> ingredients { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        ingredients = new List<Pickup>();
    }

    public void AddIngredient(Pickup ingredient)
    {
        ingredients.Add(ingredient);
    }

    public void RemoveIngredientRand()
    {
        if (ingredients.Count > 0)
        {
            int rand = Random.Range(0, ingredients.Count);
            Debug.Log("Lost " + ingredients[rand]);
            ingredients.Remove(ingredients[rand]);
        }
    }
}
