using UnityEngine;

public class Health : DeathEffectObject
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 25;
    
    public GameObject FloatingScorePrefab;
    
    private bool isDestroyed = false;

    public void TakeDamage(int dmg){
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed){
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            
            CreateDeathEffect();
            
            FloatingText floatingText = FloatingScorePrefab.GetComponent<FloatingText>();
            floatingText.SetText(currencyWorth);

            Instantiate(FloatingScorePrefab, transform.position, Quaternion.identity);
            
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
