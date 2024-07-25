using UnityEngine;
public class UpgradeManager : MonoBehaviour
{
    #region  References
    UpgradeTable upgradeTable;
    UpgradeCost upgradeCost;
    SOMoneyData soMoneyData;
    [SerializeField] ServiceAreaController serviceAreaController;
    [SerializeField] Table tableController;
    [SerializeField] KitchenAreaController kitchenAreaController;
    [SerializeField] DishwashingAreaController dishwashingAreaController;
    #endregion
    #region  Upgrade Cost
    long costChefUpgradeCookSpeed;
    long costWaitersUpgradeSpeed;
    long costHireWaiters;
    long costIncreaseTable;
    long costUpgradeChefUpgradeMoveSpeed;
    long costHireChef;
    long costUpgradeMSWashingWorker;
    long costUpgradeWashingDishwasher;
    long costUpgradeDishwasherRoom;
    #endregion

    void Awake()
    {
        upgradeTable = GetComponent<UpgradeTable>();
        upgradeCost = GetComponent<UpgradeCost>();
        soMoneyData = GetMoneyData();
    }
    private SOMoneyData GetMoneyData()
    {
        return Resources.Load<SOMoneyData>("Datas/MoneyData");
    }
    void Start()
    {
        SendCostText();
    }
    void SendCostText()
    {
        costWaitersUpgradeSpeed = (long)upgradeCost.CostWaitersUpgradeSpeed[upgradeCost.CostWaitersUpgradeIndex];
        costChefUpgradeCookSpeed = (long)upgradeCost.CostChefCookSpeedUpgrade[upgradeCost.CostChefCookSpeedIndex];
        costUpgradeMSWashingWorker = (long)upgradeCost.CostValueUpgradeSpeedWashingWorker[upgradeCost.CostValueUpgradeSpeedWashingWorkerIndex];
        costUpgradeWashingDishwasher = (long)upgradeCost.CostValueUpgradeSpeedWashingDishWasher[upgradeCost.CostValueUpgradeSpeedWashingDishWasherIndex];
        costHireWaiters = (long)upgradeCost.CostHiringWaiters[upgradeCost.CostHiringWaitersIndex];
        costIncreaseTable = (long)upgradeCost.CostIncreaseTable[upgradeCost.CostIncreaseTableIndex];
        costUpgradeChefUpgradeMoveSpeed = (long)upgradeCost.CostValuesUpgradeChefMoveSpeed[upgradeCost.CostUpgradeCMoveSpeedIndex];
        costHireChef = (long)upgradeCost.CostHireChef[upgradeCost.CostHireChefIndex];
        costUpgradeDishwasherRoom = (long)upgradeCost.CostUpgradeDishwasherRoom[upgradeCost.CostUpgradeDishwasherRoomIndex];

        CoreUISignals.Instance.onUpgradeCostText?.Invoke(0, costWaitersUpgradeSpeed);
        CoreUISignals.Instance.onUpgradeCostText?.Invoke(1, costHireWaiters);
        CoreUISignals.Instance.onUpgradeCostText?.Invoke(2, costIncreaseTable);
        CoreUISignals.Instance.onUpgradeCostText?.Invoke(3, costChefUpgradeCookSpeed);
        CoreUISignals.Instance.onUpgradeCostText?.Invoke(4, costUpgradeChefUpgradeMoveSpeed);
        CoreUISignals.Instance.onUpgradeCostText?.Invoke(5, costHireChef);
        CoreUISignals.Instance.onUpgradeCostText?.Invoke(6, costUpgradeMSWashingWorker);
        CoreUISignals.Instance.onUpgradeCostText?.Invoke(7, costUpgradeWashingDishwasher);
        CoreUISignals.Instance.onUpgradeCostText?.Invoke(8, costUpgradeDishwasherRoom);

    }

    #region  Upgrade Button Methods
    public void UpgradeWaitersSpeed() // text list 0 yapıldı sorun yok
    {
        if (!upgradeCost.CostWaitersUpgradeSpeed.ContainsKey(upgradeCost.CostWaitersUpgradeIndex)) return;
        costWaitersUpgradeSpeed = (long)upgradeCost.CostWaitersUpgradeSpeed[upgradeCost.CostWaitersUpgradeIndex];
        if (soMoneyData.moneyData.booCoin >= costWaitersUpgradeSpeed)
        {
            if (upgradeTable.IndexWaitersUpgradeSpeed < upgradeTable.valueWaitersUpgradeSpeed.Count - 1)
            {
                soMoneyData.moneyData.booCoin -= costWaitersUpgradeSpeed;
                float upgradeValue = upgradeTable.valueWaitersUpgradeSpeed[++upgradeTable.IndexWaitersUpgradeSpeed];
                CoreGameSignals.Instance.onDataUpgradeWaitersSpeed?.Invoke(upgradeValue);
                upgradeCost.CostWaitersUpgradeIndex++;
                CoreUISignals.Instance.onUpgradeCostText?.Invoke(0, (long)upgradeCost.CostWaitersUpgradeSpeed[upgradeCost.CostWaitersUpgradeIndex]);
            }
            else
            {
                Debug.Log("ValueZ is at maximum level!");
                CoreUISignals.Instance.onButtonTexts?.Invoke(0);
            }
        }
        else
        {
            Debug.Log("Not enough BooCoins!");
        }
    }
    public void UpgradeChefCookSpeed()  // textlist 3 sroun yok
    {
        if (!upgradeCost.CostChefCookSpeedUpgrade.ContainsKey(upgradeCost.CostChefCookSpeedIndex)) return;
        costChefUpgradeCookSpeed = (long)upgradeCost.CostChefCookSpeedUpgrade[upgradeCost.CostChefCookSpeedIndex];
        if (soMoneyData.moneyData.booCoin >= costChefUpgradeCookSpeed)
        {
            if (upgradeTable.IndexChefCookSpeedUpgrade < upgradeTable.valueChefCookSpeedUpgrade.Count)
            {
                soMoneyData.moneyData.booCoin -= costChefUpgradeCookSpeed;
                float upgradeValue = upgradeTable.valueChefCookSpeedUpgrade[++upgradeTable.IndexChefCookSpeedUpgrade];
                CoreGameSignals.Instance.onDataUpgradeChefCookSpeed?.Invoke(upgradeValue);
                upgradeCost.CostChefCookSpeedIndex++;
                CoreUISignals.Instance.onUpgradeCostText?.Invoke(3, (long)upgradeCost.CostChefCookSpeedUpgrade[upgradeCost.CostChefCookSpeedIndex]);
            }
            else
            {
                Debug.Log("ValueX is at maximum level!");
                CoreUISignals.Instance.onButtonTexts?.Invoke(3);
            }
        }
        else
        {
            Debug.Log("Not enough BooCoins!");


        }
    }
    public void UpgradeChefHire() // text list 5 sorun var
    {
        if (!upgradeCost.CostHireChef.ContainsKey(upgradeCost.CostHireChefIndex)) return;
        costHireChef = (long)upgradeCost.CostHireChef[upgradeCost.CostHireChefIndex];
        if (soMoneyData.moneyData.booCoin >= costHireChef)
        {
            if (upgradeTable.ValueHireChefIndex < upgradeTable.ValueHireChef.Count-1)
            {
                upgradeCost.CostHireChefIndex++;
                upgradeTable.ValueHireChefIndex++;
                soMoneyData.moneyData.booCoin -= costHireChef;
                kitchenAreaController.SpawnCooktop();
                kitchenAreaController.SpawnCookWorker();
               CoreUISignals.Instance.onUpgradeCostText?.Invoke(5, (long)upgradeCost.CostHireChef[upgradeCost.CostHireChefIndex]);
            }
            else
            {
                Debug.Log("ValueX is at maximum level!");
                CoreUISignals.Instance.onButtonTexts?.Invoke(5);
            }
        }
        else
        {
            Debug.Log("Not enough BooCoins!");


        }
    }
    public void UpgradeWashingWorkerSpeed() // text list 6
    {
        if (!upgradeCost.CostValueUpgradeSpeedWashingWorker.ContainsKey(upgradeCost.CostValueUpgradeSpeedWashingWorkerIndex)) return;
        costUpgradeMSWashingWorker = (long)upgradeCost.CostValueUpgradeSpeedWashingWorker[upgradeCost.CostValueUpgradeSpeedWashingWorkerIndex];
        if (soMoneyData.moneyData.booCoin >= costUpgradeMSWashingWorker)
        {
            if (upgradeTable.ValueUpgradeSpeedWashingWorkerIndex < upgradeTable.ValueUpgradeSpeedWashingWorker.Count - 1)
            {
                soMoneyData.moneyData.booCoin -= costUpgradeMSWashingWorker;
                float upgradeValue = upgradeTable.ValueUpgradeSpeedWashingWorker[++upgradeTable.ValueUpgradeSpeedWashingWorkerIndex];
                CoreGameSignals.Instance.onDataUpgradeWashingWorkerSpeed?.Invoke(upgradeValue);
                upgradeCost.CostValueUpgradeSpeedWashingWorkerIndex++;
                CoreUISignals.Instance.onUpgradeCostText?.Invoke(6, (long)upgradeCost.CostValueUpgradeSpeedWashingWorker[upgradeCost.CostValueUpgradeSpeedWashingWorkerIndex]);
            }
            else
            {
                Debug.Log("ValueX is at maximum level!");
                CoreUISignals.Instance.onButtonTexts?.Invoke(6);
            }
        }
        else
        {
            Debug.Log("Not enough BooCoins!");
        }
    }
    public void UpgradeWashingDishwasherSpeed() // text list 7
    {
        if (!upgradeCost.CostValueUpgradeSpeedWashingDishWasher.ContainsKey(upgradeCost.CostValueUpgradeSpeedWashingDishWasherIndex)) return;

        costUpgradeWashingDishwasher = (long)upgradeCost.CostValueUpgradeSpeedWashingDishWasher[upgradeCost.CostValueUpgradeSpeedWashingDishWasherIndex];

        if (soMoneyData.moneyData.booCoin >= costUpgradeWashingDishwasher)
        {
            if (upgradeTable.ValueUpgradeSpeedWashingDishWashIndex < upgradeTable.ValueUpgradeSpeedWashingDishWash.Count - 1)
            {
                soMoneyData.moneyData.booCoin -= costUpgradeWashingDishwasher;
                float upgradeValue = upgradeTable.ValueUpgradeSpeedWashingDishWash[++upgradeTable.ValueUpgradeSpeedWashingDishWashIndex];
                CoreGameSignals.Instance.onDataUpgradeWashSpeed?.Invoke(upgradeValue);
                upgradeCost.CostValueUpgradeSpeedWashingDishWasherIndex++;
                CoreUISignals.Instance.onUpgradeCostText?.Invoke(7, (long)upgradeCost.CostValueUpgradeSpeedWashingDishWasher[upgradeCost.CostValueUpgradeSpeedWashingDishWasherIndex]);
            }
            else
            {
                Debug.Log("ValueZ is at maximum level!");
                CoreUISignals.Instance.onButtonTexts?.Invoke(7);
            }
        }
        else
        {
            Debug.Log("Not enough BooCoins!");

        }
    }
  public void UpgradeDishwasherRoom() // text list 8 sorun var
{
    if (!upgradeCost.CostUpgradeDishwasherRoom.ContainsKey(upgradeCost.CostUpgradeDishwasherRoomIndex))
    {
        Debug.LogError($"Anahtar {upgradeCost.CostUpgradeDishwasherRoomIndex} CostUpgradeDishwasherRoom sözlüğünde bulunamadı.");
        return;
    }

    costUpgradeDishwasherRoom = (long)upgradeCost.CostUpgradeDishwasherRoom[upgradeCost.CostUpgradeDishwasherRoomIndex];

    if (soMoneyData.moneyData.booCoin >= costUpgradeDishwasherRoom)
    {
        if (upgradeTable.ValueUpgradeDishwasherRoomIndex < upgradeTable.ValueUpgradeDishwasherRoom.Count - 1)
        {
            soMoneyData.moneyData.booCoin -= costUpgradeDishwasherRoom;
            dishwashingAreaController.SpawnDishwashingMachine();
            dishwashingAreaController.SpawnWashingWorker();
            upgradeCost.CostUpgradeDishwasherRoomIndex++;
            if (!upgradeCost.CostUpgradeDishwasherRoom.ContainsKey(upgradeCost.CostUpgradeDishwasherRoomIndex))
            {
                Debug.LogError($"Anahtar {upgradeCost.CostUpgradeDishwasherRoomIndex} CostUpgradeDishwasherRoom sözlüğünde bulunamadı.");
                return;
            }
            CoreUISignals.Instance.onUpgradeCostText?.Invoke(8, (long)upgradeCost.CostUpgradeDishwasherRoom[upgradeCost.CostUpgradeDishwasherRoomIndex]);
        }
        else
        {
            Debug.Log("ValueZ is at maximum level!");
            CoreUISignals.Instance.onButtonTexts?.Invoke(8);
        }
    }
    else
    {
        Debug.Log("Not enough BooCoins!");
    }
}

    public void UpgradeWaitersHire() // text list 1 sorun var 
    {
        if (!upgradeCost.CostHiringWaiters.ContainsKey(upgradeCost.CostHiringWaitersIndex)) return;
        costHireWaiters = (long)upgradeCost.CostHiringWaiters[upgradeCost.CostHiringWaitersIndex];
        if (soMoneyData.moneyData.booCoin >= costHireWaiters)
        {
            if (upgradeTable.ValueHiringWaitersIndex < upgradeTable.ValueHiringWaiters.Count - 1)
            {
                soMoneyData.moneyData.booCoin -= costHireWaiters;
                float upgradeValue = upgradeTable.ValueHiringWaiters[++upgradeTable.ValueHiringWaitersIndex];
                serviceAreaController.SpawnServiceWorker();
                serviceAreaController.SpawnTable();
                upgradeCost.CostHiringWaitersIndex++;
                CoreUISignals.Instance.onUpgradeCostText?.Invoke(1, (long)upgradeCost.CostHiringWaiters[upgradeCost.CostHiringWaitersIndex]);
            }
            else
            {
                Debug.Log("ValueZ is at maximum level!");
                CoreUISignals.Instance.onButtonTexts?.Invoke(1);
            }
        }
        else
        {
            Debug.Log("Not enough BooCoins!");

        }
    }
    public void IncreaseTableSize() // text list 2 tabledan referans alamıyoruz 
    {
        if (!upgradeCost.CostIncreaseTable.ContainsKey(upgradeCost.CostIncreaseTableIndex)) return;
        costIncreaseTable = (long)upgradeCost.CostIncreaseTable[upgradeCost.CostIncreaseTableIndex];

        if (soMoneyData.moneyData.booCoin >= costIncreaseTable)
        {
            if (upgradeTable.valueIncreaseTableIndex < upgradeTable.valueIncreaseTable.Count - 1)
            {
                soMoneyData.moneyData.booCoin -= costIncreaseTable;
                tableController.AddSeat();
                upgradeCost.CostIncreaseTableIndex++;
                CoreUISignals.Instance.onUpgradeCostText?.Invoke(2, (long)upgradeCost.CostIncreaseTable[upgradeCost.CostIncreaseTableIndex]);
                if (tableController == null)
                {
                    Debug.Log("lounge da table yok");
                }
            }
            else
            {
                Debug.Log("ValueA is at maximum level!");
                CoreUISignals.Instance.onButtonTexts?.Invoke(2);
            }
        }
        else
        {
            Debug.Log("Not enough BooCoins!");
            // sinyal
        }
    }
    public void UpgradeChefMovementSpeed() // text list 4 sorun yok
    {
        if (!upgradeCost.CostValuesUpgradeChefMoveSpeed.ContainsKey(upgradeCost.CostUpgradeCMoveSpeedIndex)) return;
        costUpgradeChefUpgradeMoveSpeed = (long)upgradeCost.CostValuesUpgradeChefMoveSpeed[upgradeCost.CostUpgradeCMoveSpeedIndex];

        if (soMoneyData.moneyData.booCoin >= costUpgradeChefUpgradeMoveSpeed)
        {
            if (upgradeTable.valueUpgradeChefMoveSpeedIndex < upgradeTable.valuesUpgradeChefMoveSpeed.Count - 1)
            {
                soMoneyData.moneyData.booCoin -= costUpgradeChefUpgradeMoveSpeed;
                float upgradeValue = upgradeTable.valuesUpgradeChefMoveSpeed[++upgradeTable.valueUpgradeChefMoveSpeedIndex];
                CoreGameSignals.Instance.onDataUpgradeChefMovementSpeed?.Invoke(upgradeValue);
                upgradeCost.CostUpgradeCMoveSpeedIndex++;
                CoreUISignals.Instance.onUpgradeCostText?.Invoke(4, (long)upgradeCost.CostValuesUpgradeChefMoveSpeed[upgradeCost.CostUpgradeCMoveSpeedIndex]);
            }
            else
            {
                Debug.Log("ValueB is at maximum level!");
                CoreUISignals.Instance.onButtonTexts?.Invoke(4);
            }
        }
        else
        {
            Debug.Log("Not enough BooCoins!");
        }
    }
    #endregion
}
