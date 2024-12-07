using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // The only thing this class does, is looks at components and updates the UI
    [Header("Health")]
    public Image healthbar;
    [Header("Score")]
    public TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = GameManager.GM.GetEarthHealth();
        score.text = "" + GameManager.GM.Score;
    }
}
