using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameStatus { Menu, Ingame, Lose, Victory }

    private int coin;

    private void Start()
    {
        SetState(GameStatus.Menu);
    }

    public void SetState(GameStatus gamestatus)
    {
        switch (gamestatus)
        {
            case GameStatus.Menu:
                LevelManager.Instance.ResetLevel();
                UIManager.Instance.OpenUI<CanvasMainMenu>();
                break;
            case GameStatus.Ingame:
                UIManager.Instance.OpenUI<CanvasGamePlay>().updateLevelText();
                break;
            case GameStatus.Lose:
                UIManager.Instance.OpenUI<CanvasFailure>().SetCoin(coin);
                break;
            case GameStatus.Victory:
                UIManager.Instance.OpenUI<CanvasVictory>().SetCoin(coin);
                break;
            default:
                break;
        }
    }

    public void AddCoin(int coin)
    {
        this.coin += coin;
        UIManager.Instance.GetUI<CanvasGamePlay>().updateCoin(this.coin);
    }

    public void ResetCoin()
    {
        coin = 0;
        UIManager.Instance.GetUI<CanvasGamePlay>().updateCoin(coin);
    }
}
