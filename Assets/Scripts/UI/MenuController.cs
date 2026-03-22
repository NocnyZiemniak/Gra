using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public TabsController tabsController;

    int currentTab = -1;

    void Start()
    {
        menuCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMenuWithTab(0);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleMenuWithTab(1);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMenuWithTab(2);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenuWithTab(3);
        }
    }

    void ToggleMenuWithTab(int tabIndex)
    {
        if (!menuCanvas.activeSelf)
        {
            menuCanvas.SetActive(true);
            tabsController.ActiveTab(tabIndex);
            currentTab = tabIndex;
        }
        else
        {
            if (currentTab == tabIndex)
            {
                menuCanvas.SetActive(false);
                currentTab = -1;
            }
            else
            {
                tabsController.ActiveTab(tabIndex);
                currentTab = tabIndex;
            }
        }
    }
}