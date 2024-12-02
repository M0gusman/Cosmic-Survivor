using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Make sure to include the UI namespace for working with UI elements

public class LevelUp : MonoBehaviour
{
    [Header("Experience/Level")]
    public int Level = 1;
    public int exp = 0;
    public int xpCap;
    
    [Header("UI Elements")]
    public Slider xpSlider; // Reference to the XP Slider UI element
    public Text levelText;  // Reference to the Text UI element to display the level
    public Text xpText;     // Optional: To show the current XP / XP needed

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int xpCapIncrease;
    }

    public List<LevelRange> levelRanges;

    void Start()
    {
        xpCap = levelRanges[0].xpCapIncrease;
        UpdateUI();
    }

    public void AddXP(int amount)
    {
        exp += amount;
        CheckLevelUp();
    }

    void CheckLevelUp()
    {
        if (exp >= xpCap)
        {
            Level++;
            exp -= xpCap;
            int xpCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (Level >= range.startLevel && Level <= range.endLevel)
                {
                    xpCapIncrease = range.xpCapIncrease;
                    break;
                }
            }
            xpCap += xpCapIncrease;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        // Update the XP slider value based on current XP and xpCap
        if (xpSlider != null)
        {
            xpSlider.value = (float)exp / xpCap;
        }

        // Update the Level text
        if (levelText != null)
        {
            levelText.text = "Level: " + Level.ToString();
        }

        // Optionally, update the XP text
        if (xpText != null)
        {
            xpText.text = exp + " / " + xpCap + " XP";
        }
    }
}