using UnityEngine;
using UnityEngine.EventSystems;

public class DetectionButton : MonoBehaviour
{
    private bool movendoEsquerda;
    private bool movendoDireita;
    public bool walking {private set; get;}

 /*   public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name == "BotãoEsquerda")
        {
            movendoEsquerda = true;
            Debug.Log("Movendo para esquerda");
        }
        else if (eventData.pointerEnter.name == "BotãoDireita")
        {
            movendoDireita = true;
            Debug.Log("Movendo para a direita");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name == "BotãoEsquerda")
        {
            movendoEsquerda = false;
        }
        else if (eventData.pointerEnter.name == "BotãoDireita")
        {
            movendoDireita = false;
        }
    }
*/
    public void OnMouseDown(){
        walking = true;
    }
    public void OnMouseUp(){
        walking = false;
    }
    public void SetWalk(bool val)
    {
        walking = val;
    }
    
    // Você pode usar as variáveis movendoEsquerda e movendoDireita em outros scripts para controlar o movimento do personagem.
}

