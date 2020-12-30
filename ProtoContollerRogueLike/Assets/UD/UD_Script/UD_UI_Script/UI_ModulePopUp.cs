using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ModulePopUp : MonoBehaviour
{
    /*[SerializeField] Vector2 hidePosition;
    [SerializeField] Vector2 showPosition;

    [SerializeField] float showSpeed;
    [SerializeField] float timeToStayInPlace;
    RectTransform ownTransform;*/

    Animator anim;

    public bool show;
    public bool hide;

    void Start()
    {
        show = false;
        hide = true;
        /*ownTransform = GetComponent<RectTransform>();
        ownTransform.anchoredPosition = hidePosition;*/
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("show", show);
        anim.SetBool("hide", hide);
    }

    public void ShowPopUp()
    {
        /*Vector2 anchoredPosition = ownTransform.anchoredPosition;
        while (anchoredPosition.x <= showPosition.x)
        {
            ownTransform.anchoredPosition = new Vector2(ownTransform.anchoredPosition.x + showSpeed, 0);
            anchoredPosition = ownTransform.anchoredPosition;
            if (ownTransform.anchoredPosition.x <= showPosition.x)
            {
                StartCoroutine(PopUpStayInPlace());
            }
        }*/
        show = true;
        hide = false;
    }

    public void HidePopUp()
    {
        /*Vector2 anchoredPosition = ownTransform.anchoredPosition;
        while (anchoredPosition.x >= hidePosition.x)
        {
            ownTransform.anchoredPosition = new Vector2(ownTransform.anchoredPosition.x - showSpeed, 0);
            anchoredPosition = ownTransform.anchoredPosition;
        }*/
        show = false;
        hide = true;

    }

    /*IEnumerator PopUpStayInPlace()
    {
        yield return new WaitForSeconds(timeToStayInPlace);
        HidePopUp();
    }*/
}
