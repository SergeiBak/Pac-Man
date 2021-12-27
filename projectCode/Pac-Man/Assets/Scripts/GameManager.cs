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
    [SerializeField]
    private Image[] fruitIcons;

    [Header("Fruit Icons")]
    [SerializeField]
    private Sprite empty;
    [SerializeField]
    private Sprite cherry;
    [SerializeField]
    private Sprite strawberry;
    [SerializeField]
    private Sprite orange;
    [SerializeField]
    private Sprite apple;
    [SerializeField]
    private Sprite melon;
    [SerializeField]
    private Sprite galaxianStarship;
    [SerializeField]
    private Sprite bell;
    [SerializeField]
    private Sprite key;

    [Header("Fruit Pickups")]
    [SerializeField]
    private GameObject cherryPickup;
    [SerializeField]
    private GameObject strawberryPickup;
    [SerializeField]
    private GameObject orangePickup;
    [SerializeField]
    private GameObject applePickup;
    [SerializeField]
    private GameObject melonPickup;
    [SerializeField]
    private GameObject galaxianStarshipPickup;
    [SerializeField]
    private GameObject bellPickup;
    [SerializeField]
    private GameObject keyPickup;

    [Space(10)]
    [SerializeField]
    private Transform fruitSpawnLocation;

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }
    public int level { get; private set; }

    [SerializeField]
    private int bonusLifeScore = 10000;
    private bool bonusLifeAwarded = false;

    [SerializeField]
    private int pelletsForFruit = 70;
    private bool fruitSpawned = false;

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
        fruitSpawned = false;

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

        if (!bonusLifeAwarded && score >= bonusLifeScore) // If player reaches 10000, they get a bonus life
        {
            bonusLifeAwarded = true;
            SetLives(lives + 1);
        }

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

        UpdateFruits();
    }

    private void UpdateFruits()
    {
        switch (level)
        {
            case 0:
                EmptyFruits();
                break;
            case 1:
                PlaceFruit(0, cherry);
                break;
            case 2:
                PlaceFruit(1, strawberry);
                break;
            case 3:
                PlaceFruit(2, orange);
                break;
            case 4:
                PlaceFruit(3, orange);
                break;
            case 5:
                PlaceFruit(4, apple);
                break;
            case 6:
                PlaceFruit(5, apple);
                break;
            case 7:
                PlaceFruit(6, melon);
                break;
            case 8:
                PushFruit(melon);
                break;
            case 9:
            case 10:
                PushFruit(galaxianStarship);
                break;
            case 11:
            case 12:
                PushFruit(bell);
                break;
            default:
                PushFruit(key);
                break;
        }
    }

    private void PlaceFruit(int index, Sprite fruit) // if no overflow, simply place the fruit, otherwise PushFruit() method is used
    {
        fruitIcons[index].sprite = fruit;
    }

    private void PushFruit(Sprite fruit) // goes through and shifts all of the fruits by one, then pushes in the new fruit being added
    {
        for (int i = 0; i < fruitIcons.Length; i++)
        {
            if (i == (fruitIcons.Length - 1))
            {
                fruitIcons[i].sprite = fruit;
            }
            else
            {
                fruitIcons[i].sprite = fruitIcons[i+1].sprite;
            }
        }
    }

    private void EmptyFruits() // set all fruit icons to be empty
    {
        foreach (Image fruit in fruitIcons)
        {
            fruit.sprite = empty;
        }
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
        SetScore(score + (ghost.GetPoints() * (int)(Mathf.Pow(2, (ghostMultiplier - 1)))));
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
        else if (HalfPelletsEaten() && !fruitSpawned) // Checks to see if we need to spawn a fruit
        {
            fruitSpawned = true;
            SpawnFruit();
        }
    }

    private void SpawnFruit() // spawns a fruit based on the current level
    {
        switch (level)
        {
            case 0:
                Debug.LogError("Error: GameManger-->SpawnFruit-->Case 0");
                break;
            case 1:
                InstantiateFruit(cherryPickup);
                break;
            case 2:
                InstantiateFruit(strawberryPickup);
                break;
            case 3:
            case 4:
                InstantiateFruit(orangePickup);
                break;
            case 5:
            case 6:
                InstantiateFruit(applePickup);
                break;
            case 7:
            case 8:
                InstantiateFruit(melonPickup);
                break;
            case 9:
            case 10:
                InstantiateFruit(galaxianStarshipPickup);
                break;
            case 11:
            case 12:
                InstantiateFruit(bellPickup);
                break;
            default:
                InstantiateFruit(keyPickup);
                break;
        }
    }

    private void InstantiateFruit(GameObject fruit) // spawns fruit at fruitSpawnLocation
    {
        Instantiate(fruit, fruitSpawnLocation.position, Quaternion.identity);
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

    public void FruitEaten(Fruit fruit)
    {
        fruit.gameObject.SetActive(false);

        SetScore(score + fruit.points); // increment score
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

    private bool HalfPelletsEaten()
    {
        int pelletsEaten = 0;

        foreach (Transform pellet in pellets)
        {
            if (!pellet.gameObject.activeSelf)
            {
                pelletsEaten++;
            }
        }

        return (pelletsEaten >= pelletsForFruit); // check if required pellets for fruit spawn are eaten
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
