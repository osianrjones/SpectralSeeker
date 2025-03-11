using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScrollManagerComponent : MonoBehaviour
{
    [SerializeField] GameObject notePanel;
    [SerializeField] InputField inputField;
    [SerializeField] private string fileName;
    [SerializeField] private float closeTime = 2f;
    private string basePath = "C:/Users/osian/Documents/GitHub/SpectralSeeker/Assets/StreamingAssets";

    private void Start()
    {
        notePanel.SetActive(false);
        gameObject.GetComponent<ItemPickupComponent>().ScrollPickedUp += ShowNote;
    }

    public void ShowNote()
    {
        string path = "Assets/StreamingAssets/" + fileName;
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path); 
        Debug.Log(reader.ReadToEnd());
        reader.Close();

        inputField.interactable = false;
        
        notePanel.SetActive(true);
        StartCoroutine(CloseNote());
    }

    IEnumerator CloseNote()
    {
        yield return new WaitForSeconds(closeTime);
        notePanel.SetActive(false);
    }
}
