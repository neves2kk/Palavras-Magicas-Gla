using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string Fase1; // Nome da cena para a qual você deseja mudar

    public void ChangeScene()
    {
        SceneManager.LoadScene(Fase1); // Carrega a cena com o nome especificado
    }
}
