using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // The only thing this class does, is looks at components and updates the UI
    [Header("Health")]
    public GameObject healthbar;
    private float hbMaxWidth;
    private RectTransform hbTransform;
    [Header("Score")]
    public TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        hbTransform = healthbar.GetComponent<RectTransform>();
        hbMaxWidth = hbTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 hbCurrentSize = hbTransform.sizeDelta;
        hbCurrentSize.x = hbMaxWidth * GameManager.GM.GetEarthHealth();
        hbTransform.sizeDelta = hbCurrentSize;
        score.text = "" + GameManager.GM.Score;
    }
}
