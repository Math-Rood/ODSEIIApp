using UnityEngine;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;

[System.Serializable]
public class DialogueLine
{
    // O nome do personagem que está falando
    public string characterName; 
    
    // O texto que será falado
    [TextArea(3, 10)]
    public string dialogueText;

    // O sprite do rosto do personagem (para reações)
    public Sprite eyesSprite;
    public Sprite mouthSprite; 
    
    // Lista de escolhas. Se estiver vazia, é uma linha de diálogo normal.
    public List<Choice> choices;
}

[System.Serializable]
public class Choice
{
    // O texto da opção que o usuário verá
    public string choiceText; 
    
    // A pontuação de Moral que o usuário ganha ou perde
    public int moralPointsChange; 
    
    /* O índice da próxima DialogueLine na lista do ScriptableObject a ser carregada após a escolha.
    Ex: Se for 5, a próxima linha carregada será a linha no índice 5.
    */
    public int nextLineIndex;
}