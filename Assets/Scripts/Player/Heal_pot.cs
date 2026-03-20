using UnityEngine;
using UnityEngine.InputSystem;

public class DrinkPotion : MonoBehaviour
{
    public int healAmount = 25;
    public int potionCount = 3;

    private Health hp;

    private void Awake()
    {
        hp = GetComponent<Health>();
        if (hp == null)
        {
            Debug.LogError("Brak komponentu Health na graczu!");
        }
    }

    private void Update()
    {
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            UsePotion();
        }
    }

    private void UsePotion()
    {
        if (potionCount > 0 && hp != null)
        {
            if (hp.currentHP < hp.maxHP)
            {
                hp.healing(healAmount);
                potionCount--;
                Debug.Log("Użyto mikstury, pozostało: " + potionCount);
            }
            else
            {
                Debug.Log("Masz fulla kurwo");
            }
        }
        else
        {
            Debug.Log("Brak mikstur");
        }
    }
}