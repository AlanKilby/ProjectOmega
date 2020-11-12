/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    
    private Animator anim;
    private string currentAnimation;

    //Animation States
    const string A = "A'";
    const string B = "B'";
    const string C = "C'";
     

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    //Pour activer animation A
    ChangeAnimationState(A);
     

     void ChangeAnimationState(string newAnimation)
    {   
        if (currentAnimation == newAnimation) return;
        
        anim.Play(newAnimation);
        
        currentAnimation = newAnimation;
    }

    
    

}   
     
//ENEMY FLASH

private Material matWhite;
private Material matDefault;
SpriteRenderer sr;

void Start()
{
sr = GetComponent<SpriteRenderer>();
matWhite = Resources.Load("WhiteFlash", typeof(Material) as Material;
matDefault = sr.material;
}

//dans la focntion de mort

sr.material = matWhite;
if(health <=0)
{
Death();
}
else
{
Invoke("ResetMaterial", 0.1f);
}

//pour faire revenir à l'état de base

void ResetMaterial()
{
sr.material = matDefault;
}


Pour désactiver un script à partir d'un autre (2 GameObject /=)

 public GameObject otherobj;//your other object
     public string scr;// your secound script name
     void Start () {
     (otherobj. GetComponent(scr) as MonoBehaviour).enabled = false;
     }

     
    
*/
