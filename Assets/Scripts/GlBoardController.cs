using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlBoardController : MonoBehaviour
{
    // Variável estática para criar o padrão Singleton
    public static GlBoardController instance;

    // Referência pública para o objeto da biblioteca GLA
    public GLBoard gboard;

    // Awake é chamado quando a instância do script é carregada
    void Awake()
    {
        // Lógica para garantir que exista apenas uma instância deste objeto no jogo
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Impede que o objeto seja destruído ao carregar novas cenas
        }
        else
        {
            // Se uma instância já existe, destrói esta para evitar duplicatas
            Destroy(gameObject);
        }
    }

    // --- NOVO: Start é chamado uma vez, após o Awake ---
    void Start()
    {
        // Verifica se a cena atual NÃO é o Menu, para evitar recarregar a cena em loop.
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            SceneManager.LoadScene("Menu");
        }
    }

    // Função pública para ser chamada pelo AuthManager após o login/registro
    public void instantiateGlBoard(string userId)
    {
        // Cria uma nova instância da biblioteca GLBoard, associada ao ID do jogo e ao ID do usuário
        gboard = new GLBoard("F6ypBJQWSCseX9hKNrNxsA", userId);
        
        // Chama a função para configurar a estrutura das fases do jogo
        InitializeGlaPhases();
    }

    // Prepara a estrutura de dados do GLA com as informações das fases
    private void InitializeGlaPhases()
    {
        if (gboard == null) return;
        
        // Define o número total de fases que o jogo possui
        gboard.SetQuantPhaseGame(4); // 4 = Tutorial + 3 Fases

        // Cria um array com os nomes exatos das cenas de cada fase
        string[] phaseNames = { "Tutorial", "Fase 1", "Fase 2", "Fase 3" }; // Importante: estes nomes devem ser idênticos aos nomes das suas cenas na Unity

        // Percorre a lista de nomes e registra cada fase no sistema GLA
        foreach (var name in phaseNames)
        {
            gboard.AddPhaseGame(name); // Versão corrigida, com apenas um argumento
        }
        
        // Envia os dados da estrutura inicial do jogo para o servidor
        StartCoroutine(gboard.SEND_USER_DATA());
    }
    
    // Função para definir os dados demográficos do jogador
    public void setUserData(string name, string birthday, string gender)
    {
        GENDER g;
        if (gender.ToLower() == "masculino")
        {
            g = GENDER.MASCULINO;
        }
        else if (gender.ToLower() == "feminino")
        {
            g = GENDER.FEMININO;
        }
        else
        {
            g = GENDER.OUTROS;
        }
        
        gboard.SetPlayerData(name, birthday, g);
        StartCoroutine(gboard.SEND_USER_DATA());
    }

    // Função para carregar os dados do usuário e registrar a data do último login
    public async void setLastLogin()
    {
        await gboard.LOAD_USER_DATA();
        gboard.SetLastLogin(DateTime.Now);
        StartCoroutine(gboard.SEND_USER_DATA());
    }
}