using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI levelText;

    public override void Setup()
    {
        base.Setup();
        updateCoin(0);
    }

    public void updateLevelText()
    {
        levelText.text = $"LEVEL {LevelManager.Instance.CurrentLevel}";
    }

    public void updateCoin(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void SettingButton()
    {
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this);
    }
}
