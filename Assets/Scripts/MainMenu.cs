using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Canvas settingsMenu;
    [SerializeField] private Canvas mainMenu;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSounds;
    [SerializeField] private AudioSource buttonSound;

    void Start()
    {
        sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sliderSounds.value = PlayerPrefs.GetFloat("SoundsVolume", 1f);
    }

    public void Play()
    {
        PlayerPrefs.SetInt("Load", 0);
        SceneManager.LoadScene("Game");
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("Load", 1);
        SceneManager.LoadScene("Game");
    }

    public void ButtonSound()
    {
        buttonSound.Play();
    }

    public void MusicVol(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SoundsVol(float sliderValue)
    {
        mixer.SetFloat("SoundsVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SoundsVolume", sliderValue);
    }

    public void Settings()
    {
        mainMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        settingsMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
