using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField]
    private float despawnTime = 8f;
    public int points = 100;

    private void Start()
    {
        Object.Destroy(gameObject, despawnTime);
    }

    protected virtual void Eat()
    {
        FindObjectOfType<GameManager>().PlayFruitSound();
        FindObjectOfType<GameManager>().FruitEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
}
