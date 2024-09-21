using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Music")]
    public AudioSource music;
    [SerializeField] private bool startPlaying;

    [Header("References")]
    [SerializeField] private InputScroller inputScroller;
    public static GameManager instance;

    [Header("Score")]
    [SerializeField] private int currentScore;
    [SerializeField] private int scorePerNote = 100;
    [SerializeField] private int scorePerGoodNote = 125;
    [SerializeField] private int scorePerPerfectNote = 150;
    [SerializeField] private Text scoreText;

    [Header("Multiplier")]
    [SerializeField] private int currentMultiplier;
    [SerializeField] private int multiplierTracker;
    [SerializeField] private int[] multiplierTreshholds;
    [SerializeField] private Text multText;

    [Header("Note Checks")]
    [SerializeField] private float totalNotes;
    [SerializeField] private float normalHits;
    [SerializeField] private float goodHits;
    [SerializeField] private float perfectHits;
    [SerializeField] private float missedHits;

    [Header("End Game Screen")]
    [SerializeField] private GameObject resultsScreen;
    [SerializeField] private Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";

        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<InputObject>().Length;

    }

    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                inputScroller.hasStarted = true;

                music.Play();
            }

        } else {

            if (!music.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);

                normalsText.text = normalHits.ToString();
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = missedHits.ToString();

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";
             }
        }
    }

    public void NoteHit()
    {
        if (currentMultiplier - 1 < multiplierTreshholds.Length)
        {
            multiplierTracker++;

            if (multiplierTreshholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }

            multText.text = "Multiplier: x" + currentMultiplier;

            currentScore += scorePerNote * currentMultiplier;
            scoreText.text = "Score: " + currentScore;
        }
    }

    #region ALL HIT TYPES

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        perfectHits++;
    }

    public void NoteMissed()
    {
        Debug.Log("! MISSED !");

        currentMultiplier = 1;
        multiplierTracker = 0;

        multText.text = "Multiplier: x" + currentMultiplier;

        missedHits++;
    }

    #endregion

}
