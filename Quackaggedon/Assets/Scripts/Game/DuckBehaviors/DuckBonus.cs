using UnityEngine;

public class DuckBonus : MonoBehaviour, IDuck
{
    public static int AmountOfDucks { get; private set; }
    
    public void OnClick()
    {
        
    }
    
    private void OnEnable()
    {
        SetDuckAmount(AmountOfDucks + 1);
    }
    
    private void OnDisable()
    {
        SetDuckAmount(AmountOfDucks - 1);
    }
    
    public static void SetDuckAmount(int amount)
    {
        AmountOfDucks = amount;
    }
}