using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasButtons : MonoBehaviour
{
    public Sprite music_on, music_off;

    private void Start()
    {
        if (PlayerPrefs.GetString("music") == "No" && gameObject.name == "Music")
            GetComponent<Image>().sprite = music_off;
    }

    public void RestartGame()
    {
        if (PlayerPrefs.GetString("music") != "No")
            GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadShop()
    {
        if (PlayerPrefs.GetString("music") != "No")
            GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Shop");
    }

    public void CloseShop()
    {
        if (PlayerPrefs.GetString("music") != "No")
            GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Main");
    }

    public void LoadInstagram()
    {
        if (PlayerPrefs.GetString("music") != "No")
            GetComponent<AudioSource>().Play();
        Application.OpenURL("https://rossgram.ru/");
    }

    public void MusicWork()
    {
        if(PlayerPrefs.GetString("music") == "No")
        {
            PlayerPrefs.SetString("music", "Yes");
            GetComponent<AudioSource>().Play();
            GetComponent<Image>().sprite = music_on;
        }
        else
        {
            PlayerPrefs.SetString("music", "No");
            GetComponent<Image>().sprite = music_off;
        }
    }
}
