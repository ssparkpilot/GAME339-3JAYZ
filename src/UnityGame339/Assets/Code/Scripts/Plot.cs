using TMPro;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    
    private GameObject tower;
    private Color startColor;

    public AudioSource audioSource;
    public AudioClip placeSound;

    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;

    private void Start()
    {
        startColor = sr.color;
    }
    
    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (tower != null) return;

        GameObject towerToBuild = BuildManager.main.GetSelectedTower();
        tower = Instantiate(towerToBuild, transform.position, Quaternion.identity);
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        //make the audiosource play at half the volume
        audioSource.volume = 0.5f;
        //play the place sound at the randomized pitch
        audioSource.PlayOneShot(placeSound);
    }
}
