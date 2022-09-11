using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GamePolygon;

public class GameWinScene : MonoBehaviour
{

    public GameObject[] Players;
    GameObject SelectedPlayer;
    

    public void Start()
    {
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlayMusic(SoundManager.Instance.Menu);
        int PlayerPos = (PlayerPrefs.GetInt("CURRENT_CHARACTER", 0));
        Players[PlayerPos].SetActive(true);
        SelectedPlayer = Players[PlayerPos];
    }

    public void Next()
    {
        SceneManager.LoadScene("Game");

    }

    public void Menu()
    {

        SceneManager.LoadScene("Menu");
    }
    public void Shop()
    {

        SceneManager.LoadScene("Shop");
    }
}
