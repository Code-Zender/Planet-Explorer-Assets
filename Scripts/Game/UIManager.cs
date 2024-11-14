using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject RealsavegamePanel;
    public GameObject savegamePanel;
    public GameObject subSavegamePanel;
    public GameObject savegameButtonPrefab;
    public GameObject subSavegameButtonPrefab;
    public Button loadButton;
    public Button continueButton;
    public Button newGameButton;
    public Button settingsButton;
    public GameObject settingsPanel;

    private Savegame selectedSavegame;
    private SaveData selectedSubSave;

    private void Start()
    {
        if (gameManager == null)
        {
            Debug.LogError("GameManager ist nicht zugewiesen.");
            return;
        }

        RealsavegamePanel.SetActive(false);
        settingsButton.onClick.AddListener(OnSettingButtonClicked);
    }

    public void OnLoadButtonClicked()
    {
        Debug.Log("1");
        RealsavegamePanel.SetActive(true);
        Debug.Log("2");
        PopulateSavegameList();
        Debug.Log("3");
    }

    private void PopulateSavegameList()
    {
        List<string> excludeNames = new List<string> { "SubPanel", "Scroll View", "LoadGame" };
        foreach (Transform child in savegamePanel.transform)
        {
            if (excludeNames.Contains(child.gameObject.name))
                continue;
            Destroy(child.gameObject);
        }

        foreach (Savegame savegame in gameManager.GetAllSavegames())
        {
            GameObject buttonObject = Instantiate(savegameButtonPrefab, savegamePanel.transform);
            Button button = buttonObject.GetComponent<Button>();
            TextMeshProUGUI buttonText = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = savegame.gameName;

            button.onClick.AddListener(() => OnSavegameSelected(savegame));
        }
        UpdateContinueButtonVisibility(); // Button aktualisieren
    }

    private void OnSavegameSelected(Savegame savegame)
    {
        selectedSavegame = savegame;
        PopulateSubSavegameList();
    }

    private void PopulateSubSavegameList()
    {
        if (selectedSavegame == null || selectedSavegame.saves == null)
        {
            Debug.LogWarning("Kein Unter-Spielstand vorhanden.");
            return;
        }

        foreach (Transform child in subSavegamePanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (SaveData subSave in selectedSavegame.saves)
        {
            GameObject buttonObject = Instantiate(subSavegameButtonPrefab, subSavegamePanel.transform);
            Button button = buttonObject.GetComponent<Button>();
            TextMeshProUGUI buttonText = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = subSave.saveName + " - Punkte: " + subSave.score;

            button.onClick.AddListener(() => OnSubSaveSelected(subSave));
        }

        if (selectedSavegame.saves.Count > 0)
        {
            selectedSubSave = selectedSavegame.saves[selectedSavegame.saves.Count - 1];
        }
    }

    private void OnSubSaveSelected(SaveData subSave)
    {
        selectedSubSave = subSave;
    }

    public void OnLoadSelectedSave()
    {
        if (selectedSubSave != null)
        {
            Debug.Log("Lade Spielstand: " + selectedSubSave.saveName + " - Punkte: " + selectedSubSave.score);
            SceneManager.LoadScene(selectedSubSave.world);
        }
        else
        {
            Debug.LogWarning("Kein Unter-Spielstand ausgewählt.");
        }
    }

    public void OnNewGameButtonClicked()
    {
        SaveData newSave = new SaveData
        {
            saveName = "Spielstand_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss"),
            score = 0,
            world = "space"
        };
        gameManager.SaveGame("MeinSpiel", newSave);

        Debug.Log("Neuer Spielstand erstellt: " + newSave.saveName);
        UpdateContinueButtonVisibility();
    }

    private void UpdateContinueButtonVisibility()
    {
        continueButton.gameObject.SetActive(gameManager.HasSavegames());
    }

    public void ClosePanel()
    {
        RealsavegamePanel.SetActive(false);
    }

    public void OnSettingButtonClicked()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
