using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigationController : MonoBehaviour
{
    public Page startingPage;
    public float transitionDuration = 1f, fromScale = 2f, toScale = 0.5f;
    public Dictionary<string, Page> pages;
    private Page previousPage, currentPage;
    // Start is called before the first frame update
    void Awake()
    {
        pages = new Dictionary<string, Page>();
        foreach (Page p in GetComponentsInChildren<Page>())
        {
            pages.Add(p.gameObject.name, p);
            p.gameObject.SetActive(false);
        }
        currentPage = startingPage;
        startingPage.gameObject.SetActive(true);
    }

    #region Base Navigation Functions

    private void ExitCurrentPage(float targetScale)
    {
        if (currentPage == null) { return; }
        currentPage.LeavePage(transitionDuration, targetScale);
        previousPage = currentPage;
    }

    private void NextPage(Page page, float enterScale, float exitScale)
    {
        page.gameObject.SetActive(true);
        ExitCurrentPage(exitScale);
        currentPage = page;
        page.EnterPage(transitionDuration, enterScale);
    }

    public void BackPage()
    {
        if (currentPage == null || currentPage.parentPage == null) { return; }

        NextPage(currentPage.parentPage, fromScale, toScale);
    }

    public void Exit()
    {
        Application.Quit();
    }

    #endregion

    public void NavigateToPlay()
    {
        Page play = pages["PlayPage"];
        NextPage(play, toScale, fromScale);
    }
}