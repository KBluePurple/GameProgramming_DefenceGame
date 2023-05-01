using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    private GameObject retryBtn = null;
    private GameObject mainMenuBtn = null;

    private void Awake()
    {
        Instance = this;

        transform.Find("RetryBtn").GetComponent<Button>().onClick.AddListener(() => { GameSceneManager.Load(GameSceneManager.Scene.GameScene); });
        transform.Find("MainMenuBtn").GetComponent<Button>().onClick.AddListener(() => { GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene); });
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        transform.Find("WaveSurvivedTMP").GetComponent<TextMeshProUGUI>().SetText($"You Survived {EnemyWaveManager.Instance.GetWaverNumber()}Wave!");
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
