
using System.Collections.Generic;
using UnityEngine;

namespace MyBase
{
    public abstract class EnemyBase : MonoBehaviour, IEnemy, IPrefabs
    {
        private List<string> controlImmunity;
        private int life;
        private float speed;
        private int damage;
        private int immunityCount;
        private int blocks = 1;
        private float rangeFire;
        private float atkSpeed;
        private float weight;
        private float derateAd;
        private float derateIce;
        private float derateFire;
        private float derateElec;
        private float derateWind;
        private float derateEnergy;
        private bool canAction;
        public List<string> ControlImmunityList
        {
            get => controlImmunity;
            set => controlImmunity = value;
        }
        public bool CanAction
        {
            get => canAction;
            set => canAction = value;
        }
        private bool isInit;
        public int Life
        {
            get => life;
            set => life = value;
        }

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public int Damage
        {
            get => damage;
            set => damage = value;
        }

        public int ImmunityCount
        {
            get => immunityCount;
            set => immunityCount = value;
        }

        public int Blocks
        {
            get => blocks;
            set => blocks = value;
        }

        public float RangeFire
        {
            get => rangeFire;
            set => rangeFire = value;
        }

        public float AtkSpeed
        {
            get => atkSpeed;
            set => atkSpeed = value;
        }

        public float Weight
        {
            get => weight;
            set => weight = value;
        }

        public float DerateAd
        {
            get => derateAd;
            set => derateAd = value;
        }

        public float DerateIce
        {
            get => derateIce;
            set => derateIce = value;
        }

        public float DerateFire
        {
            get => derateFire;
            set => derateFire = value;
        }

        public float DerateElec
        {
            get => derateElec;
            set => derateElec = value;
        }

        public float DerateWind
        {
            get => derateWind;
            set => derateWind = value;
        }

        public float DerateEnergy
        {
            get => derateEnergy;
            set => derateEnergy = value;
        }
        public bool IsInit
        {
            get => isInit;
            set
            {
                isInit = value;
            }
        }

        public void Init()
        {
            IsInit = true;
        }
        public abstract void SetContorlImmunityList();
    }
}




