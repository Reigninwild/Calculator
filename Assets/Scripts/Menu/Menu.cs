using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SmartLocalization;

public class Menu : MonoBehaviour {

    LanguageManager languageManager;

    public GameObject panel;

    public Text newgameText;
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
        newgameText.text = languageManager.GetTextValue("Menu.NewGame");
        settingsText.text = languageManager.GetTextValue("Menu.Settings");
        exitText.text = languageManager.GetTextValue("Menu.Exit");
    }

    public void Show()
    {
        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        Application.LoadLevel(1);
    }
}
