using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // The only thing this class does, is looks at components and updates the UI
    [Header("Health")]
    public Transform healthbar;
    [Header("Score")]
    public Transform score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.localScale = new Vector3(GameManager.GM.health,0.5f,1);
    }
}
