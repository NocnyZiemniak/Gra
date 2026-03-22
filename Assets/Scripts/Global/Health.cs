using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHP;

    public int currentHP;

    private void Start()
    {
        currentHP = maxHP;
    }

    public void taking_damage(int damage)
    {
        currentHP -= damage;
        if(currentHP < 0)
        {
            Die();
        }
        Debug.Log("Zadano" + damage + "obrazen");
    }
    public void healing(int heal)
    {
        currentHP += heal;
        if (currentHP > maxHP)
        {
            currentHP=maxHP;
        }
        Debug.Log("Wyleczono" + heal + "Zycia");
    }
    void Die()
    {
        Time.timeScale = 0;
    }
}