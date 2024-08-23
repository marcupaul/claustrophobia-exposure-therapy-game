using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;
    public GameObject successUI;
    public GameObject promptUI;

    private IEnumerator NoKeyFound()
    {
        _prompt = "YOU ARE MISSING THE KEY.";
        yield return new WaitForSeconds(2);
        _prompt = "E - PROGRESS";
    }

    public bool Interact(PlayerController interactor)
    {
        if (interactor.getKey() == false)
        {
            StartCoroutine(NoKeyFound());
            return false;
        }
        this.gameObject.SetActive(false);
        promptUI.SetActive(false);
        interactor.setMovement(false);
        interactor.setWon(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        successUI.SetActive(true);
        return true;
    }
}
