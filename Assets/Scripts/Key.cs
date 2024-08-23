using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;

    public bool Interact(PlayerController interactor)
    {
        interactor.setKey(true);
        this.gameObject.SetActive(false);
        return true;
    }
}
