using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _startCanvas;
    [SerializeField] private GameObject _controlMenu;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _controlButton;


    private void OnEnable()
    {
        _controlButton.onClick.AddListener(OnControlPressed);
        _backButton.onClick.AddListener(OnBackPressed);
    }

    void OnControlPressed()
    {
        _mainMenu.SetActive(false);
        _startCanvas.SetActive(false);
        _controlMenu.SetActive(true);
    }
    
    void OnBackPressed()
    {
        _mainMenu.SetActive(true);
        _startCanvas.SetActive(true);
        _controlMenu.SetActive(false);
    }
}
