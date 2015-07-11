using UnityEngine;
using System.Collections;
using SmartLocalization;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    LanguageManager languageManager;

    public GameObject panel;

    public GameObject pauseButton;
    public Text resumeText;
    public Text settingsText;
    public Text exitText;

	// Use this for initialization
	void Start () {
        languageManager = LanguageManager.Instance;
        languageManager.OnChangeLanguage += OnLanguageChanged;

        SmartCultureInfo systemLanguage = languageManager.GetSupportedSystemLanguage();
        if (systemLanguage != null)
        {
            languageManager.ChangeLanguage(systemLanguage);
        }
	}

    private void OnDestroy()
    {
        if (LanguageManager.HasInstance)
        {
            LanguageManager.Instance.OnChangeLanguage -= OnLanguageChanged;
        }
    }

    private void OnLanguageChanged(LanguageManager lm)
    {
        resumeText.text = languageManager.GetTextValue("Menu.NewGame");
        settingsText.text = languageManager.GetTextValue("Menu.Settings");
        exitText.text = languageManager.GetTextValue("Menu.Exit");
    }

    public void Show()
    {
        panel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    public void Exit()
    {
        Application.LoadLevel("Menu");
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Close();
        pauseButton.SetActive(true);
    }

    public void OnPause()
    {
        Time.timeScale = 0;
        Show();
    }
}
