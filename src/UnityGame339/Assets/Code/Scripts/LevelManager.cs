using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;

    private void Awake(){
        main = this;
    }

    private void Start()
    {
        currency = 1000;
    }

    public void IncreaseCurrency(int ammount)
    {
        currency += ammount;
    }

    public bool SpendCurrency(int ammount)
    {
        if (currency >= ammount)
        {
            currency -= ammount;
            return true;
        }
        else
        {
            Debug.Log("Not enough money");
            return false;
        }
    }
}
