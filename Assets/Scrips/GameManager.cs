using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{

    #region Props

    [SerializeField]

    public bool Playing = false;
    public GameObject[] Magnets;
    public PlayerController Player;
    public EnemyController Enemy;
    public SwitchController Switch;

    public GameObject Canvas;
    public GameObject PlayButton;
    public GameObject SettingsButton;
    public GameObject Settigs;
    public GameObject Camera;
    public AudioSource Music;

    public Slider Slider;

    public GameObject ScoreRender;
    public GameObject BestScoreRender;
    public int Score = 0;
    public int BestScore = 0;

    public GameObject LivesString;
    //public GameObject InputLog;

    #endregion

    private void Awake()
    {
        Slider.value = 8;
        Load();
    }

    void Start()
    {
        //Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        var livesText = LivesString.GetComponent<TextMeshProUGUI>();
        livesText.text = "  Lives: " + Player.Lives.ToString();
        Cursor.visible = false;
        Playing = false;
    }

    void Update()
    {

        if (Player.Lives < 1)
        {
            StopMusic();
            ShowCanvas();
            UpdateBestScore();
            Player.Lives = 3;
            UpdateLiveCount();
            ResetEnemyPosition();

            Save();
        } 
        if(Playing)
        {
            PlayMusic();
        }
    }

    private void ResetEnemyPosition()
    {
        Vector3 enemyPosition = new Vector3(UnityEngine.Random.Range(-2.2f, 2.2f), -6.5f, Enemy.transform.position.z);
        Vector3 switchPosition = new Vector3(UnityEngine.Random.Range(-2.2f, 2.2f), -6.5f, Enemy.transform.position.z);
        Vector3 playerPosition = new Vector3(0, 4, 3);
        Player.transform.position = playerPosition;
        Switch.transform.position = switchPosition;
        Enemy.transform.position = enemyPosition;
    }

    public void SwitchMagnets()
    {
        foreach (var magnet in Magnets)
        {
            foreach (Transform childMagnet in magnet.transform)
            {
                Vector3 switchPosition = new Vector3(-childMagnet.transform.position.x, childMagnet.transform.position.y, childMagnet.transform.position.z);
                childMagnet.transform.position = switchPosition;
            }

        }
    }

    public void CameraShake()
    {
        Camera.GetComponent<CameraShake>().shakeDuration = 0.2f;
    }

    #region Canvas

    public void HiddeCanvas()
    {
        Canvas.SetActive(false);
        Playing = true;
        UpdateScore(true);
    }

    public void ShowCanvas()
    {
        Canvas.SetActive(true);
        Playing = false;
    }

    public void ShowSettings()
    {
        PlayButton.SetActive(false);
        SettingsButton.SetActive(false);
        Settigs.SetActive(true);
    }

    public void SettingsBack()
    {
        PlayButton.SetActive(true);
        SettingsButton.SetActive(true);
        Settigs.SetActive(false);
    }

    public void UpdateLiveCount()
    {
        var livesText = LivesString.GetComponent<TextMeshProUGUI>();
        livesText.text = "  Lives: " + Player.Lives.ToString();

    }

    public void UpdateScore(bool reset)
    {
        if (reset)
        {
            Score = 0;
        }
        else
        {
            Score += 100;
        }
        var scoreText = ScoreRender.GetComponent<TextMeshProUGUI>();
        scoreText.text = "  Score: " + Score.ToString();

    }

    public void UpdateBestScore()
    {
        if(Score > BestScore)
        {
            BestScore = Score;
        }
        var scoreText = BestScoreRender.GetComponent<TextMeshProUGUI>();
        scoreText.text = "  Best score: " + BestScore.ToString();
    }

    public void SetSpeed()
    {
        Enemy.Speed = Slider.value;
    }

    #endregion

    #region Music

    public void PlayMusic()
    {
        if (!Music.isPlaying)
        {
            Music.Play();
        }
    }

    public void StopMusic()
    {
        Music.Stop();
    }

    #endregion

    #region Persistence
    public void Save()
    {
        BinaryFormatter BF = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Create);
        PlayerData data = new PlayerData() { Score = BestScore };
        BF.Serialize(file, data);
        file.Close();

    }

    public void Load ()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter BF = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)BF.Deserialize(file);
            file.Close();

            BestScore = data.Score;
            var scoreText = BestScoreRender.GetComponent<TextMeshProUGUI>();
            scoreText.text = "  Best score: " + Score.ToString();
        }
    }
    #endregion

}

[Serializable]
class PlayerData
{
    public int Score;

}