using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour
{
    public ImageTimer harvestTimer;
    public ImageTimer foodTimer;
    public ImageTimer raidTimer;
    public Image enemyTimer;

    public AudioSource audioSource;
    public AudioSource music;
    public AudioSource raidAudioSource;
    public AudioListener audioListener;
    public AudioClip defeatedMusic;
    public AudioClip winMusic;
    public AudioClip raidSnd;

    public Image peasantCooldownTimer;
    public Image warriorCooldownTimer;

    public Button peasantBtn;
    public Button warriorBtn;

    public GameObject gameOverScreen;
    public GameObject congratulationScreen;
    public GameObject pauseScreen;
    public GameObject pauseBtn;
    public GameObject startScreen;

    public float peasantCooldown;
    public float peasantCooldownMax;
    public float warriorCooldown;
    public float warriorCooldownMax;
    public float wheatPerCycle;

    public int peasantCount;
    public int warriorCount;
    public int wheatCount;
    public int wheatWarriors;
    public int wheatPeasants;
    public int peasantCost;
    public int warriorCost;

    public int totalWheat;
    public int totalRaids;

    public int raidCycle;
    public int raiders;
    public int firstRaid;
    public int raidStart;

    public bool timeScale = true;
    public bool soundState = true;

    public Text resources;
    public Text nextRaid;
    public Text gameOverText;
    public Text winText;
    // Start is called before the first frame update
    void Start()
    {
        peasantCooldownTimer = peasantCooldownTimer.GetComponent<Image>();
        warriorCooldownTimer = warriorCooldownTimer.GetComponent<Image>();
        music = music.GetComponent<AudioSource>();
        raidAudioSource = raidAudioSource.GetComponent<AudioSource>();
        raidCycle = 1;
        raiders = 1;
        raidStart = firstRaid;
        Time.timeScale = 0;
        timeScale = false;
        pauseBtn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (totalRaids >= 15 && wheatCount >= 500)
        {
            Congratulation();
        }
        if (harvestTimer.tick)
        {
            wheatCount += peasantCount * wheatPeasants;
            totalWheat += peasantCount * wheatPeasants;
        }
        if (foodTimer.tick)
        {
            if (wheatCount <= 0)
            {
                peasantCount -= 1;
            }
            wheatCount -= warriorCount * wheatWarriors;
        }
        if (raidTimer.tick)
        {
            if (raidCycle == raidStart)
            {
                raidAudioSource.clip = raidSnd;
            }
            if (raidCycle <= raidStart)
            {
                raidCycle += 1;
                firstRaid -= 1;
            }
            else if (warriorCount >= raiders)
            {
                warriorCount -= raiders;
                raidCycle += 1;
                if (raiders <= 31)
                {
                    raiders *= 2;
                }
                totalRaids += 1;
            }
            else
            {
                GameOver();
            }
        }

        TextUpdate();

        peasantCooldown -= Time.deltaTime;
        warriorCooldown -= Time.deltaTime;

        if (wheatCount <= peasantCost || peasantCooldown > 0)
        {
            peasantBtn.interactable = false;
        }
        else
        {
            peasantBtn.interactable = true;
        }
        if (wheatCount <= warriorCost || warriorCooldown > 0)
        {
            warriorBtn.interactable = false;
        }
        else
        {
            warriorBtn.interactable = true;
        }
        peasantCooldownTimer.fillAmount = peasantCooldown / peasantCooldownMax;
        warriorCooldownTimer.fillAmount = warriorCooldown / warriorCooldownMax;
    }
    public void PeasantAdd()
    {
        peasantCooldown = peasantCooldownMax;
        peasantCount += 1;
        wheatCount -= peasantCost;
        audioSource.Play();
    }
    public void WarriorAdd()
    {
        warriorCooldown = warriorCooldownMax;
        warriorCount += 1;
        wheatCount -= warriorCost;
        audioSource.Play();
    }
    public void TextUpdate()
    {
        wheatPerCycle = peasantCount * wheatPeasants - warriorCount * wheatWarriors;
        resources.text = peasantCount + "\n" + warriorCount + "\n\n" + wheatCount + "\n\n" + (warriorCount * wheatWarriors) + "\n" + (peasantCount * wheatPeasants);
        nextRaid.text = raidCycle + "\n" + raiders + "\n" + firstRaid;
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        pauseBtn.SetActive(false);
        Time.timeScale = 0;
        timeScale = false;
        gameOverText.text = "За все время вы собрали" + totalWheat + "пшеницы и пережили" + totalRaids + "рейдов";
        music.clip = defeatedMusic;
    }
    public void Congratulation()
    {
        congratulationScreen.SetActive(true);
        pauseBtn.SetActive(false);
        Time.timeScale = 0;
        timeScale = false;
        winText.text = "За все время вы собрали" + totalWheat + "пшеницы и пережили" + totalRaids + "рейдов";
        music.clip = winMusic;
    }
    public void MuteAll()
    {
        audioSource.Play();
        if (soundState == true)
        {
            audioListener.enabled = false;
            soundState = false;
        }
        else if (soundState == false)
        {
            audioListener.enabled = true;
            soundState = true;
        }
    }
    public void Pause()
    {
        audioSource.Play();
        if (timeScale == true)
        {
            Time.timeScale = 0;
            timeScale = false;
            pauseScreen.SetActive(true);
        }
        else if (timeScale == false) 
        {
            Time.timeScale = 1;
            timeScale = true;
            pauseScreen.SetActive(false);
        }
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        timeScale = true;
        startScreen.SetActive(false);
        pauseBtn.SetActive(true);
    }
}
