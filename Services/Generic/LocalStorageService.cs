using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using WasmSpa.Helpers;
using WasmSpa.Interfaces;

namespace WasmSpa.Services.Generic
{
    /// <summary>
    /// Реализация работы с localStorage.
    /// </summary>
    /// <typeparam name="T">Тип данных хранилища.</typeparam>
    /// <remarks>
    /// Быть внимательным при использовании, т.к. методы
    /// соответствуют специфике работы с localStorage.
    /// </remarks>
    public class LocalStorageService<T> : ILocalStorage<T>
    where T : class
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly string _key;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _key = GetStoreKeyHelper.GetKeyLocal(typeof(T));
        }
        public async Task<T> Get()
        {
            var storageObject = await _jsRuntime
                .InvokeAsync<string>(
                    "localStorage.getItem",
                    _key);

            return storageObject == null
                ? null
                : JsonSerializer.Deserialize<T>(storageObject);
        }

        public async Task Set(T record)
        {
            await _jsRuntime
                .InvokeVoidAsync(
                    "localStorage.setItem",
                    _key,
                    JsonSerializer.Serialize(record));
        }

        public async Task Clear()
        {
            await _jsRuntime
                .InvokeVoidAsync(
                    "localStorage.removeItem",
                    _key);
        }
    }
}