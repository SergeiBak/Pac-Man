using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Text highScoreText;

    [Header("Blinky")]
    [SerializeField]
    private GameObject blinkyIcon;
    [SerializeField]
    private Text blinkyName;
    [SerializeField]
    private Text blinkyNickname;

    [Header("Pinky")]
    [SerializeField]
    private GameObject pinkyIcon;
    [SerializeField]
    private Text pinkyName;
    [SerializeField]
    private Text pinkyNickname;

    [Header("Inky")]
    [SerializeField]
    private GameObject inkyIcon;
    [SerializeField]
    private Text inkyName;
    [SerializeField]
    private Text inkyNickname;

    [Header("Clyde")]
    [SerializeField]
    private GameObject clydeIcon;
    [SerializeField]
    private Text clydeName;
    [SerializeField]
    private Text clydeNickname;

    [Header("Pellets")]
    [SerializeField]
    private GameObject tenPts;
    [SerializeField]
    private GameObject fiftyPts;

    [Header("Chase Sequence")]
    [SerializeField]
    private GameObject powerPellet;
    [SerializeField]
    private GameObject pacman;
    [SerializeField]
    private GameObject blinky;
    [SerializeField]
    private GameObject pinky;
    [SerializeField]
    private GameObject inky;
    [SerializeField]
    private GameObject clyde;

    [Header("Timing Variables")]
    [SerializeField]
    private float ghostIconDelay;
    [SerializeField]
    private float ghostNameDelay;

    [Space(10)]
    [SerializeField]
    private Text anyKeyText;

    private void Start()
    {
        SetupStats();
        ClearUI();
        StartCoroutine(LoadUI());
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            StartGame();
        }
    }

    private IEnumerator LoadUI() // loads UI in piece by piece with delays like in original
    {
        yield return new WaitForSeconds(1f); // initial delay



        yield return new WaitForSeconds(ghostNameDelay);

        blinkyIcon.SetActive(true);

        yield return new WaitForSeconds(ghostIconDelay);

        blinkyName.enabled = true;

        yield return new WaitForSeconds(ghostNameDelay);

        blinkyNickname.enabled = true;

        yield return new WaitForSeconds(ghostNameDelay);



        pinkyIcon.SetActive(true);

        yield return new WaitForSeconds(ghostIconDelay);

        pinkyName.enabled = true;

        yield return new WaitForSeconds(ghostNameDelay);

        pinkyNickname.enabled = true;

        yield return new WaitForSeconds(ghostNameDelay);



        inkyIcon.SetActive(true);

        yield return new WaitForSeconds(ghostIconDelay);

        inkyName.enabled = true;

        yield return new WaitForSeconds(ghostNameDelay);

        inkyNickname.enabled = true;

        yield return new WaitForSeconds(ghostNameDelay);



        clydeIcon.SetActive(true);

        yield return new WaitForSeconds(ghostIconDelay);

        clydeName.enabled = true;

        yield return new WaitForSeconds(ghostNameDelay);

        clydeNickname.enabled = true;

        yield return new WaitForSeconds(ghostIconDelay);

        tenPts.SetActive(true);
        fiftyPts.SetActive(true);
    }

    private void ClearUI() // hides all of the UI that is going to be revealed
    {
        blinkyIcon.SetActive(false);
        blinkyName.enabled = false;
        blinkyNickname.enabled = false;

        pinkyIcon.SetActive(false);
        pinkyName.enabled = false;
        pinkyNickname.enabled = false;

        inkyIcon.SetActive(false);
        inkyName.enabled = false;
        inkyNickname.enabled = false;

        clydeIcon.SetActive(false);
        clydeName.enabled = false;
        clydeNickname.enabled = false;

        tenPts.SetActive(false);
        fiftyPts.SetActive(false);
    }

    private void StartGame() // loads game scene
    {
        SceneManager.LoadScene("GameScene");
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
