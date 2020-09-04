using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
    [SerializeField] int x = 0;

    string TowerName;
    string Description;
    int Cost;

    int level = 1;
    int baseRequredXP = 200;
    int experienceScaler = 2;

    int damage = 5;

    float minimumRange = 10f;
    float maximumRange = 10f;

    [Header("General Stats")]
    [SerializeField]
    string towerName = "";
    [SerializeField]
    string description = "";

    void Start()
    {

    }

    string GetDisplayText()
    {
        string display = string.Format("Name: {0} Cost: {1}\n", TowerName, Cost.ToString());
        display += Description + "\n";
        display += string.Format("Min Range: {0}, Max Range: {1}, Damage: {2}", minimumRange.ToString(), damage.ToString());
        return display;
    }

    float RequiredXP => baseRequredXP * (level - 1) * experienceScaler;

    float MaximumRange => maximumRange *(level * 0.5f + 0.5f);

    float Damage => damage * (level * 0.5f + 0.5f);
}