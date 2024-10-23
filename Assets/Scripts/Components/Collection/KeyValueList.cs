using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components.Collection
{
    [Serializable]
    public class KeyValuePair<TK, TV>
    {
        public TK key;
        public TV value;
    }

    [Serializable]
    public class KeyValueList<TK, TV>
    {
        [SerializeField] private List<KeyValuePair<TK, TV>> list = new List<KeyValuePair<TK, TV>>();
        public TV this[TK key]
        {
            get => list[KeyIndex(key)].value;
            set
            {
                var index = KeyIndex(key);
                if (index != -1) list[index].value = value;
                else list.Add(new KeyValuePair<TK, TV> {key = key, value = value});
            }
        }

        private int KeyIndex(TK key)
        {
            return list.FindIndex(item => item.key.Equals(key));
        }
    }
}