﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonState
{
    ACTIVE,
    DISABLED,
    TRANSITION
}


public class BuildTowerGUI : MonoBehaviour
{

    [SerializeField] GameObject buildButtons;
    [SerializeField] GameObject upgradeButtonsBasic;
    [SerializeField] GameObject upgradeButtonsAdvanced;
    [SerializeField] GameObject sliderTowerPrefab;

    public ButtonState stateBuild = ButtonState.DISABLED;
    public ButtonState stateUpgrades = ButtonState.DISABLED;

    // Start is called before the first frame update
    void Start()
    {
        buildButtons.SetActive(false);
        upgradeButtonsBasic.SetActive(false);
    }

    public IEnumerator SetBuildButtons(Vector3 worldPos)
    {
        if (stateBuild != ButtonState.DISABLED) yield break;

        stateBuild = ButtonState.TRANSITION;
        buildButtons.SetActive(true);
        Vector3 canvasPos = Camera.main.WorldToScreenPoint(worldPos);
        buildButtons.transform.position = canvasPos;
        buildButtons.GetComponent<Animator>().SetBool("show", true);
        yield return new WaitForSeconds(0.5f);

        stateBuild = ButtonState.ACTIVE;
    }

    public IEnumerator DisableBuildButtons()
    {
        if (stateBuild != ButtonState.ACTIVE) yield break;

        stateBuild = ButtonState.TRANSITION;
        buildButtons.GetComponent<Animator>().SetBool("show", false);

        yield return new WaitForSeconds(0.5f);
        buildButtons.SetActive(false);
        stateBuild = ButtonState.DISABLED;
    }

    public IEnumerator SetUpgradeButtons(Tower tower)
    {
        if (stateUpgrades != ButtonState.DISABLED) yield break;
        stateUpgrades = ButtonState.TRANSITION;
        Vector3 canvasPos = Camera.main.WorldToScreenPoint(tower.transform.position);
        if (tower.state == TowerState.LEVEL_3)
        {
            upgradeButtonsAdvanced.SetActive(true);
            upgradeButtonsAdvanced.transform.position = canvasPos;
            upgradeButtonsAdvanced.GetComponent<Animator>().SetBool("show", true);
            SetLevel4Prices(tower);
        }
        else
        {
            ShowSellButton(canvasPos , tower);
            upgradeButtonsBasic.GetComponent<Animator>().SetBool("show", true);
        }
        SetPrices(tower, tower.state);
        yield return new WaitForSeconds(0.5f);
        stateUpgrades = ButtonState.ACTIVE;
    }

    void SetLevel4Prices(Tower tower)
    {
        if(tower is ArrowTower) 
        {
            int priceUpgrade4A = PricesForTowers.ARROW_4A;
            int priceUpgrade4B = PricesForTowers.ARROW_4B;
            int salePrice = (int)(PricesForTowers.ARROW_3 * 0.8f);
            upgradeButtonsAdvanced.GetComponent<UpgradeButtons>().SetPrices(priceUpgrade4A, priceUpgrade4B, salePrice);
        }
        if(tower is CannonTower) 
        {
            int priceUpgrade4A = PricesForTowers.CANNON_4A;
            int priceUpgrade4B = PricesForTowers.CANNON_4B;
            int salePrice = (int)(PricesForTowers.CANNON_3 * 0.8f);
            upgradeButtonsAdvanced.GetComponent<UpgradeButtons>().SetPrices(priceUpgrade4A, priceUpgrade4B, salePrice);
        }
        if(tower is MagicTower) 
        {
            int priceUpgrade4A = PricesForTowers.CANNON_4A;
            int priceUpgrade4B = PricesForTowers.CANNON_4B;
            int salePrice = (int)(PricesForTowers.CANNON_3 * 0.8f);
            upgradeButtonsAdvanced.GetComponent<UpgradeButtons>().SetPrices(priceUpgrade4A, priceUpgrade4B, salePrice);
        }
    }

    void ShowSellButton(Vector3 canvasPos, Tower tower)
    {
            upgradeButtonsBasic.SetActive(true);
            upgradeButtonsBasic.transform.GetChild(0).gameObject.SetActive(false);
        upgradeButtonsBasic.transform.position = canvasPos;
        upgradeButtonsBasic.GetComponent<Animator>().SetBool("show", true);
        SetPriceForSale(tower, tower.state);

    }
    void SetPriceForSale(Tower tower, TowerState state)
    {
        if(tower is ArrowTower)
        {
         int sellPrice = 0;
         if(state == TowerState.LEVEL_4A) sellPrice = (int)(PricesForTowers.ARROW_4A * 0.8f);
         if(state == TowerState.LEVEL_4A) sellPrice = (int)(PricesForTowers.ARROW_4B * 0.8f);
         upgradeButtonsBasic.GetComponent<UpgradeButtons>().SetPrices(sellPrice);
        }
        if(tower is CannonTower)
        {
         int sellPrice = 0;
         if(state == TowerState.LEVEL_4A) sellPrice = (int)(PricesForTowers.CANNON_4A * 0.8f);
         if(state == TowerState.LEVEL_4A) sellPrice = (int)(PricesForTowers.CANNON_4B * 0.8f);
         upgradeButtonsBasic.GetComponent<UpgradeButtons>().SetPrices(sellPrice);
        }
        if(tower is MagicTower)
        {
         int sellPrice = 0;
         if (state == TowerState.LEVEL_4A) sellPrice = (int)(PricesForTowers.MAGIC_4A * 0.8f);
         if (state == TowerState.LEVEL_4A) sellPrice = (int)(PricesForTowers.MAGIC_4B * 0.8f);
         upgradeButtonsBasic.GetComponent<UpgradeButtons>().SetPrices(sellPrice);

        }
    }
    void SetPrices(Tower tower,TowerState state)
    {
        if(tower is ArrowTower)
        {
        int priceForUpgrades = 0;
        int priceForSale = 0;
        if (state == TowerState.LEVEL_1)
        {
            priceForUpgrades = PricesForTowers.ARROW_2;
            priceForSale = (int)(PricesForTowers.ARROW_1 * 0.8f);
        }
        else if (state == TowerState.LEVEL_2)
        {
            priceForUpgrades = PricesForTowers.ARROW_3;
            priceForSale =(int)(PricesForTowers.ARROW_2 * 0.8f) ;
        }
        upgradeButtonsBasic.GetComponent<UpgradeButtons>().SetPrices(priceForUpgrades, priceForSale);

        }
        if(tower is CannonTower)
        {
          int priceForUpgrades = 0;
          int priceForSale = 0;
          if (state == TowerState.LEVEL_1)
          {
            priceForUpgrades = PricesForTowers.CANNON_2;
            priceForSale = (int)(PricesForTowers.CANNON_1 * 0.8f);
          }
        else if (state == TowerState.LEVEL_2)
        {
            priceForUpgrades = PricesForTowers.CANNON_3;
            priceForSale =(int)(PricesForTowers.CANNON_2 * 0.8f) ;
        }
        upgradeButtonsBasic.GetComponent<UpgradeButtons>().SetPrices(priceForUpgrades, priceForSale);
        }
        if(tower is MagicTower)
        {
            int priceForUpgrades = 0;
            int priceForSale = 0;
            if (state == TowerState.LEVEL_1)
            {
                priceForUpgrades = PricesForTowers.MAGIC_2;
                priceForSale = (int)(PricesForTowers.MAGIC_1 * 0.8f);
            }
            else if (state == TowerState.LEVEL_2)
            {
                priceForUpgrades = PricesForTowers.MAGIC_3;
                priceForSale = (int)(PricesForTowers.MAGIC_2 * 0.8f);
            }
            upgradeButtonsBasic.GetComponent<UpgradeButtons>().SetPrices(priceForUpgrades, priceForSale);

        }
    }
    public IEnumerator DisableUpgradeButtons()
    {
        if (stateUpgrades != ButtonState.ACTIVE) yield break;
        if(upgradeButtonsBasic.activeSelf == true)
        {
            upgradeButtonsBasic.GetComponent<Animator>().SetBool("show", false);
        }
        else
        {
            upgradeButtonsAdvanced.GetComponent<Animator>().SetBool("show", false);
        }
        stateUpgrades = ButtonState.TRANSITION;

        yield return new WaitForSeconds(0.5f);
        stateUpgrades = ButtonState.DISABLED;
        upgradeButtonsBasic.SetActive(false);
        upgradeButtonsAdvanced.SetActive(false);
    }


    public void CreateTowerSlider(Transform tower)
    {
        GameObject cloneSlider = Instantiate(sliderTowerPrefab, transform);
        cloneSlider.GetComponent<SliderForTower>().SetTower(tower);
    }

   
}
