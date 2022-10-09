using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace WasmSpa.Services.Generic
{
    /// <summary>
    /// Сервис для вывода сообщений в javaScript alert() механизме.
    /// </summary>
    public class JsAlertService
    {
        private readonly IJSRuntime _jsRuntime;

        public JsAlertService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Вывод сообщения в alert().
        /// </summary>
        /// <param name="message">Сообщение для вывода.</param>
        public async Task Alert(string message)
        {
            await _jsRuntime
                .InvokeVoidAsync(
                    "alert",
                    message);
        }
    }
}