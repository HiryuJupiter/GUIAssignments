using UnityEngine;
using System.Collections;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        [Header("General Stats")]
        [SerializeField] string towerName;
        [SerializeField] string description;
        [SerializeField] int cost;

        [Header("Attack Stats")]
        [SerializeField, Min(0.1f)] float damage = 1;
        [SerializeField, Min(0.1f)] float minimumRange = 1;
        [SerializeField] float maximumRange = 5;
        [SerializeField, Min(0.1f)] float fireRate = 0.1f;

        [Header("Experience Stats")]
        [SerializeField, Range(2, 5)] private int maxLevel = 3;
        [SerializeField, Min(1)] private float baseRequiredXp = 5;
        [SerializeField, Min(1)] private float experienceScaler = 1;

        int level = 1;
        float xp = 0;
        Enemy target = null;

        float firingTimer = 0f;

        #region Properties 
        //public accessors to private variables
        public string TowerName => towerName;
        public int Cost => cost;
        //Public accessors that calculates the return value based on level and scaler.
        public float RequiredXP => baseRequiredXp * (level - 1) * experienceScaler;
        public float MaximumRange => maximumRange * (level * 0.5f + 0.5f);
        public float Damage => damage * (level * 0.5f + 0.5f);
        
        #endregion


#if UNITY_EDITOR
        //OnValidate runs whenever a variable is chagned within the inspector
        private void OnValidate()
        {
            maximumRange = Mathf.Clamp(maximumRange, minimumRange + 1, float.MaxValue);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0, .25f);
            Gizmos.DrawSphere(transform.position, minimumRange);

            Gizmos.color = new Color(0, 0, 1, 0.25f);
            Gizmos.DrawSphere(transform.position, maximumRange);
        }
#endif

        public void AddExperience(float xp)
        {
            this.xp += xp;
            if (level < maxLevel)
            {
                if (this.xp >= RequiredXP)
                {
                    LevelUp();
                }
            }
        }

        void LevelUp()
        {
            level++;
            xp = 0;
        }

        void Fire()
        {
            if (target != null)
            {
                target.Damage(this);
            }
        }


        void FireWhenReady()
        {
            if (target != null)
            {
                if (firingTimer < fireRate)
                {
                    firingTimer += Time.deltaTime;
                }
                else
                {
                    firingTimer = 0;
                    Fire();
                }
            }
        }

        void Target()
        {
            //Get enemies within range

            //Call get closest enemy

        }

        Enemy GetClosestEnemy(Enemy[] enemies)
        {
            float closestDist = float.MaxValue;
            Enemy closest = null;

            foreach (Enemy enemy in enemies)
            {
                float dist = Vector3.Distance(enemy.transform.position, transform.position);

            }

            return closest;
        }

        /// <summary>
        /// Gets a formatted string containing all fo the description, tower prop, nme, cost to be displayed onthe UI
        /// </summary>
        /// <returns></returns>
        public string GetDisplayText
        {
            get
            {
                string display = string.Format("Name: {0} Cost: {1}\n", towerName, cost.ToString());
                display += description + "\n";
                display += string.Format("Min Range: {0}, Max Range: {1}, Damage: {2}", minimumRange.ToString(), damage.ToString());
                return display;
            }            
        }

    }
}