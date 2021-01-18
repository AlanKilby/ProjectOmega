using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum SceneTransition { HUB, Donjon, DeathScreen, MainMenu, Outro}
public class FadeSceneManagerScript : MonoBehaviour
{
    public static SceneTransition whatTransition;

    Animator anim;

    GameObject UI;

    private string currentAnimation;

    const string FADEIN = "FadeIn";
    const string FADEOUT = "FadeOut";

    private void Start()
    {
        anim = GetComponent<Animator>();
        UI = GameObject.Find("UI");
    }

    public void FadeIn()
    {
        ChangeAnimationState(FADEIN);
    }
    
    public void FadeOut()
    {
        ChangeAnimationState(FADEOUT);
    }

    public void Transition()
    {
        if(whatTransition == SceneTransition.HUB)
        {
            print("goHUB");
            SceneManager.LoadScene("UD_HUBtest");
        }
        if(whatTransition == SceneTransition.Donjon)
        {
            print("goDonjon");
            SceneManager.LoadScene("ProceduralGeneration");
        }
        if(whatTransition == SceneTransition.DeathScreen)
        {
            print("goDeathScreen");
            SceneManager.LoadScene("GameOver");
            Destroy(UI);
        }
        if(whatTransition == SceneTransition.MainMenu)
        {
            print("goMenu");
            SceneManager.LoadScene("Menu");
            Destroy(UI);
        }
        if(whatTransition == SceneTransition.Outro)
        {
            print("goOutro");
            SceneManager.LoadScene("UD_GameOutro");
            Destroy(UI);
        }
        currentAnimation = "FadeIdle";
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        anim.Play(newAnimation);
        currentAnimation = newAnimation;
    }
}
