using UnityEngine;
using System.Collections;

namespace TowerDefense
{
    public class TowerDefensePlayer : MonoBehaviour
    {
        /// <summary>
        /// The reference to the only player gameobject in the game
        /// </summary>
        //singleton
        public static TowerDefensePlayer instance = null;

        [SerializeField, Tooltip("Initial money the player has")]
        int money = 100;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                DestroyImmediate(gameObject);
                return;
            }
        }

        void Start()
        {

        }

        void Update()
        {

        }

        public void AddMoney (int amount)
        {
            money += amount;
        }

        /// <summary>
        /// Handles the removal of moeny when purchasing a tower and notifies the TowerManager to place the tower
        /// </summary>
        /// <param name="tower"></param>
        public void PurchaseTower (Tower tower)
        {
            money -= tower.Cost;
        }
    }
}