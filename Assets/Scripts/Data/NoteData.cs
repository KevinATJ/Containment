using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Note Data")]
public class NoteData : ScriptableObject
{
    public string noteId = "NOTE_ID_UNICO";
    public string title = "Título de la Nota";
    [TextArea(3, 10)] public List<string> pages = new List<string>() { "Página 1" };
}

