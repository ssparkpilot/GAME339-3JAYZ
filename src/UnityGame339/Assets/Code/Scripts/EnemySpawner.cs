using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    public AudioSource audioSource;
    public AudioClip chudSpawn;
    public AudioClip waveCompleteSFX;
    public AudioClip waveStartSFX;

    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;


    private void Awake(){
        onEnemyDestroy.AddListener(onEnemyDestroyed);
    }

    private void Start(){
        StartCoroutine(StartWave());
    }

    private void Update(){
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= (1f/enemiesPerSecond) && enemiesLeftToSpawn > 0){
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0){
            EndWave();
        }
    }

    private void onEnemyDestroyed(){
        enemiesAlive--;
    }

    private IEnumerator StartWave() {
        yield return new WaitForSeconds(timeBetweenWaves);

        audioSource.pitch = Random.Range(minPitch, maxPitch);
        //make the audiosource play at half the volume
        audioSource.volume = 0.5f;
        //play the wave start sound at the randomized pitch
        audioSource.PlayOneShot(waveStartSFX);

        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void EndWave(){
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        //make the audiosource play at half the volume
        audioSource.volume = 0.5f;
        //play the wave complete sound at the randomized pitch
        audioSource.PlayOneShot(waveCompleteSFX);
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy(){
        GameObject prefabToSpawn =  enemyPrefabs[0];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        //make the audiosource play at half the volume
        audioSource.volume = 0.5f;
        //play the shoot sound at the randomized pitch
        audioSource.PlayOneShot(chudSpawn);
    }

    private int EnemiesPerWave() {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}
