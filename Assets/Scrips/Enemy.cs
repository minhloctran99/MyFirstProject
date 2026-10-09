using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string enemyType = "Goblin";
    public int damage = 25;

    public int healthPoints = 75;
    void Start()
    {
        Debug.Log("Enemy: " + enemyType);
        Debug.Log("Damage: " + damage);
        Debug.Log("Health Points: " + healthPoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
