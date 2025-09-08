using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public int totalScore;
    public Text scoreText;
    
    public GameObject gameOver;
    public GameObject gameWin;
    public GameObject dialogue;
    public GameObject control;
    public GameObject goal;
    public GameObject pause;
    public GameObject heart;

    // Variáveis para capturar os dados da sessão atual
    private DateTime startTime;
    private List<string> palavrasCorretasDaSessao;
    private List<string> palavrasIncorretasDaSessao;
    
    // --- NOVO ---
    // Contador para as tentativas incorretas entre acertos.
    private int tentativasIncorretasDesdeUltimoAcerto = 0;
    
    private bool hasSessionEnded = false;
   
    void Start()
    {
        instance = this;

        // Prepara as variáveis para a nova tentativa
        startTime = DateTime.Now;
        palavrasCorretasDaSessao = new List<string>();
        palavrasIncorretasDaSessao = new List<string>();
        
        // --- NOVO ---
        // Reseta os contadores no início de cada fase.
        tentativasIncorretasDesdeUltimoAcerto = 0;
        hasSessionEnded = false;
    }

    // --- MODIFICADO ---
    public void RegistrarPalavraCorreta(string palavra)
    {
        if (palavrasCorretasDaSessao != null)
        {
            string registro = $"CORRETA: {palavra} (tentativas_ate_acerto: {tentativasIncorretasDesdeUltimoAcerto})";
            palavrasCorretasDaSessao.Add(registro);

            // Zera o contador, pois o jogador acertou
            tentativasIncorretasDesdeUltimoAcerto = 0;
        }
    }

    // --- MODIFICADO ---
    public void RegistrarPalavraIncorreta(string palavra)
    {
        if (palavrasIncorretasDaSessao != null)
        {
            // Adiciona a palavra incorreta à sua lista
            palavrasIncorretasDaSessao.Add("INCORRETA: " + palavra);
            
            // Incrementa o contador de tentativas
            tentativasIncorretasDesdeUltimoAcerto++;
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
        if (hasSessionEnded) return;
        hasSessionEnded = true;

        if (GlBoardController.instance == null || GlBoardController.instance.gboard == null)
        {
            Debug.LogWarning("GlBoardController não encontrado. Dados da sessão não foram salvos.");
            return;
        }

        List<string> pathPlayerFinal = new List<string>();
        pathPlayerFinal.AddRange(palavrasCorretasDaSessao);
        pathPlayerFinal.AddRange(palavrasIncorretasDaSessao);

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

    public void RestartGame(string lvlname)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(lvlname);
    }
    
    public void Talking()
    {
        if (Dialogue1.instance != null)
        {
            Dialogue1.instance.wordSpeed = 0.1f;
        }
        Personagem.Instance.StopMove = true;
        Personagem.Instance.Speed = 0.1f;
        Personagem.Instance.JumpForce *= 0f;
        control.SetActive(false);
        goal.SetActive(false);
        pause.SetActive(false);
    }
    
    public void StopTalk()
    {
        Personagem.Instance.StopMove = false;
        Personagem.Instance.Speed = 5f;
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