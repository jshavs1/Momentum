using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigationController : MonoBehaviour
{
    public Page startingPage;
    public float transitionDuration = 1f, fromScale = 2f, toScale = 0.5f;
    public Dictionary<string, Page> pages;
    public AnimationCurve transitionCurve;


    private Page previousPage, currentPage, currentModal;

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
        currentPage.LeavePage(transitionDuration, targetScale, transitionCurve);
        previousPage = currentPage;
    }

    private void NextPage(Page page, float enterScale, float exitScale)
    {
        page.gameObject.SetActive(true);
        ExitCurrentPage(exitScale);
        currentPage = page;
        page.EnterPage(transitionDuration, enterScale, transitionCurve);
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

    public void PresentPageModal(Page page)
    {
        if (currentPage != null) { currentPage.interactable = false; }
        if (currentModal != null) { ExitCurrentModal(); }
        page.gameObject.SetActive(true);
        currentModal = page;
        page.EnterPage(transitionDuration, 0.9f, transitionCurve);
    }

    public void ExitCurrentModal()
    {
        if (currentPage != null) { currentPage.interactable = true; }
        if (currentModal == null) { return; }
        currentModal.LeavePage(transitionDuration, 0.9f, transitionCurve);
        currentModal = null;
    }

    #endregion

    public void NavigateTo(string page)
    {
        Page nextPage = pages[page];
        NextPage(nextPage, toScale, fromScale);
    }

    public void PresentAsModal(string page)
    {
        Page nextModal = pages[page];
        PresentPageModal(nextModal);
    }
}