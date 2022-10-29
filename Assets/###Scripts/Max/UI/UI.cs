using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private StartMenu _mainMenu;
    [SerializeField] private PlayMenu _playMenu;
    [SerializeField] private SettingsMenu _settingsMenu;
    [SerializeField] private EndLevelMenu _endLevelMenu;
    [SerializeField] private FailMenu _failMenu;

    public StartMenu MainMenu => _mainMenu;
    public PlayMenu PlayMenu => _playMenu;
    public SettingsMenu SettingsMenu => _settingsMenu;
    public EndLevelMenu EndLevelMenu => _endLevelMenu;
    public FailMenu FailMenu => _failMenu;
}
