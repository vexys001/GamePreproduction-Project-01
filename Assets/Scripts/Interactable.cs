using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;

    public void ShowUI()
    {
        _canvas.SetActive(true);
    }
    
    public void HideUI()
    {
        _canvas.SetActive(false);
    }
}
