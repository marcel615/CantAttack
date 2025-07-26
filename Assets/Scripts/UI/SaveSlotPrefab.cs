using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotPrefab : MonoBehaviour
{
    //내 컴포넌트
    [SerializeField] private Button slotButton;

    //내 자식 오브젝트
    [SerializeField] private GameObject SaveFileName;
    [SerializeField] private GameObject isSaved;

    //슬롯별 정보
    private int slotIndex;
    private string saveFilePath;

    public void Init(int index, string filePath)
    {
        slotIndex = index;
        saveFilePath = filePath;
        SetSaveSlot();
        slotButton.onClick.AddListener(OnSlotClicked);
    }
    
    void SetSaveSlot()
    {
        SaveFileName.GetComponent<TextMeshProUGUI>().text = $"Save File {slotIndex}";
        if (File.Exists(saveFilePath))
        {
            isSaved.GetComponent<TextMeshProUGUI>().text = "Yes";
        }
        else
        {
            isSaved.GetComponent<TextMeshProUGUI>().text = "No";
        }
    }
    public void OnSlotClicked()
    {
        SystemEvents.InvokeDataLoadStart(slotIndex);
    }
    public void DeleteSaveFile()
    {

    }





}
