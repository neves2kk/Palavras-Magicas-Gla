using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System; // Adicionado para poder usar a classe DateTime

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public int totalScore;                          // Pontuação do jogador
    public Text scoreText;                          // Texto do placar
    
    public GameObject gameOver;                     // Tela de derrota do jogo 
    public GameObject gameWin;                      // Tela de vitória do jogo
    public GameObject dialogue;                     // Popup de diálogo do jogo
    public GameObject control;                      // UI de controle do personagem
    public GameObject goal;                         // Painel de objetivo da fasse
    public GameObject pause;                        // Botão pause
    public GameObject heart;                        // Ícone de conração

    // --- VARIÁVEIS PARA CAPTURAR A SESSÃO ATUAL ---
    private DateTime startTime;
    private List<string> palavrasCorretasDaSessao;
    private List<string> palavrasIncorretasDaSessao;
   
    void Start()
    {
        instance = this;

        // --- PREPARA A CAPTURA PARA A NOVA TENTATIVA ---
        startTime = DateTime.Now;
        palavrasCorretasDaSessao = new List<string>();
        palavrasIncorretasDaSessao = new List<string>();
        
        // A linha "gboard.SetStartPhase" foi removida daqui, pois a função não existe.
    }

    // --- MÉTODOS PARA REGISTRAR PALAVRAS ---
    public void RegistrarPalavraCorreta(string palavra)
    {
        if (palavrasCorretasDaSessao != null)
        {
            palavrasCorretasDaSessao.Add(palavra);
        }
    }

    public void RegistrarPalavraIncorreta(string palavra)
    {
        if (palavrasIncorretasDaSessao != null)
        {
            palavrasIncorretasDaSessao.Add(palavra);
        }
    }
    
    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString();
    }

    public void ShowGameOver()
    {
        EnviarDadosDaSessao("DERROTA");

        Personagem.Instance.StopMove = true;
        Personagem.Instance.Speed = 0f;
        Personagem.Instance.JumpForce = 0f;
        gameOver.SetActive(true);
        control.SetActive(false);
        dialogue.SetActive(false);
        goal.SetActive(false);
        pause.SetActive(false);
        heart.SetActive(false);
    }

    public void RestartGame(string lvlname)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(lvlname);
    }

    public void WinGame()
    {
        EnviarDadosDaSessao("VITORIA");

        Time.timeScale = 1;
        Personagem.Instance.StopMove = true;
        Personagem.Instance.Speed = 0f;
        Personagem.Instance.JumpForce = 0f;
        gameWin.SetActive(true);
        control.SetActive(false);
        dialogue.SetActive(false);
        goal.SetActive(false);
        pause.SetActive(false);
        heart.SetActive(false);
    }

    private void EnviarDadosDaSessao(string conclusao)
    {
        if (GlBoardController.instance == null || GlBoardController.instance.gboard == null)
        {
            Debug.LogWarning("GlBoardController não encontrado. Não foi possível salvar os dados da sessão.");
            return;
        }

        List<string> pathPlayerFinal = new List<string>();
        foreach(string palavra in palavrasCorretasDaSessao)
        {
            pathPlayerFinal.Add("CORRETA: " + palavra);
        }
        foreach(string palavra in palavrasIncorretasDaSessao)
        {
            pathPlayerFinal.Add("INCORRETA: " + palavra);
        }

        string currentSceneName = SceneManager.GetActiveScene().name;
        
        STATUS_SECTION status = (conclusao == "VITORIA") ? STATUS_SECTION.VITORIA : STATUS_SECTION.DERROTA;

        GlBoardController.instance.gboard.AddSectionInPhase(
            phase_id: currentSceneName,
            conclusion: status,
            perfomance: 0,
            dateTimeStartSection: startTime,
            dateTimeFinishSection: DateTime.Now,
            finalized_challenges: null,
            path_player: pathPlayerFinal,
            route_image_b64: null
        );
        
        StartCoroutine(GlBoardController.instance.gboard.SEND_USER_DATA());
    }

    public void Talking()
    {
        Dialogue1.instance.wordSpeed = 0.1f;
        Personagem.Instance.StopMove = true;
        Personagem.Instance.Speed = 0.1f ;
        Personagem.Instance.JumpForce *= 0f;
        control.SetActive(false);
        goal.SetActive(false);
        pause.SetActive(false);
    }
    
    public void StopTalk()
    {
        Personagem.Instance.StopMove = false;
        Personagem.Instance.Speed = 5f ;
        Personagem.Instance.JumpForce = 10f;
        control.SetActive(true);
        goal.SetActive(true);
        pause.SetActive(true);
    }

    public void PassLevel(string lvlname)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(lvlname);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
    }
}