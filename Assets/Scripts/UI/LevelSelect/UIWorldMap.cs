using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldMap : MonoBehaviour
{
    public List<UILevelSelectButton> m_levelSelectButtons = new List<UILevelSelectButton>();

    private void Start()
    {
        if (Constants.DebugRandomStarterLevels)
        {
            Debug.LogWarning("Randomizing all starter card levels");
            
            PlayerAccountData data = PlayerDataManager.PlayerAccountData;

            data.m_starterCardUnlockLevels.Clear();

            GameCard alphaBoarCard = new ContentAlphaBoarCard();
            GameCard lizardSoldierCard = new ContentLizardSoldierCard();
            GameCard undeadMammothCard = new ContentUndeadMammothCard();

            GameCard dwarvenSoldierCard = new ContentDwarvenSoldierCard();
            GameCard sandwalkerCard = new ContentSandwalkerCard();
            GameCard mechanizedBeastCard = new ContentMechanizedBeastCard();

            GameCard aegisCard = new ContentAegisCard();
            GameCard cureWoundsCard = new ContentCureWoundsCard();
            GameCard joltCard = new ContentJoltCard();

            GameCard fireboltCard = new ContentFireboltCard();
            GameCard drainingBoltCard = new ContentDrainingBoltCard();
            GameCard weakeningBoltCard = new ContentWeakeningBoltCard();

            GameCard growTalonsCard = new ContentGrowTalonsCard();
            GameCard staminaTrainingCard = new ContentStaminaTrainingCard();
            GameCard optimizeCard = new ContentOptimizeCard();


            data.m_starterCardUnlockLevels.Add(alphaBoarCard.GetBaseName(), Random.Range(0, 3));
            data.m_starterCardUnlockLevels.Add(lizardSoldierCard.GetBaseName(), Random.Range(0, 3));
            data.m_starterCardUnlockLevels.Add(undeadMammothCard.GetBaseName(), Random.Range(0, 3));

            data.m_starterCardUnlockLevels.Add(dwarvenSoldierCard.GetBaseName(), Random.Range(0, 3));
            data.m_starterCardUnlockLevels.Add(sandwalkerCard.GetBaseName(), Random.Range(0, 3));
            data.m_starterCardUnlockLevels.Add(mechanizedBeastCard.GetBaseName(), Random.Range(0, 3));

            data.m_starterCardUnlockLevels.Add(aegisCard.GetBaseName(), Random.Range(0, 3));
            data.m_starterCardUnlockLevels.Add(cureWoundsCard.GetBaseName(), Random.Range(0, 3));
            data.m_starterCardUnlockLevels.Add(joltCard.GetBaseName(), Random.Range(0, 3));

            data.m_starterCardUnlockLevels.Add(fireboltCard.GetBaseName(), Random.Range(0, 3));
            data.m_starterCardUnlockLevels.Add(drainingBoltCard.GetBaseName(), Random.Range(0, 3));
            data.m_starterCardUnlockLevels.Add(weakeningBoltCard.GetBaseName(), Random.Range(0, 3));

            data.m_starterCardUnlockLevels.Add(growTalonsCard.GetBaseName(), Random.Range(0, 3));
            data.m_starterCardUnlockLevels.Add(staminaTrainingCard.GetBaseName(), Random.Range(0, 3));
            data.m_starterCardUnlockLevels.Add(optimizeCard.GetBaseName(), Random.Range(0, 3));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_levelSelectButtons.Count; i++)
        {
            if (m_levelSelectButtons[i].GetMap() == null)
            {
                continue;
            }

            m_levelSelectButtons[i].gameObject.SetActive(Constants.CheatsOn || PlayerDataManager.GetCurLevel() >= m_levelSelectButtons[i].GetMap().GetPlayerUnlockLevel());
        }
    }
}
