using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{
    [SerializeField]
    public List<Sprite> resolutionSprites;
    [SerializeField]
    public Sprite currentResolutionSprite;
    public Image currentResolutionButtonImage;
    private int currentSpriteListIndex;

    public Button confirmButton;
    public Toggle toggleCheckbox;
    void Start()
    {
        //Cursor.visible = true;

        currentSpriteListIndex = 0;

        // resoultion PREF
        if (!PlayerPrefs.HasKey("Resoluton"))
        {
            PlayerPrefs.SetInt("Resolution_W", 800);
            PlayerPrefs.SetInt("Resolution_H", 600);
        }
        else
        {
            Screen.SetResolution(PlayerPrefs.GetInt("Resolution_W"), PlayerPrefs.GetInt("Resolution_H"), true);
        }

        // decreased HUD PREF

        if (!PlayerPrefs.HasKey("Decreased_HUD"))
        {
            PlayerPrefs.SetInt("Decreased_HUD", 0);
        }
        // listener do buttona confirmButton
        confirmButton.onClick.AddListener(SaveSettings);


    }

    // Update is called once per frame
    void Update()
    {
        currentResolutionSprite = resolutionSprites[currentSpriteListIndex];

        currentResolutionButtonImage.sprite = currentResolutionSprite;

    }

    public void NextResolution()
    {
        currentSpriteListIndex++;
        if(currentSpriteListIndex>=resolutionSprites.Count)
        {
            currentSpriteListIndex = 0;
        }
    }

    public void SaveSettings()
    {
        // resoultion

        string currentWidth = "";
        string currentHeight = "";
        int index = 0;
        bool height = false;

        for(index=0; ;index++)
        {
            if(!height)
            {
                if (currentResolutionSprite.name[index] != 'x')
                    currentWidth += currentResolutionSprite.name[index];
                else
                    height = true;
            }
            else
            {
                if (currentResolutionSprite.name[index] != 'r')
                    currentHeight += currentResolutionSprite.name[index];
                else
                    break;
            }
        }


        PlayerPrefs.SetInt("Resolution_W", int.Parse(currentWidth));
        PlayerPrefs.SetInt("Resolution_H", int.Parse(currentHeight));

        //

        // hud decreasing

        var hudDecreasing = toggleCheckbox.isOn;
        if(hudDecreasing)
        {
            PlayerPrefs.SetInt("Decreased_HUD", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Decreased_HUD", 0);
        }

        SceneManager.LoadScene("StartMenuScene");


        Screen.SetResolution(PlayerPrefs.GetInt("Resolution_W"), PlayerPrefs.GetInt("Resolution_H"), true);
    }
}
