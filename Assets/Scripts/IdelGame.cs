using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;

//Data center
[Serializable]
public class PlayerData
{
    public BigDouble coins;
    public BigDouble coinsClickValue;
    public BigDouble coinsPerSecond;

    public BigDouble clickUpgrade1Level;
    public BigDouble clickUpgrade2Level;
    public BigDouble productionUpgrade1Level;
    public BigDouble productionUpgrade2Level;
    public BigDouble productionUpgrade2Power;

    public BigDouble gems;
    public BigDouble gemboost;
    public BigDouble gemsToGet;

    public PlayerData()
    {
        FullReset();
    }

    public void FullReset()
    {
        coins = 0;
        coinsClickValue = 1;
        coinsPerSecond = 0;
        clickUpgrade1Level = 0;
        clickUpgrade2Level = 0;
        productionUpgrade1Level = 0;
        productionUpgrade2Level = 0;
        productionUpgrade2Power = 5;
        gems = 0;
        gemboost = 1;
        gemsToGet = 0;
    }
}

//Program of main game
public class IdelGame : MonoBehaviour
{
    public PlayerData data;

    //Text
    public Text coinsText;
    public Text clickValueText;

    public Text coinsPerSecText;
    public Text clickUpgrade1Text;
    public Text clickUpgrade2Text;
    public Text productionUpgrade1Text;
    public Text productionUpgrade2Text;

    public Text gemsText;
    public Text gemBoostText;
    public Text gemsToGetText;

    public Image clickUpgrade1Bar;
    public Image clickUpgrade2Bar;
    public Image productionUpgrade1Bar;
    public Image productionUpgrade2Bar;

    public Text clickUpgrade1MaxText;
    public Text clickUpgrade2MaxText;
    public Text productionUpgrade1MaxText;
    public Text productionUpgrade2MaxText;

    public CanvasGroup header;
    public CanvasGroup mainMenuGroup;
    public CanvasGroup upgradesGroup;
    public CanvasGroup settingScreen;

    public GameObject settings;

    //value
    public void Start()
    {
        //set FPS to 60
        Application.targetFrameRate = 60;

        //turn on main screen and turn off the upgrade screen
        CanvasGroupChanger(true, mainMenuGroup);
        CanvasGroupChanger(false, upgradesGroup);
        CanvasGroupChanger(false, settingScreen);
        CanvasGroupChanger(true, header);

        //Load the data
        SaveSystem.LoadPlayer(ref data);
    }

    //Turn on and off the CanvasGroup
    public void CanvasGroupChanger(bool x, CanvasGroup y)
    {
        if (x)
        {
            y.alpha = 1;
            y.interactable = true;
            y.blocksRaycasts = true;
            return;
        }

        else
        {
            y.alpha = 0;
            y.interactable = false;
            y.blocksRaycasts = false;
        }
    }

    //Update every frame
    public void Update()
    {
        //gems
        data.gemsToGet = (150 * Sqrt(data.coins / 1e7));
        data.gemboost = (data.gems * 0.05) +1;

        gemsToGetText.text = "Prestige:\n+" + Floor(data.gemsToGet).ToString("F0") + " Gems";
        gemsText.text = "Gems: " + Floor(data.gems).ToString("F0");
        gemBoostText.text = data.gemboost.ToString("F2") + "x boost";

        data.coinsPerSecond = (data.productionUpgrade1Level + (data.productionUpgrade2Power * data.productionUpgrade2Level)) * data.gemboost;

        //coinsClickValue text
        clickValueText.text = "Click\n+" + NotationMethod(data.coinsClickValue,y:"F0") + "Coins";

        //coins text
        coinsText.text = "Coins: " + NotationMethod(data.coins, y: "F0");

        //coins per second text
        coinsPerSecText.text = data.coinsPerSecond.ToString("F0") + " coins/s";

        //clickUpgrade1CostString
        string clickUpgrade1CostString;
        var clickUpgrade1Cost = 10 * Pow(1.07, data.clickUpgrade1Level);
        clickUpgrade1CostString = NotationMethod(clickUpgrade1Cost, y: "F0");

        //clickUpgrade1LevelString
        string clickUpgrade1LevelString;
        clickUpgrade1LevelString = NotationMethod(data.clickUpgrade1Level, y: "F0");

        //clickUpgrade2CostString
        string clickUpgrade2CostString;
        var clickUpgrade2Cost = 100 * Pow(1.09, data.clickUpgrade2Level);
        clickUpgrade2CostString = NotationMethod(clickUpgrade2Cost, y: "F0");

        //clickUpgrade2LevelString
        string clickUpgrade2LevelString;
        clickUpgrade2LevelString = NotationMethod(data.clickUpgrade2Level, y: "F0");

        //productionUpgrade1CostString
        string productionUpgrade1CostString;
        var productionUpgrade1Cost = 25 * Pow(1.07, data.productionUpgrade1Level);
        productionUpgrade1CostString = NotationMethod(productionUpgrade1Cost, y: "F0");

        //productionUpgrade1LevelString
        string productionUpgrade1LevelString;
        productionUpgrade1LevelString = NotationMethod(data.productionUpgrade1Level, y: "F0");

        //productionUpgrade2CostString
        string productionUpgrade2CostString;
        var productionUpgrade2Cost = 250 * Pow(1.07, data.productionUpgrade2Level);
        productionUpgrade2CostString = NotationMethod(productionUpgrade2Cost, y: "F0");

        //productionUpgrade2LevelString
        string productionUpgrade2LevelString;
        productionUpgrade2LevelString = NotationMethod(data.productionUpgrade2Level, y: "F0");

        //show text and calculation
        clickUpgrade1Text.text = "Click Upgrade 1\nCost: " + clickUpgrade1CostString + "coins\nPower: +1 Click\nLevel: " + clickUpgrade1LevelString;
       
        clickUpgrade2Text.text = "Click Upgrade 2\nCost: " + clickUpgrade2CostString + "coins\nPower: +5 Click\nLevel: " + clickUpgrade2LevelString;

        productionUpgrade1Text.text = "Production Upgrade 1\nCost: " + productionUpgrade1CostString + "coins\nPower: +" + data.gemboost.ToString("F2") + " coins/s\nLevel: " + productionUpgrade1LevelString;
        productionUpgrade2Text.text = "Production Upgrade 2\nCost: " + productionUpgrade2CostString+ "coins\nPower: +" + (data.productionUpgrade2Power * data.gemboost).ToString("F2") + " coins /s\nLevel: " + productionUpgrade2LevelString;

        data.coins += data.coinsPerSecond * Time.deltaTime;

        //clickupgrade1bar
        if (data.coins / clickUpgrade1Cost < 0.01)
        {
            clickUpgrade1Bar.fillAmount = 0;
        }
        else if(data.coins /clickUpgrade1Cost > 10)
        {
            clickUpgrade1Bar.fillAmount = 1;
        }
        else
        {
        clickUpgrade1Bar.fillAmount = (float)(data.coins / clickUpgrade1Cost).ToDouble();
        }

        //clickupgrade2bar
        if (data.coins / clickUpgrade2Cost < 0.01)
        {
            clickUpgrade2Bar.fillAmount = 0;
        }
        else if (data.coins / clickUpgrade2Cost > 10)
        {
            clickUpgrade2Bar.fillAmount = 1;
        }
        else
        {
            clickUpgrade2Bar.fillAmount = (float)(data.coins / clickUpgrade2Cost).ToDouble();
        }

        //productionupgrade1bar
        if (data.coins / productionUpgrade1Cost < 0.01)
        {
            productionUpgrade1Bar.fillAmount = 0;
        }
        else if (data.coins / productionUpgrade1Cost > 10)
        {
            productionUpgrade1Bar.fillAmount = 1;
        }
        else
        {
            productionUpgrade1Bar.fillAmount = (float)(data.coins / productionUpgrade1Cost).ToDouble();
        }

        //productionupgrade2bar
        if (data.coins / productionUpgrade2Cost < 0.01)
        {
            productionUpgrade2Bar.fillAmount = 0;
        }
        else if (data.coins / productionUpgrade2Cost > 10)
        {
            productionUpgrade2Bar.fillAmount = 1;
        }
        else
        {
            productionUpgrade2Bar.fillAmount = (float)(data.coins / productionUpgrade2Cost).ToDouble();
        }

        //Buy Max text
        clickUpgrade1MaxText.text = "Buy Max (" + BuyClickUpgrade1MaxCount() + ")";
        clickUpgrade2MaxText.text = "Buy Max (" + BuyClickUpgrade2MaxCount() + ")";
        productionUpgrade1MaxText.text = "Buy Max (" + BuyProductionUpgrade1MaxCount() + ")";
        productionUpgrade2MaxText.text = "Buy Max (" + BuyProductionUpgrade2MaxCount() + ")";

        SaveSystem.SavePlayer(data);
    }

    //Method of calculate and round in double
    public string NotationMethod(BigDouble x, string y)
    {
        if (x > 1000)
        {
            var exponent = Floor(Log10(Abs(x)));
            var mantissa = x / Pow(10, exponent);
            return mantissa.ToString(format: "F2") + "e" + exponent;
        }
        else
        {
            return x.ToString(y);
        }
    }

    //Method of calculate and round in float
    public string NotationMethod(float x, string y)
    {
        if (x > 1000)
        {
            var exponent = Mathf.Floor(Mathf.Log10(Mathf.Abs(x)));
            var mantissa = x / Mathf.Pow(10, exponent);
            return mantissa.ToString(format: "F2") + "e" + exponent;
        }
        else
        {
            return x.ToString(y);
        }
    }

    //prestige
    public void Prestige()
    {
        if(data.coins > 1000)
        {
            data.coins = 0;
            data.coinsClickValue = 1;
            data.productionUpgrade2Power = 5;

            data.clickUpgrade1Level = 0;
            data.clickUpgrade2Level = 0;
            data.productionUpgrade1Level = 0;
            data.productionUpgrade2Level = 0;

            data.gems += data.gemsToGet;
        }
    }


    //Buttons
    public void Click()
    {
        data.coins += data.coinsClickValue;
    }

    //ClickUpgrade1
    public void BuyClickUpgrade1()
    {
        var cu1cost = 10 * Pow(1.07, data.clickUpgrade1Level);
        if(data.coins >= cu1cost)
        {
            data.clickUpgrade1Level++;
            data.coins -= cu1cost;
        cu1cost *= 1.07;
            data.coinsClickValue++;
        }
        else
        {
            print("BuyClickUpgrade1() else");
        }
    }

    public void BuyClickUpgrade1Max()
    {
        print("BuyClickUpgrade1Max() enter");
        //print("buy max enter");
        var cu1b = 10;
        var cu1c = data.coins;
        var cu1r = 1.07;
        var cu1k = data.clickUpgrade1Level;
        var cu1n = Floor(Log(cu1c * (cu1r - 1) / (cu1b * Pow(cu1r, cu1k)) + 1, cu1r));

        var cu1cost = cu1b * ((Pow(cu1r, cu1k) * (Pow(cu1r, cu1n) - 1)) / (cu1r - 1));
        print("culcost should be 10 but its" + cu1cost );
        if(data.coins >= cu1cost)
        {
            print("BuyClickUpgrade1Max() if statement");
            data.clickUpgrade1Level += cu1n;
            data.coins -= cu1cost;
            data.coinsClickValue += cu1n;
        }
        else
        {
            print("BuyClickUpgrade1Max() else");
        }
        print("BuyClickUpgrade1Max() exit");
    }

    public BigDouble BuyClickUpgrade1MaxCount()
    {
        var cu1b = 10;
        var cu1c = data.coins;
        var cu1r = 1.07;
        var cu1k = data.clickUpgrade1Level;
        var cu1n = Floor(Log(cu1c * (cu1r - 1) / (cu1b * Pow(cu1r, cu1k)) + 1, cu1r));
        return cu1n;
    }


    //ClickUpgrade2
    public void BuyClickUpgrade2()
    {
        var cu2cost = 100 * Pow(1.09, data.clickUpgrade2Level);
        if (data.coins >= cu2cost)
        {
            data.clickUpgrade2Level++;
            data.coins -= cu2cost;
            cu2cost *= 1.09;
            data.coinsClickValue += 5;
        }
    }

    public void BuyClickUpgrade2Max()
    {
        var cu2b = 100;
        var cu2c = data.coins;
        var cu2r = 1.09;
        var cu2k = data.clickUpgrade2Level;
        var cu2n = Floor(Log(cu2c * (cu2r - 1) / (cu2b * Pow(cu2r, cu2k)) + 1, cu2r));

        var cu2cost = cu2b * ((Pow(cu2r, cu2k) * (Pow(cu2r, cu2n) - 1)) / (cu2r - 1));

        if (data.coins >= cu2cost)
        {
            data.clickUpgrade2Level += cu2n;
            data.coins -= cu2cost;
            data.coinsClickValue += cu2n;
        }
    }

    public BigDouble BuyClickUpgrade2MaxCount()
    {
        var cu2b = 100;
        var cu2c = data.coins;
        var cu2r = 1.09;
        var cu2k = data.clickUpgrade2Level;
        var cu2n = Floor(Log(cu2c * (cu2r - 1) / (cu2b * Pow(cu2r, cu2k)) + 1, cu2r));
        return cu2n;
    }


    //ProductionUpgrade1
    public void BuyProductionUpgrade1()
    {
        var pu1cost = 25 * Pow(1.07, data.productionUpgrade1Level);
        if (data.coins >= pu1cost)
        {
            data.productionUpgrade1Level++;
            data.coins -= pu1cost;
            pu1cost *= 1.07;
            pu1cost++;
        }
    }
    public void BuyProductionUpgrade1Max()
    {
        var pu1b = 25;
        var pu1c = data.coins;
        var pu1r = 1.07;
        var pu1k = data.productionUpgrade1Level;
        var pu1n = Floor(Log(pu1c * (pu1r - 1) / (pu1b * Pow(pu1r, pu1k)) + 1, pu1r));

        var pu1cost = pu1b * ((Pow(pu1r, pu1k) * (Pow(pu1r, pu1n) - 1)) / (pu1r - 1));

        if (data.coins >= pu1cost)
        {
            data.productionUpgrade1Level += pu1n;
            data.coins -= pu1cost;
            data.coinsClickValue += pu1n;
        }
    }

    public BigDouble BuyProductionUpgrade1MaxCount()
    {
        var pu1b = 25;
        var pu1c = data.coins;
        var pu1r = 1.07;
        var pu1k = data.productionUpgrade1Level;
        var pu1n = Floor(Log(pu1c * (pu1r - 1) / (pu1b * Pow(pu1r, pu1k)) + 1, pu1r));
        return pu1n;
    }


    //ProductionUpgrade2
    public void BuyProductionUpgrade2()
    {
        var pu2cost = 250 * Pow(1.07, data.productionUpgrade2Level);
        if (data.coins >= pu2cost)
        {
            data.productionUpgrade2Level++;
            data.coins -= pu2cost;
            pu2cost *= 1.07;
            pu2cost++;
        }
    }

    public void BuyProductionUpgrade2Max()
    {
        var pu2b = 250;
        var pu2c = data.coins;
        var pu2r = 1.07;
        var pu2k = data.productionUpgrade2Level;
        var pu2n = Floor(Log(pu2c * (pu2r - 1) / (pu2b * Pow(pu2r, pu2k)) + 1, pu2r));

        var pu2cost = pu2b * ((Pow(pu2r, pu2k) * (Pow(pu2r, pu2n) - 1)) / (pu2r - 1));

        if (data.coins >= pu2cost)
        {
            data.productionUpgrade2Level += pu2n;
            data.coins -= pu2cost;
            data.coinsClickValue += pu2n;
        }
    }

    public BigDouble BuyProductionUpgrade2MaxCount()
    {
        var pu2b = 250;
        var pu2c = data.coins;
        var pu2r = 1.07;
        var pu2k = data.productionUpgrade2Level;
        var pu2n = Floor(Log(pu2c * (pu2r - 1) / (pu2b * Pow(pu2r, pu2k)) + 1, pu2r));
        return pu2n;
    }

    //Change Tabs
    public void ChangeTabs(string id)
    {
        switch (id)
        {
            case "upgrades":
                CanvasGroupChanger(false, mainMenuGroup);
                CanvasGroupChanger(true, upgradesGroup);
                CanvasGroupChanger(false, settingScreen);
                CanvasGroupChanger(true, header);
                break;
            case "main":
                CanvasGroupChanger(true, mainMenuGroup);
                CanvasGroupChanger(false, upgradesGroup);
                CanvasGroupChanger(false, settingScreen);
                CanvasGroupChanger(true, header);
                break;
            case "Settings":
                CanvasGroupChanger(false, mainMenuGroup);
                CanvasGroupChanger(false, upgradesGroup);
                CanvasGroupChanger(true, settingScreen);
                CanvasGroupChanger(false, header);
                break;
            case "BackFromSetting":
                CanvasGroupChanger(true, mainMenuGroup);
                CanvasGroupChanger(false, upgradesGroup);
                CanvasGroupChanger(false, settingScreen);
                CanvasGroupChanger(true, header);
                break;
        }
    }

    public void GoToSettings()
    {
        settings.gameObject.SetActive(true);
    }
    public void GoBackFromSettings()
    {
        settings.gameObject.SetActive(false);
    }

    //FullReset
    public void FullReset()
    {
        data.FullReset();
    }
}
