using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float speed = 1;
    void Update()
    {
        gameObject.transform.Translate(0, speed * Time.deltaTime, 0);
    }
}
