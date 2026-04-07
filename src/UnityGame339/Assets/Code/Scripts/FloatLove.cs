using UnityEngine;

public class FloatingLove : MonoBehaviour
{
    [SerializeField] private TextMesh textMesh;

        [SerializeField] private TextAsset CantDateFile;
        [SerializeField] private TextAsset LoveFile;


    public float speed = 1;

private string[] linesCant;
private string[] linesLove;

    void Start() {
        // Split the file text by new line characters
        linesCant = CantDateFile.text.Split('\n');
        linesLove = LoveFile.text.Split('\n');

        // Choose a random line
        //string randomLine = linesCant[Random.Range(0, linesCant.Length)];
        SetText();
    }

    
    void Update()
    {
        gameObject.transform.Translate(0, speed * Time.deltaTime, 0);
    }

    public void SetText()
    {
        textMesh.text = linesCant[Random.Range(0, linesCant.Length)];
    }
    

}
