using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public void PlayButton()
    {
        Close(0);
        GameManager.Instance.SetState(GameManager.GameStatus.Ingame);
        LevelManager.Instance.InitMap(1);
    }

    public void SettingButton()
    {
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this);
    }
}
