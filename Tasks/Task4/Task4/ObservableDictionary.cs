using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Task4
{
    /// <summary>
    /// Класс вызывающий события при добавлении и удалении объектов из коллекции.
    /// </summary>
    [Serializable]
    public class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        /// <summary>
        /// Событие изменения
        /// </summary>
        public event Action Changed;
        
        protected ObservableDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Коструктор по умолчанию
        /// </summary>
        public ObservableDictionary()
        {
        }
        
        /// <summary>
        /// Расширенный метод добавления значений в коллекцию
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        public new void Add(TKey key, TValue value)
        {
            if(ContainsKey(key))
                base[key] = value;
            else
                base.Add(key, value);
                
            Changed?.Invoke();
        }

        /// <summary>
        /// Расширенный метод удаления значений в коллекцию
        /// </summary>
        /// <param name="key">Ключ</param>
        public new void Remove(TKey key)
        {
            base.Remove(key);
            Changed?.Invoke();
        }
    }
}