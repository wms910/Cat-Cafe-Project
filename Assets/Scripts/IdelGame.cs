using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;

public class IdelGame : MonoBehaviour
{
    public Text coinsText;
    public Text clickValueText;
    public BigDouble coins;
    public BigDouble coinsClickValue;

    public Text coinsPerSecText;
    public Text clickUpgrade1Text;
    public Text clickUpgrade2Text;
    public Text productionUpgrade1Text;
    public Text productionUpgrade2Text;

    public BigDouble coinsPerSecond;

    public BigDouble clickUpgrade1Level;
    public BigDouble clickUpgrade2Level;
    public BigDouble productionUpgrade1Level;
    public BigDouble productionUpgrade2Level;
    public BigDouble productionUpgrade2Power;

    public Text gemsText;
    public Text gemBoostText;
    public Text gemsToGetText;

    public BigDouble gems;
    public BigDouble gemboost;
    public BigDouble gemsToGet;

    public Image clickUpgrade1Bar;
    public Image clickUpgrade2Bar;
    public Image productionUpgrade1Bar;
    public Image productionUpgrade2Bar;

    public Text clickUpgrade1MaxText;
    public Text clickUpgrade2MaxText;
    public Text productionUpgrade1MaxText;
    public Text productionUpgrade2MaxText;

    public CanvasGroup mainMenuGroup;
    public CanvasGroup upgradesGroup;

    //value
    public void Start()
    {
        Application.targetFrameRate = 60;

        CanvasGroupChanger(true, mainMenuGroup);
        CanvasGroupChanger(false, upgradesGroup);

        Load();
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

    //Load the data
    public void Load()
    {
        coins = Parse(PlayerPrefs.GetString("coins", "0"));
        coinsClickValue = Parse(PlayerPrefs.GetString("coinsClickValue", "1"));
        productionUpgrade2Power = Parse(PlayerPrefs.GetString("productionUpgrade2Power", "5"));

        gems = Parse(PlayerPrefs.GetString("gems", "0"));

        clickUpgrade1Level = Parse(PlayerPrefs.GetString("clickUpgrade1Level", "0"));
        clickUpgrade2Level = Parse(PlayerPrefs.GetString("clickUpgrade2Level", "0"));
        productionUpgrade1Level = Parse(PlayerPrefs.GetString("productionUpgrade1Level", "0"));
        productionUpgrade2Level = Parse(PlayerPrefs.GetString("productionUpgrade2Level", "0"));
    }

    //Save the data
    public void Save()
    {
        PlayerPrefs.SetString("coins", coins.ToString());
        PlayerPrefs.SetString("coinsClickValue", coinsClickValue.ToString());
        PlayerPrefs.SetString("productionUpgrade2Power", productionUpgrade2Power.ToString());

        PlayerPrefs.SetString("gems", gems.ToString());

        PlayerPrefs.SetString("clickUpgrade1Level", clickUpgrade1Level.ToString());
        PlayerPrefs.SetString("clickUpgrade2Level", clickUpgrade2Level.ToString());
        PlayerPrefs.SetString("productionUpgrade1Level", productionUpgrade1Level.ToString());
        PlayerPrefs.SetString("productionUpgrade2Level", productionUpgrade2Level.ToString());
    }

    //Update every frame
    public void Update()
    {
        //gems
        gemsToGet = (150 * Sqrt(coins / 1e7));
        gemboost = (gems * 0.05) +1;

        gemsToGetText.text = "Prestige:\n+" + Floor(gemsToGet).ToString("F0") + " Gems";
        gemsText.text = "Gems: " + Floor(gems).ToString("F0");
        gemBoostText.text = gemboost.ToString("F2") + "x boost";

        coinsPerSecond = (productionUpgrade1Level + (productionUpgrade2Power * productionUpgrade2Level)) * gemboost;

        //coinsClickValue text
        clickValueText.text = "Click\n+" + NotationMethod(coinsClickValue,y:"F0") + "Coins";

        //coins text
        coinsText.text = "Coins: " + NotationMethod(coins, y: "F0");

        //coins per second text
        coinsPerSecText.text = coinsPerSecond.ToString("F0") + " coins/s";

        //clickUpgrade1CostString
        string clickUpgrade1CostString;
        var clickUpgrade1Cost = 10 * Pow(1.07, clickUpgrade1Level);
        clickUpgrade1CostString = NotationMethod(clickUpgrade1Cost, y: "F0");

        //clickUpgrade1LevelString
        string clickUpgrade1LevelString;
        clickUpgrade1LevelString = NotationMethod(clickUpgrade1Level, y: "F0");

        //clickUpgrade2CostString
        string clickUpgrade2CostString;
        var clickUpgrade2Cost = 100 * Pow(1.09, clickUpgrade2Level);
        clickUpgrade2CostString = NotationMethod(clickUpgrade2Cost, y: "F0");

        //clickUpgrade2LevelString
        string clickUpgrade2LevelString;
        clickUpgrade2LevelString = NotationMethod(clickUpgrade2Level, y: "F0");

        //productionUpgrade1CostString
        string productionUpgrade1CostString;
        var productionUpgrade1Cost = 25 * Pow(1.07, productionUpgrade1Level);
        productionUpgrade1CostString = NotationMethod(productionUpgrade1Cost, y: "F0");

        //productionUpgrade1LevelString
        string productionUpgrade1LevelString;
        productionUpgrade1LevelString = NotationMethod(productionUpgrade1Level, y: "F0");

        //productionUpgrade2CostString
        string productionUpgrade2CostString;
        var productionUpgrade2Cost = 250 * Pow(1.07, productionUpgrade2Level);
        productionUpgrade2CostString = NotationMethod(productionUpgrade2Cost, y: "F0");

        //productionUpgrade2LevelString
        string productionUpgrade2LevelString;
        productionUpgrade2LevelString = NotationMethod(productionUpgrade2Level, y: "F0");

        //show text and calculation
        clickUpgrade1Text.text = "Click Upgrade 1\nCost: " + clickUpgrade1CostString + "coins\nPower: +1 Click\nLevel: " + clickUpgrade1LevelString;
       
        clickUpgrade2Text.text = "Click Upgrade 2\nCost: " + clickUpgrade2CostString + "coins\nPower: +5 Click\nLevel: " + clickUpgrade2LevelString;

        productionUpgrade1Text.text = "Production Upgrade 1\nCost: " + productionUpgrade1CostString + "coins\nPower: +" + gemboost.ToString("F2") + " coins/s\nLevel: " + productionUpgrade1LevelString;
        productionUpgrade2Text.text = "Production Upgrade 2\nCost: " + productionUpgrade2CostString+ "coins\nPower: +" + (productionUpgrade2Power * gemboost).ToString("F2") + " coins /s\nLevel: " + productionUpgrade2LevelString;

        coins += coinsPerSecond * Time.deltaTime;

        //clickupgrade1bar
        if (coins / clickUpgrade1Cost < 0.01)
        {
            clickUpgrade1Bar.fillAmount = 0;
        }
        else if(coins/clickUpgrade1Cost > 10)
        {
            clickUpgrade1Bar.fillAmount = 1;
        }
        else
        {
        clickUpgrade1Bar.fillAmount = (float)(coins / clickUpgrade1Cost).ToDouble();
        }

        //clickupgrade2bar
        if (coins / clickUpgrade2Cost < 0.01)
        {
            clickUpgrade2Bar.fillAmount = 0;
        }
        else if (coins / clickUpgrade2Cost > 10)
        {
            clickUpgrade2Bar.fillAmount = 1;
        }
        else
        {
            clickUpgrade2Bar.fillAmount = (float)(coins / clickUpgrade2Cost).ToDouble();
        }

        //productionupgrade1bar
        if (coins / productionUpgrade1Cost < 0.01)
        {
            productionUpgrade1Bar.fillAmount = 0;
        }
        else if (coins / productionUpgrade1Cost > 10)
        {
            productionUpgrade1Bar.fillAmount = 1;
        }
        else
        {
            productionUpgrade1Bar.fillAmount = (float)(coins / productionUpgrade1Cost).ToDouble();
        }

        //productionupgrade2bar
        if (coins / productionUpgrade2Cost < 0.01)
        {
            productionUpgrade2Bar.fillAmount = 0;
        }
        else if (coins / productionUpgrade2Cost > 10)
        {
            productionUpgrade2Bar.fillAmount = 1;
        }
        else
        {
            productionUpgrade2Bar.fillAmount = (float)(coins / productionUpgrade2Cost).ToDouble();
        }

        clickUpgrade1MaxText.text = "Buy Max (" + BuyClickUpgrade1MaxCount() + ")";
        clickUpgrade2MaxText.text = "Buy Max (" + BuyClickUpgrade2MaxCount() + ")";
        productionUpgrade1MaxText.text = "Buy Max (" + BuyProductionUpgrade1MaxCount() + ")";
        productionUpgrade2MaxText.text = "Buy Max (" + BuyProductionUpgrade2MaxCount() + ")";
        
        //save here to keep all the data from above.10/19/2020 MW
        Save();
    }

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
        if(coins > 1000)
        {
            coins = 0;
            coinsClickValue = 1;
            productionUpgrade2Power = 5;

            clickUpgrade1Level = 0;
            clickUpgrade2Level = 0;
            productionUpgrade1Level = 0;
            productionUpgrade2Level = 0;

            gems += gemsToGet;
        }
    }


    //Buttons
    public void Click()
    {
        coins += coinsClickValue;
    }

    //ClickUpgrade1
    public void BuyClickUpgrade1()
    {
        var cu1cost = 10 * Pow(1.07, clickUpgrade1Level);
        if(coins >= cu1cost)
        {
        clickUpgrade1Level++;
        coins -= cu1cost;
        cu1cost *= 1.07;
        coinsClickValue++;
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
        var cu1c = coins;
        var cu1r = 1.07;
        var cu1k = clickUpgrade1Level;
        var cu1n = Floor(Log(cu1c * (cu1r - 1) / (cu1b * Pow(cu1r, cu1k)) + 1, cu1r));

        var cu1cost = cu1b * ((Pow(cu1r, cu1k) * (Pow(cu1r, cu1n) - 1)) / (cu1r - 1));
        print("culcost should be 10 but its" + cu1cost );
        if(coins >= cu1cost)
        {
            print("BuyClickUpgrade1Max() if statement");
            clickUpgrade1Level += cu1n;
            coins -= cu1cost;
            coinsClickValue += cu1n;
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
        var cu1c = coins;
        var cu1r = 1.07;
        var cu1k = clickUpgrade1Level;
        var cu1n = Floor(Log(cu1c * (cu1r - 1) / (cu1b * Pow(cu1r, cu1k)) + 1, cu1r));
        return cu1n;
    }

    //ClickUpgrade2
    public void BuyClickUpgrade2()
    {
        var cu2cost = 100 * Pow(1.09, clickUpgrade2Level);
        if (coins >= cu2cost)
        {
            clickUpgrade2Level++;
            coins -= cu2cost;
            cu2cost *= 1.09;
            coinsClickValue += 5;
        }
    }

    public void BuyClickUpgrade2Max()
    {
        var cu2b = 100;
        var cu2c = coins;
        var cu2r = 1.09;
        var cu2k = clickUpgrade2Level;
        var cu2n = Floor(Log(cu2c * (cu2r - 1) / (cu2b * Pow(cu2r, cu2k)) + 1, cu2r));

        var cu2cost = cu2b * ((Pow(cu2r, cu2k) * (Pow(cu2r, cu2n) - 1)) / (cu2r - 1));

        if (coins >= cu2cost)
        {
            clickUpgrade2Level += cu2n;
            coins -= cu2cost;
            coinsClickValue += cu2n;
        }
    }

    public BigDouble BuyClickUpgrade2MaxCount()
    {
        var cu2b = 100;
        var cu2c = coins;
        var cu2r = 1.09;
        var cu2k = clickUpgrade2Level;
        var cu2n = Floor(Log(cu2c * (cu2r - 1) / (cu2b * Pow(cu2r, cu2k)) + 1, cu2r));
        return cu2n;
    }





    //ProductionUpgrade1
    public void BuyProductionUpgrade1()
    {
        var pu1cost = 25 * Pow(1.07, productionUpgrade1Level);
        if (coins >= pu1cost)
        {
            productionUpgrade1Level++;
            coins -= pu1cost;
            pu1cost *= 1.07;
            pu1cost++;
        }
    }
    public void BuyProductionUpgrade1Max()
    {
        var pu1b = 25;
        var pu1c = coins;
        var pu1r = 1.07;
        var pu1k = productionUpgrade1Level;
        var pu1n = Floor(Log(pu1c * (pu1r - 1) / (pu1b * Pow(pu1r, pu1k)) + 1, pu1r));

        var pu1cost = pu1b * ((Pow(pu1r, pu1k) * (Pow(pu1r, pu1n) - 1)) / (pu1r - 1));

        if (coins >= pu1cost)
        {
            productionUpgrade1Level += pu1n;
            coins -= pu1cost;
            coinsClickValue += pu1n;
        }
    }

    public BigDouble BuyProductionUpgrade1MaxCount()
    {
        var pu1b = 25;
        var pu1c = coins;
        var pu1r = 1.07;
        var pu1k = productionUpgrade1Level;
        var pu1n = Floor(Log(pu1c * (pu1r - 1) / (pu1b * Pow(pu1r, pu1k)) + 1, pu1r));
        return pu1n;
    }

    //ProductionUpgrade2
    public void BuyProductionUpgrade2()
    {
        var pu2cost = 250 * Pow(1.07, productionUpgrade2Level);
        if (coins >= pu2cost)
        {
            productionUpgrade2Level++;
            coins -= pu2cost;
            pu2cost *= 1.07;
            pu2cost++;
        }
    }

    public void BuyProductionUpgrade2Max()
    {
        var pu2b = 250;
        var pu2c = coins;
        var pu2r = 1.07;
        var pu2k = productionUpgrade2Level;
        var pu2n = Floor(Log(pu2c * (pu2r - 1) / (pu2b * Pow(pu2r, pu2k)) + 1, pu2r));

        var pu2cost = pu2b * ((Pow(pu2r, pu2k) * (Pow(pu2r, pu2n) - 1)) / (pu2r - 1));

        if (coins >= pu2cost)
        {
            productionUpgrade2Level += pu2n;
            coins -= pu2cost;
            coinsClickValue += pu2n;
        }
    }

    public BigDouble BuyProductionUpgrade2MaxCount()
    {
        var pu2b = 250;
        var pu2c = coins;
        var pu2r = 1.07;
        var pu2k = productionUpgrade2Level;
        var pu2n = Floor(Log(pu2c * (pu2r - 1) / (pu2b * Pow(pu2r, pu2k)) + 1, pu2r));
        return pu2n;
    }

    public void ChangeTabs(string id)
    {
        switch (id)
        {
            case "upgrades":
                CanvasGroupChanger(false, mainMenuGroup);
                CanvasGroupChanger(true, upgradesGroup);
                break;
            case "main":
                CanvasGroupChanger(true, mainMenuGroup);
                CanvasGroupChanger(false, upgradesGroup);
                break;
        }
    }

    public void FullReset()
    {
        coins = 0;
        coinsClickValue = 1;
        productionUpgrade2Power = 5;
        gems = 0;
        clickUpgrade1Level = 0;
        clickUpgrade2Level = 0;
        productionUpgrade1Level = 0;
        productionUpgrade2Level = 0;
    }
}
