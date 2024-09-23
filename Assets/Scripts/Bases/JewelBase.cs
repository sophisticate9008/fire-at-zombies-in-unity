
using System;


namespace MyBase
{

    [Serializable]
    public class JewelBase
    {
        public int id;
        public string place;
        public int level;
        public string description;
        public bool isEmbedded;
        public virtual void Effect()
        {

        }
    }
}