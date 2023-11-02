using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance {  get; private set; }
    [SerializeField] private Text scoreText;
    private int scoreCount = 0;

    private void Awake()
    {
        if (instance == null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }

    public void UpdateScore(int points)
    {
        scoreCount += points;
        scoreText.text = "Score: " + scoreCount.ToString() + " (+" + points.ToString() + ")";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
