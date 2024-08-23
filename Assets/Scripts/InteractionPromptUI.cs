using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    public Camera playerCamera;
    [SerializeField] private GameObject _UIPanel;
    [SerializeField] private TextMeshProUGUI _promptText;

    public bool isDisplayed = false;

    private void Start()
    {
        _UIPanel.SetActive(false);
    }
    private void LateUpdate()
    {
        var rotation = playerCamera.transform.rotation;
        transform.LookAt(transform.position +  rotation * Vector3.forward, rotation * Vector3.up);
        
    }

    public string getText()
    {
        return _promptText.text;
    }

    public void SetUp(string promptText)
    {
        _promptText.text = promptText;
        _UIPanel.SetActive(true);
        isDisplayed = true;
    }

    public void Close()
    {
        _UIPanel.SetActive(false);
        isDisplayed = false;
    }
}
