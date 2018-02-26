using UnityEngine;
using System.Collections;
using ffDevelopmentSpace;


/* 
    Author:     fyw 
    CreateDate: 2018-02-25 15:12:33 
    Desc:       注释 
*/

namespace ffDevelopmentSpace
{
    public class Manager_Base<T> where T : new()
    {
        private static T _instance;
        private static readonly object syslock = new object();

        public static T GetInstance()
        {
            if (_instance == null)
            {
                lock (syslock)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                }
            }
            return _instance;
        }
        public  Manager_Base()
        {
            Init();
        }

        #region private function
        protected virtual void Init() {
            Debug.Log(" Manager Init");
        }
        //public  virtual void OnUpdate() { }
        #endregion
    }
}
