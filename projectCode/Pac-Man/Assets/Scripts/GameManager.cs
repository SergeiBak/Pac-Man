using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Ghost[] ghosts;
    [SerializeField]
    private Pacman pacman;
    [SerializeField]
    private Transform pellets;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text highScoreText;
    [SerializeField]
    private Text levelText;

    [SerializeField]
    private Image[] lifeIcons;

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }
    public int level { get; private set; }

    private void Start()
    {
        SetupStats();

        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLevel(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound() // Set pacman, ghosts, and pellets to be active
    {
        foreach (Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        SetLevel(level + 1);

        ResetState();
    }

    private void ResetState() // resets ghosts + pacman to true
    {
        ResetGhostMultiplier();

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }

        pacman.ResetState();
    }

    private void GameOver() // sets ghosts + pacman to false
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;

        if (score > PlayerPrefs.GetInt("PacmanHighScore"))
        {
            PlayerPrefs.SetInt("PacmanHighScore", score);
            SetHighScoreText();
        }

        scoreText.text = score.ToString();
    }

    private void SetLevel(int level)
    {
        this.level = level;

        if (level > PlayerPrefs.GetInt("PacmanHighLevel"))
        {
            PlayerPrefs.SetInt("PacmanHighLevel", level);
            SetHighScoreText();
        }

        levelText.text = level.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;

        DisableAllIcons();

        int displayedLives = lives - 1;

        for (int i = 0; i < displayedLives; i++)
        {
            lifeIcons[i].enabled = true;
        }
    }

    private void DisableAllIcons()
    {
        foreach (Image lifeIcon in lifeIcons)
        {
            lifeIcon.enabled = false;
        }
    }

    public void GhostEaten(Ghost ghost) // adds points for killing ghost
    {
        SetScore(score + (ghost.GetPoints() * ghostMultiplier));
        ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        pacman.gameObject.SetActive(false);

        SetLives(lives - 1); // decrement lives

        if (lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false); // hide pellet from map

        SetScore(score + pellet.points); // increment score

        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet) // set all ghosts to frightened state 
    {
        for (int i = 0; i < ghosts.Length; i++) // loop through & set each ghost to be frightened
        {
            ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration); // start power state countdown
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }

    private void SetupStats()
    {
        if (!PlayerPrefs.HasKey("PacmanHighScore"))
        {
            PlayerPrefs.SetInt("PacmanHighScore", 0);
        }
        if (!PlayerPrefs.HasKey("PacmanHighLevel"))
        {
            PlayerPrefs.SetInt("PacmanHighLevel", 0);
        }

        SetHighScoreText();
    }

    private void SetHighScoreText()
    {
        highScoreText.text = PlayerPrefs.GetInt("PacmanHighScore").ToString() + " L" + PlayerPrefs.GetInt("PacmanHighLevel").ToString();
    }
}
