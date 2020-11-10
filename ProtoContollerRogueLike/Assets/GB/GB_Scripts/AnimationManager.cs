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
     
     

void ChangeAnimationState(string newAnimation)
{
    //empêche l'anim de s'interrompre elle-même
    if (currentAnimation == newAnimation) return;

    //joue l'anim
    anim.Play(newAnimation);

    //reassign the current state
    currentAnimation = newAnimation;

*/