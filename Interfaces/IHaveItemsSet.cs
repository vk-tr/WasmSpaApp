using System.Collections.Generic;

namespace WasmSpa.Interfaces
{
    /// <summary>
    /// Тип набора данных хранилища.
    /// </summary>
    /// <typeparam name="T">Тип данных внутри набора данных.</typeparam>
    public interface IHaveItemsSet<T>
    where T : IHaveId
    {
        /// <summary>
        /// Сущность набора данных.
        /// </summary>
        public List<T> ItemsSet { get; set; }
    }
}