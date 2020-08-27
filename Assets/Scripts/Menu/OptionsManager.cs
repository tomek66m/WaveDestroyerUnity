using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public int width, height;

    public Button ResolutionButton;

    public List<Button> ButtonsList;

    public static int CurrentResolution = 0;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Resoluton"))
        {
            PlayerPrefs.SetInt("Resolution_W", 800);
            PlayerPrefs.SetInt("Resolution_H", 600);
        }

    }
    // Start is called before the first frame update
    public void ChangeResolution()
    {
        PlayerPrefs.SetInt("Resolution_W", width);
        PlayerPrefs.SetInt("Resolution_H", height);
    }

    public void NextResolution()
    {
        Debug.Log("X");
        bool set = false;
        for(int i=0;i<ButtonsList.Count;i++)
        {
            if(set)
            {
                ButtonsList[i].enabled = true;
                break;
            }
            if (ButtonsList[i].IsActive())
            {
                ButtonsList[i].enabled = false;
                set = true;
                if (i == ButtonsList.Count - 1)
                    ButtonsList[0].enabled = true;
            }
        }
    }

}
