using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


namespace PlatformerPrototype.InfernKP.Boss
{
    public class ObjectPool : MonoBehaviour
    {
        #region Variables

        [SerializeField] GameObject projectilePrefab;

        ObjectPool<GameObject> pool;
        [SerializeField] Transform objectPoolParent;
        [SerializeField] int maxPoolSize = 10;
        [SerializeField] int defaultCapacity = 2;

        public GameObject GetObjectFromPool() => pool.Get();


        #endregion

        #region Default Methods
        private void Start()
        {
            CreateObjectPool();
        }

        #endregion

        #region Unique Methods

        void CreateObjectPool()
        {
            pool = new ObjectPool<GameObject>(
                createFunc: () => CreateObject(),
                actionOnGet: (obj) => obj.SetActive(true),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                false,
                defaultCapacity,
                maxPoolSize);
        }
        GameObject CreateObject()
        {
            GameObject createdObject = Instantiate(projectilePrefab, objectPoolParent);
            return createdObject;
        }

        #endregion
    }


}