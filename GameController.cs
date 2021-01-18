using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private Fader Fader;
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(StartRoutine());
    }

    public void LoseGame()
    {
        StartCoroutine(LoseRoutine());
    }

    private IEnumerator StartRoutine()
    {
        yield return StartCoroutine(Fader.Fade(false));
        Fader.gameObject.SetActive(false);
        Time.timeScale = 1;

    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }
    private IEnumerator LoseRoutine()
    {
        Time.timeScale = 0f;
        Fader.gameObject.SetActive(true);
        yield return StartCoroutine(Fader.Fade(true));
        StartCoroutine(Fader.StartBlinkRetryBtn());
    }
}
