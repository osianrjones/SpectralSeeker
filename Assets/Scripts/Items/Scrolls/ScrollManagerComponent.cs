using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScrollManagerComponent : MonoBehaviour, IScroll
{
    [SerializeField] GameObject notePanel;
    [SerializeField] InputField inputField;
    [SerializeField] private string fileName;
    [SerializeField] private float closeTime = 2f;
    [SerializeField] private AudioClip pickupSound;
    private string basePath = "C:/Users/osian/Documents/GitHub/SpectralSeeker/Assets/StreamingAssets";

    private void Start()
    {
        notePanel.SetActive(false);
        gameObject.GetComponent<ItemPickupComponent>().ScrollPickedUp += ShowNote;
    }

    public void ShowNote()
    {
        SoundManager.Instance.PlaySFX(pickupSound);
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string note = File.ReadAllText(filePath);
            inputField.text = note;
        }
        else
        {
            inputField.text = "This is not the note you are looking for...";
        }
        inputField.interactable = false;
        
        notePanel.SetActive(true);
        StartCoroutine(CloseNote());
    }

    public IEnumerator CloseNote()
    {
        yield return new WaitForSeconds(closeTime);
        notePanel.SetActive(false);
        gameObject.gameObject.SetActive(false);
        Debug.Log("DEACTIVATED");
    }
}
