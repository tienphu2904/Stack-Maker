using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasFailure : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText;

    public override void Setup()
    {
        base.Setup();
        Time.timeScale = 0;
    }

    public void SetCoin(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        UIManager.Instance.CloseAll();
        GameManager.Instance.SetState(GameManager.GameStatus.Menu);
    }

    public void ReplayButton()
    {
        Close(0);
        Time.timeScale = 1;
        LevelManager.Instance.Replay();
    }
}
