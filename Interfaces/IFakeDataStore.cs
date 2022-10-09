using System.Threading.Tasks;

namespace WasmSpa.Interfaces
{
    /// <summary>
    /// Интерфейс хранилища, имитирующего базу данных.
    /// </summary>
    /// <typeparam name="T1">Тип набора данных.</typeparam>
    /// <typeparam name="T2">Тип данных внутри набора данных.</typeparam>
    public interface IFakeDataStore<T1, T2>
    where T1 : IHaveItemsSet<T2>
    where T2 : IHaveId
    {
        /// <summary>
        /// Получение всего набора данных хранилиша.
        /// </summary>
        /// <returns>Набор данных.</returns>
        /// <remarks>
        /// Т.к. конечная реализация работает с localStorage,
        /// то данный метод вытаскивает и материализует сразу
        /// весь набор данных внутри хранилища.
        /// </remarks>
        public Task<T1> GetAll();

        /// <summary>
        /// Сохранение записи в хранилище.
        /// </summary>
        /// <param name="record">Запись для сохранения.</param>
        public Task Save(T2 record);

        /// <summary>
        /// Полная очистка хранилища.
        /// </summary>
        public Task Flush();

        /// <summary>
        /// Удаление записи в хранилище.
        /// </summary>
        /// <param name="record">Запись для удаления.</param>
        public Task Delete(T2 record);

        /// <summary>
        /// Обновление записи в хранилище.
        /// </summary>
        /// <param name="record">Запись для обновления.</param>
        public Task Update(T2 record);

        /// <summary>
        /// Инициализация хранилища.
        /// </summary>
        /// <param name="rootStore">Изначальный набор данных.</param>
        /// <param name="rootRecord">Изначальная запись в хранилище.</param>
        /// <remarks>
        /// Т.к. конечная реализация работает с localStorage,
        /// данный метод необходим, чтобы на момент работы с
        /// реализацией не падали обращения к хранилищу.
        /// </remarks>
        public Task Initialize(T1 rootStore, T2 rootRecord);
    }
}