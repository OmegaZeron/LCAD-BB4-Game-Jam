using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Collectibles item;

    private string m_ingredientName;
    private SpriteRenderer sprite;
    private int hot;
    private int cold;
    private int wet;
    private int dry;
    private int toxic;

    public void Setup()
    {
        m_ingredientName = item.ingredientName;
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = item.sprite;
        hot = item.hot;
        cold = item.cold;
        wet = item.wet;
        dry = item.dry;
        toxic = item.toxic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            IngredientManager.Instance.AddIngredient(this);
        }
    }

}
