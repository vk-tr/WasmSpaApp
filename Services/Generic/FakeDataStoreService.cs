using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using WasmSpa.Exceptions;
using WasmSpa.Helpers;
using WasmSpa.Interfaces;

namespace WasmSpa.Services.Generic
{
    /// <summary>
    /// Реализация хранилища, имитирующего базу данных.
    /// </summary>
    /// <typeparam name="T1">Тип набора данных.</typeparam>
    /// <typeparam name="T2">Тип данных внутри набора данных.</typeparam>
    public class FakeDataStoreService<T1, T2> : IFakeDataStore<T1, T2>
    where T1 : class, IHaveItemsSet<T2>
    where T2 : class, IHaveId
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly JsAlertService _jsAlertService;

        private readonly string _key;
        private bool _initialized;

        public FakeDataStoreService(
            IJSRuntime jsRuntime,
            JsAlertService jsAlertService)
        {
            _jsRuntime = jsRuntime;
            _jsAlertService = jsAlertService;

            _key = GetStoreKeyHelper.GetKey(typeof(T1));
            _initialized = false;
        }

        public async Task<T1> GetAll()
        {
            try
            {
                InitializedValidation();
            }
            catch (FakeDataStoreException dsEx)
            {
                await _jsAlertService.Alert(dsEx.Message);
                return null;
            }

            var storageObject = await _jsRuntime
                .InvokeAsync<string>(
                    "localStorage.getItem",
                    _key);

            if (storageObject == null)
            {
                throw new FakeDataStoreException("Try to use empty Storage object. Please, Re- run project.");
            }

            return JsonSerializer.Deserialize<T1>(storageObject);
        }

        public async Task Save(T2 record)
        {
            try
            {
                var store = await GetAll();

                record.Id = GenerateId(store);

                store.ItemsSet.Add(record);

                await _jsRuntime
                    .InvokeVoidAsync(
                        "localStorage.setItem",
                        _key,
                        JsonSerializer.Serialize(store));
            }
            catch (FakeDataStoreException dsEx)
            {
                await _jsAlertService.Alert(dsEx.Message);
            }
        }

        public async Task Flush()
        {
            await _jsRuntime
                .InvokeVoidAsync(
                    "localStorage.removeItem",
                    _key);
        }

        public async Task Delete(T2 record)
        {
            try
            {
                var store = await GetAll();

                var recordToDelete = store.ItemsSet
                    .FirstOrDefault(x => x.Id == record.Id);

                if (recordToDelete == null)
                {
                    throw new FakeDataStoreException("Record didn't find in storage.");
                }

                store.ItemsSet.Remove(recordToDelete);

                await _jsRuntime
                    .InvokeVoidAsync(
                        "localStorage.setItem",
                        _key,
                        JsonSerializer.Serialize(store));
            }
            catch (FakeDataStoreException dsEx)
            {
                await _jsAlertService.Alert(dsEx.Message);
            }
        }

        public async Task Update(T2 record)
        {
            try
            {
                var store = await GetAll();

                var index = store.ItemsSet
                    .TakeWhile(x => x.Id != record.Id).Count();

                if (index == 0)
                {
                    throw new FakeDataStoreException("Record didn't find in storage.");
                };

                store.ItemsSet[index] = record;

                await _jsRuntime
                    .InvokeVoidAsync(
                        "localStorage.setItem",
                        _key,
                        JsonSerializer.Serialize(store));
            }
            catch (FakeDataStoreException dsEx)
            {
                await _jsAlertService.Alert(dsEx.Message);
            }
        }

        public async Task Initialize(T1 rootStore, T2 rootRecord)
        {
            rootRecord.Id = 1;

            rootStore.ItemsSet.Add(rootRecord);

            await _jsRuntime
                .InvokeVoidAsync(
                    "localStorage.setItem",
                    _key,
                    JsonSerializer.Serialize(rootStore));

            _initialized = true;
        }

        /// <summary>
        /// Генерация идентификатора записи.
        /// </summary>
        /// <param name="store">Объект хранилища.</param>
        /// <returns>Идентификатор записи.</returns>
        /// <remarks>
        /// Т.к. работаем с localStorage, который не
        /// умеет в реляционку и не имеет механизмов
        /// инкрементации идентификаторов, необходимо
        /// вычислять их с помощью отдельного метода.
        /// </remarks>
        private long GenerateId(IHaveItemsSet<T2> store)
        {
            var lastId = store.ItemsSet
                .OrderBy(x => x.Id)
                .Select(x => x.Id)
                .LastOrDefault();

            if (lastId == default)
            {
                throw new FakeDataStoreException("Can't create Id of record.");
            }

            return lastId + 1;
        }

        /// <summary>
        /// Проверка того, что хранилище было инициализировано
        /// </summary>
        /// <remarks>
        /// см. документацию метода Initialize интерфейса IFakeDataStore.
        /// </remarks>
        private void InitializedValidation()
        {
            if (!_initialized)
            {
                throw new FakeDataStoreException("Trying to use an empty DataStore");
            }
        }
    }
}