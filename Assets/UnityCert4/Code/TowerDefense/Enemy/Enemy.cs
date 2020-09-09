using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Enemy : MonoBehaviour
    {
        [Header("General Stats")]
        [SerializeField, Tooltip("How fast the enemy will move within")] 
        float speed = 1;
        [SerializeField, Tooltip("HP")] 
        float health = 1;
        [SerializeField, Tooltip("Enemy damage")] 
        float damage = 1;
        [SerializeField, Tooltip("How big is the enemy visually")] 
        float size = 1;
        // RESISTANCE HERE

        [Header("Rewards")]
        [SerializeField, Tooltip("The amount of experience the killing tower will gain from killing")] 
        float xp = 1;
        [SerializeField, Tooltip("The amount of money that the player will gain upon killing the enemy")] 
        int money = 1;

        TowerDefensePlayer player;

        

        void Start()
        {
            player = TowerDefensePlayer.instance;
        }

        /// <summary>
        /// Handles damage of the enemy andif below or equal to 0, calls Die
        /// </summary>
        /// <param name="tower"> The tower doing the damage to the enemy </param>
        public void Damage(Tower tower)
        {
            health -= tower.Damage;
            if (health <= 0)
            {
                Die(tower);
            }

        }

        /// <summary>
        /// Handles the visual and technical features of dying, such as giving the tower experience.
        /// </summary>
        /// <param name="tower">The tower that killed the enemy</param>
        void Die(Tower tower)
        {
            tower.AddExperience(xp);
            player.AddMoney(money);
        }


    }
}