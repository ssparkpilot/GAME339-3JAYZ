using UnityEngine;

public class HoverText : MonoBehaviour
{
    [SerializeField] private TextMesh textMesh;
    [SerializeField] private Vector3 offset = new Vector3(0, 0.8f, 0);

    public void Show(string text, Vector3 worldPosition)
    {
        textMesh.text = text;
        transform.position = worldPosition + offset;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}