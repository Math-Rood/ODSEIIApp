using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Referências de UI")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    // --- ALTERAÇÃO AQUI: REFERÊNCIAS APENAS PARA OLHOS E BOCA ---
    // Imagem para os olhos (altera as reações)
    public Image characterEyes;
    
    // Imagem para a boca (altera as reações/falas)
    public Image characterMouth;
    // --- FIM DA ALTERAÇÃO ---
    public Button nextButton;
    public GameObject choicesPanel;
    public Button choiceButtonPrefab; // Prefab/Modelo do botão de escolha

    [Header("Data & Gerenciadores")]
    public Dialogue currentDialogue;
    public ScoreManager scoreManager; 

    private int currentLineIndex = 0;

    void Start()
    {
        // Inicializa o estado da UI
        choicesPanel.SetActive(false); 
        nextButton.onClick.AddListener(NextLine); 
        
        // Inicia o diálogo com o ScriptableObject que você anexar no Inspector
        if (currentDialogue != null)
        {
            StartDialogue(currentDialogue);
        }
        else
        {
            Debug.LogError("Nenhum ScriptableObject 'Dialogue' anexado ao DialogueManager.");
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        DisplayLine(currentDialogue.dialogueLines[currentLineIndex]);
    }

    public void NextLine()
    {
        // Se a linha atual tem escolhas, o botão 'Next' está desativado. 
        // Não avance até que uma escolha seja feita.
        if (currentDialogue.dialogueLines[currentLineIndex].choices.Count > 0)
        {
            return; 
        }

        currentLineIndex++;

        // Verifica se há mais linhas ou se o diálogo terminou
        if (currentLineIndex < currentDialogue.dialogueLines.Count)
        {
            DisplayLine(currentDialogue.dialogueLines[currentLineIndex]);
        }
        else
        {
            EndDialogue();
        }
    }

    void DisplayLine(DialogueLine line)
    {
        // 1. Limpa o estado anterior
        ClearChoices(); 
        
        // 2. Atualiza a UI de texto e sprite
        nameText.text = line.characterName;
        dialogueText.text = line.dialogueText;
        
        // 3. Atualiza as Sprites de Expressão (A MUDANÇA PRINCIPAL)
        
        // Atualiza Olhos
        if (line.eyesSprite != null)
        {
            characterEyes.sprite = line.eyesSprite;
            // Garante que a imagem dos olhos esteja visível
            characterEyes.enabled = true; 
        }
        else if (line.characterName == "Narrador" || line.characterName == "")
        {
            // Oculta os olhos/boca quando for a vez de outro falante (narrador, etc.)
            characterEyes.enabled = false;
        }

        // Atualiza Boca
        if (line.mouthSprite != null)
        {
            characterMouth.sprite = line.mouthSprite;
            // Garante que a imagem da boca esteja visível
            characterMouth.enabled = true;
        }
        else if (line.characterName == "Narrador" || line.characterName == "")
        {
            characterMouth.enabled = false;
        }
        
        // --- FIM DA ATUALIZAÇÃO DE SPRITES ---

        // 4. Verifica e exibe as escolhas (Mecânica de Escolha)
        if (line.choices != null && line.choices.Count > 0)
        {
            choicesPanel.SetActive(true);
            nextButton.interactable = false; // DESATIVA o botão de próximo

            foreach (Choice choice in line.choices)
            {
                // Cria um novo botão a partir do prefab
                Button newChoiceButton = Instantiate(choiceButtonPrefab, choicesPanel.transform);
                newChoiceButton.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceText;
                
                // Adiciona o listener que chama MakeChoice com os dados específicos
                newChoiceButton.onClick.AddListener(() => MakeChoice(choice.moralPointsChange, choice.nextLineIndex));
            }
        }
        else
        {
            // É uma linha de diálogo normal, ATIVA o botão de próximo
            nextButton.interactable = true; 
        }
    }

    public void MakeChoice(int points, int nextLine)
    {
        // 1. Pontuação
        if (scoreManager != null)
        {
            scoreManager.AddMoralPoints(points);
        }

        // 2. Limpeza da UI
        ClearChoices();
        choicesPanel.SetActive(false);
        nextButton.interactable = true; 

        // 3. Lógica de salto para a próxima linha
        if (nextLine != -1 && nextLine < currentDialogue.dialogueLines.Count)
        {
            // Define o índice para o destino desejado. 
            // Subtraímos 1 porque NextLine() irá incrementar.
            currentLineIndex = nextLine - 1; 
            NextLine();
        }
        else
        {
            // Se o índice for -1 (ou inválido), o diálogo termina.
            EndDialogue(); 
        }
    }

    void ClearChoices()
    {
        // Destrói todos os botões filhos do painel de escolhas
        foreach (Transform child in choicesPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void EndDialogue()
    {
        Debug.Log("Diálogo finalizado.");
        // Chama o ScoreManager para iniciar a próxima fase (quiz)
        if (scoreManager != null)
        {
            scoreManager.StartModuleQuiz();
        }
    }
}
