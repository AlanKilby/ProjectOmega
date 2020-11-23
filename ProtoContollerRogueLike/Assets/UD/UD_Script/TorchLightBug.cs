using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLightBug : MonoBehaviour
{
    public GameObject torchLight;

    bool canBug;

    [SerializeField] float bugChance;
    [SerializeField] float timeBetweenBug;
    float timeBetweenBugTimer;
    [SerializeField] float firstDisappearTime;
    [SerializeField] float timeBetweenDisappear;
    [SerializeField] float secondDisappearTime;

    void Start()
    {
        canBug = true;
        torchLight.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (canBug)
        {
            BugChanceStart();
            timeBetweenBugTimer = timeBetweenBug;
            canBug = false;
        }
        TimerBetweenBug();

    }

    void TimerBetweenBug()
    {
        if (!canBug)
        {
            timeBetweenBugTimer -= Time.deltaTime;
            if(timeBetweenBugTimer <= 0)
            {
                canBug = true;
            }
        }
    }

    void BugChanceStart()
    {
        int i = Random.Range(0, 101);
        if (i <= bugChance)
        {
            canBug = false;
            StartCoroutine(Bug());
        }
    }

    private IEnumerator Bug()
    {
        torchLight.SetActive(false);
        yield return new WaitForSeconds(firstDisappearTime);
        torchLight.SetActive(true);
        yield return new WaitForSeconds(timeBetweenDisappear);
        torchLight.SetActive(false);
        yield return new WaitForSeconds(secondDisappearTime);
        torchLight.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HUB"))
        {
            torchLight.SetActive(false);
            canBug = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("HUB"))
        {
            torchLight.SetActive(false);
            canBug = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HUB"))
        {
            torchLight.SetActive(true);
            canBug = true;
        }
    }
}
