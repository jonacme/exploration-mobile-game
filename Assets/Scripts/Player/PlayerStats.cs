using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Level")]
    public int level; // the player Level
    public int currentEXP;
    public int maxEXP;

    [Header("Health Points")]
    public int currentHealth;
    public int maxHealth;

    [Header("Magic Points")]
    public int currentMP;
    public int maxMP;

    [Header("Player Stats")]
    public int strength;
    public int magicStrength;
    //public int endurance;
    //public int agility;
    public int haste;

    [Header("Slider")]
    public Slider healthBar;
    public Slider mpBar;
    public Slider xpBar;

    public Gradient gradient;

    public TextMeshProUGUI healthSliderDisplay;
    public TextMeshProUGUI mpSliderDisplay;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSliderUI()
    {
        healthBar.value = maxHealth;
        healthBar.value = currentHealth;

        mpBar.value = maxMP;
        mpBar.value = currentMP;

        xpBar.value = maxEXP;
        xpBar.value = currentEXP;

        healthSliderDisplay.text = currentHealth + " / " + maxHealth;
        mpSliderDisplay.text = currentMP + " / " + maxMP;
    }
}
