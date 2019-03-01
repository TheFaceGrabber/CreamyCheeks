using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreamyCheaks.Input;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    private GameObject InteractText;
    private GameObject StatsPanel;
    private Image Healthbar;
    private Image Sanitybar;
    private Text HealthText;
    private Text SanityText;
    public Sprite KeyboardImage;
    public Sprite GamepadImage;
    private float barstartsize;
    private Image InteractButton;
    private Text InteractTextOption;
    private GameObject Menu;
    public Sprite ToggleOn;
    public Sprite ToggleOff;
    private Image MusicToggle;
    private Image SfxToggle;
    public Text[] OptionsText;
    private SfxPlayer Sfx;
    private MusicPlayer Music;
    private int CurrentSelection;
    private InventorySystem inventory;
    public Image Fader;


    private void Awake()
    {
        Fader.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1);
        Color newcolor = Fader.color;
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.05f);
            newcolor.a -= 0.05f;
            Fader.color = newcolor;
        }

        yield return new WaitForSeconds(0);
        Fader.gameObject.SetActive(false);
    }

    IEnumerator FadeOut(int Nextscene)
    {
        Fader.gameObject.SetActive(true);
        Color newcolor = Fader.color;
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.05f);
            newcolor.a += 0.05f;
            Fader.color = newcolor;
        }

        yield return new WaitForSeconds(0);
        SceneManager.LoadScene(Nextscene);
    }
    // Use this for initialization
    void Start () {
        OptionsText = new Text[4];
        Menu = transform.GetChild(6).gameObject;
        InteractText = transform.GetChild(3).gameObject;
        InteractTextOption = InteractText.transform.GetChild(1).GetComponent<Text>();
        StatsPanel = transform.GetChild(1).gameObject;
        Healthbar = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        HealthText = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
        Sanitybar = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>();
        SanityText = transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>();
        barstartsize = Healthbar.transform.localScale.x;
        InteractButton = InteractText.transform.GetChild(2).GetComponent<Image>();
        HideInteractText();
        MusicToggle = Menu.transform.GetChild(5).GetComponent<Image>();
        SfxToggle = Menu.transform.GetChild(6).GetComponent<Image>();
        inventory = transform.GetChild(4).GetChild(0).GetComponent<InventorySystem>();
        for (int i = 0; i < 4; i++)
        {
            OptionsText[i] = Menu.transform.GetChild(i).GetComponent<Text>();
        }
        Menu.SetActive(false);
        Music = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
        Sfx = GameObject.Find("SfxPlayer").GetComponent<SfxPlayer>();
	}
    public void UpdateInteractText(string newtext)
    {
        InteractButton.sprite = InputManager.GetLastInputType() == InputType.Gamepad ? GamepadImage : KeyboardImage;
        InteractTextOption.text = newtext;

    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StatsPanel.SetActive(!StatsPanel.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            ToggleMenu();
        }

        if (Menu.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.F)) //to update to new input manager
            {
                SelectionMade();
            }

            if (Input.GetKeyUp(KeyCode.UpArrow) || (Input.GetAxis("DPad Y") > 0))
            {
                SelectUp();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetAxis("DPad Y") < 0))
            {
                SelectDown();
            }
        }



    }

    public void ShowInteractText()
    {
        InteractText.SetActive(true);
    }

    public void HideInteractText()
    {
        InteractText.SetActive(false);
    }


    public void ShowItemDescription()
    {

    }

    public void HideItemDescription()
    {

    }

    public void ShowStats()
    {

    }

    public void HideStats()
    {

    }

    public void SetStats(int strength, int agility, int intelligence, int willpower, int perception, int charisma)
    {
        int[] values = new int[6];
        values[0] = strength;
        values[1] = agility;
        values[2] = intelligence;
        values[3] = willpower;
        values[4] = perception;
        values[5] = charisma;
        for (int i = 0; i < 6; i++)
        {
            for (int x = 0; x < values [i]; x++)
            {
                transform.GetChild(1).GetChild(i).GetChild(x).GetComponent<Image>().color = Color.yellow;
            }
        }
    }

    public void UpdateHealth(int newhealth)
    {
        HealthText.text = "Health: " + newhealth + "/10";
        Healthbar.transform.localScale = new Vector2(((float)newhealth / 10) * barstartsize, transform.localScale.y);
    }

    public void UpdateSanity(int newsanity)
    {
        SanityText.text = "Sanity: " + newsanity + "/10";
        Sanitybar.transform.localScale = new Vector2(((float)newsanity / 10) * barstartsize, transform.localScale.y);
        
    }



    private void ToggleMenu()
    {
        Menu.SetActive(!Menu.activeInHierarchy);
        inventory.MenuOpen = Menu.activeInHierarchy;
    }


    private void ToggleMusic()
    {
        Music.ToggleMusic();
        if (MusicToggle.sprite == ToggleOn) MusicToggle.sprite = ToggleOff;
        else MusicToggle.sprite = ToggleOn;
    }

    private void ToggleSfx()
    {
        Sfx.ToggleSfx();
        if (SfxToggle.sprite == ToggleOn) SfxToggle.sprite = ToggleOff;
        else SfxToggle.sprite = ToggleOn;
    }

    private void SelectDown()
    {
        OptionsText[CurrentSelection].color = Color.black;
        CurrentSelection++;
        if (CurrentSelection > 3) CurrentSelection = 0;
        OptionsText[CurrentSelection].color = Color.red;
    }

    private void SelectUp()
    {
        OptionsText[CurrentSelection].color = Color.black;
        CurrentSelection--;
        if (CurrentSelection < 0) CurrentSelection = 3;
        OptionsText[CurrentSelection].color = Color.red;
    }

    private void SelectionMade()
    {
        switch (CurrentSelection)
        {
            case (0): //Toggle Music
                ToggleMusic();
                break;
            case (1): //toggle Sfx
                ToggleSfx();
                break;
            case (2): //load menu
                StartCoroutine(FadeOut(0));
                break;
            case (3): //reload level
                StartCoroutine(FadeOut(1));
                break;
        };
    }

    public void EndGame()
    {
        StartCoroutine(FadeOut(0));
    }


    
}
