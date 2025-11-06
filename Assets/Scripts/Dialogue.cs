using UnityEngine;
using System.Collections.Generic;

// Permite criar um novo asset de diálogo no menu 'Create' do Unity.
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues/Dialogue")]
public class Dialogue : ScriptableObject
{
    // A lista principal de todas as linhas de diálogo para este módulo/cena
    public List<DialogueLine> dialogueLines;
}
