using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteUIManager : MonoBehaviour
{
    public static NoteUIManager Instance;

    public GameObject panel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI pageText;

    private NoteData current;
    private int pageIndex;

    void Awake() => Instance = this;
    public bool IsOpen => panel.activeSelf;

    public void Show(NoteData data)
    {
        current = data;
        pageIndex = 0;
        UpdatePage();
        panel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void Hide()
    {
        panel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void Next()
    {
        if (current == null) return;
        if (pageIndex < current.pages.Count - 1)
        {
            pageIndex++;
            UpdatePage();
        }
    }

    public void Prev()
    {
        if (current == null) return;
        if (pageIndex > 0)
        {
            pageIndex--;
            UpdatePage();
        }
    }

    void UpdatePage()
    {
        if (current == null) return;
        titleText.text = current.title;
        pageText.text = current.pages[pageIndex];
    }
}

