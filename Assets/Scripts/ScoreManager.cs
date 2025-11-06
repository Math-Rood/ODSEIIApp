using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Pontuações Atuais")]
    private int moralPoints = 0;
    private int ethicsQuizPoints = 0;
    private int totalModulePoints = 0;

    [Header("Referências da Cena")]
    public GameObject dialoguePanel; // O painel principal do diálogo
    public GameObject quizPanel;     // O painel principal do quiz (crie no Unity)

    void Start()
    {
        // Garante que o quiz não esteja visível no início da cena
        if (quizPanel != null)
        {
            quizPanel.SetActive(false);
        }
    }

    public void AddMoralPoints(int points)
    {
        moralPoints += points;
        Debug.Log($"Pontos de Moral: {moralPoints} (Mudança: {points})");
        // Você pode atualizar um texto de HUD aqui se quiser mostrar os pontos em tempo real
    }

    public void AddEthicsQuizPoints(int points)
    {
        ethicsQuizPoints += points;
        Debug.Log($"Pontos do Quiz de Ética: {ethicsQuizPoints} (Mudança: {points})");
    }

    public void StartModuleQuiz()
    {
        Debug.Log("FIM DO DIÁLOGO. Iniciando Quiz.");

        // Esconde o diálogo e mostra o painel do quiz
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        if (quizPanel != null) quizPanel.SetActive(true);

        // TODO: Adicione a lógica para carregar e exibir as 5 perguntas do quiz aqui.
        
        // Simulação: Após o quiz, você chamaria FinishModule()
        // Por enquanto, vamos chamar diretamente para demonstrar o resultado final:
        //Invoke("FinishModule", 5f); // Chama FinishModule após 5 segundos para simular o quiz
    }

    public void FinishModule()
    {
        totalModulePoints = moralPoints + ethicsQuizPoints;

        string summary = $@"
        --- FIM DO MÓDULO UM ---
        Pontuação de Moral (das Escolhas): **{moralPoints}**
        Pontuação do Quiz de Ética: **{ethicsQuizPoints}**
        Pontuação Total do Módulo: **{totalModulePoints}**
        ";

        Debug.Log(summary);
        // TODO: Exiba estas pontuações em um painel final na sua UI.
    }
}
