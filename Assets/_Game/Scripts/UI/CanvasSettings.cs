using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSettings : UICanvas
{
    [SerializeField] GameObject[] buttons;

    public override void Setup()
    {
        base.Setup();
        Time.timeScale = 0;
    }

    public void SetState(UICanvas canvas)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        if (canvas is CanvasMainMenu)
        {
            buttons[0].gameObject.SetActive(true);
        }
        else if (canvas is CanvasGamePlay)
        {
            buttons[1].gameObject.SetActive(true);
            buttons[2].gameObject.SetActive(true);

        }
    }

    public void MainMenuButton()
    {
        Close(0);
        GameManager.Instance.SetState(GameManager.GameStatus.Menu);
    }

    public void ContinueButton()
    {
        Close(0);
        Time.timeScale = 1;
    }

    public void ReplayButton()
    {
        Close(0);
        Time.timeScale = 1;
        LevelManager.Instance.Replay();
    }
}
