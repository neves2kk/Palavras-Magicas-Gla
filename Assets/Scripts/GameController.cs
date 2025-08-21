using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    GLBoard gLBoard;
    public int totalScore;                          // Pontuação do jogador
    public Text scoreText;                          // Texto do placar
    
    public GameObject gameOver;                     // Tela de derrota do jogo 
    public GameObject gameWin;                      // Tela de vitória do jogo
    public GameObject dialogue;                     // Popup de diálogo do jogo
    public GameObject control;                      // UI de controle do personagem
    public GameObject goal;                         // Painel de objetivo da fasse
    public GameObject pause;                        // Botão pause
    public GameObject heart;                        // Ícone de conração


    async void Awake()
    {
        gLBoard = new GLBoard("F6ypBJQWSCseX9hKNrNxsA", SystemInfo.deviceUniqueIdentifier);
        await gLBoard.LOAD_USER_DATA();
        gLBoard.SetCustomReport("Esse jogador possui dificiculdade em soma");
        StartCoroutine(gLBoard.SEND_USER_DATA());
    }
   

    // Start is called before the first frame update

    void Start()
    {
        instance = this;                    // Instancia a ClasseController
    }

    // Atualizar o placar
    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString();     // Atualiza o placar do jogo convertendo um número inteiro em uma string
    }

    // Reiniciar o jogo
    public void ShowGameOver()
    {
        Personagem.Instance.StopMove = true;        // Desabilita a movimentação do personagem
        Personagem.Instance.Speed = 0f;             // Zera a velocidade do personagem
        Personagem.Instance.JumpForce = 0f;         // Zera a força de pulo do personagem
        gameOver.SetActive(true);                   // Ativa a tela de derrota
        control.SetActive(false);                   // Desativa o controle do jogador
        dialogue.SetActive(false);                  // Desativa o diálogo
        goal.SetActive(false);                      // Desativa o painel central do objetivo
        pause.SetActive(false);                     // Desativa o botão de pause 
        heart.SetActive(false);                     // Desativa o botão de coração
    }

    // Recomeça o jogo
    public void RestartGame(string lvlname)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(lvlname);            // Carrega mesma cena do jogo do jogo 
    }

    // Avançar o jogo
    public void WinGame()
    {
        Time.timeScale = 1;
        Personagem.Instance.StopMove = true;        // Desabilita a movimentação do personagem
        Personagem.Instance.Speed = 0f;             // Zera a velocidade do personagem
        Personagem.Instance.JumpForce = 0f;         // Zera a força de pulo do personagem
        gameWin.SetActive(true);                    // Ativa a tela de vitória 
        control.SetActive(false);                   // Desabilita o controle do jogador
        dialogue.SetActive(false);                  // Desabilita o diálogo
        goal.SetActive(false);                      // Desativa o painel central do objetivo
        pause.SetActive(false);                     // Desativa o botão de pause
        heart.SetActive(false);                     // Desativa o botão de coração
    }

    // Faz com que o personagem pare de andar enquanto o npc fala
    public void Talking()
    {
        Dialogue1.instance.wordSpeed = 0.1f;        // Defini a velocidade de fala para 0.1
        Personagem.Instance.StopMove = true;        // Desabilita a movimentação do personagem
        Personagem.Instance.Speed = 0.1f ;          // Zera a velocidade do personagem
        Personagem.Instance.JumpForce *= 0f;        // Zera a força de pulo do personagem
        control.SetActive(false);                   // Desabilita o controle do jogador
        goal.SetActive(false);                      // Desativa o painel central do objetivo
        pause.SetActive(false);                     // Desativa o botão de pause
    }
    
    // Retorna o movimento do personagem
    public void StopTalk()
    {
        Personagem.Instance.StopMove = false;      // Habilita a movimentação do personagem
        Personagem.Instance.Speed = 5f ;           // Altera a velocidade do personagem
        Personagem.Instance.JumpForce = 10f;       // Altera a força de pulo do personagem
        control.SetActive(true);                   // Ativa o controle do jogador
        goal.SetActive(true);                      // Ativa o painel central do objetivo    
        pause.SetActive(true);                     // Ativa o botão de pause

    }


    public void PassLevel(string lvlname)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(lvlname);            // Avança para a próxima fase do jogo 
    }

     public void PauseGame()
    {
        Time.timeScale = 0;                         // pausado
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;                         // sai do pause
    }
}
