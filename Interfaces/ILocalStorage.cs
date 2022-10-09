using System.Threading.Tasks;

namespace WasmSpa.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с localStorage.
    /// </summary>
    /// <typeparam name="T">Тип данных хранилища.</typeparam>
    /// <remarks>
    /// Быть внимательным при использовании, т.к. методы
    /// соответствуют специфике работы с localStorage.
    /// </remarks>
    public interface ILocalStorage<T>
    {
        /// <summary>
        /// Получение объекта локального хранилища.
        /// </summary>
        public Task<T> Get();

        /// <summary>
        /// Установка объекта локального хранилища.
        /// </summary>
        /// <param name="record">Объект для установки в локальное хранилище.</param>
        public Task Set(T record);

        /// <summary>
        /// Очистка локального хранилища.
        /// </summary>
        public Task Clear();
    }
}