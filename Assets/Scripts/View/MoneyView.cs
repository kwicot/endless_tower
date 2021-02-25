    using System;
    using UnityEngine;

    public class MoneyView : MonoBehaviour
    {
        public string MoneyName;
        public int Count;
        public float LifeTime = 5;


        private float time = 10f;
        private void FixedUpdate()
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                Destroy(gameObject);
            }
        }

        public MoneyView(string name, int count, float lifeTime)
        {
            MoneyName = name;
            Count = count;
            LifeTime = lifeTime;
        }
    }
