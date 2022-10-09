using Microsoft.AspNetCore.Components;
using WasmSpa.Enums;

namespace WasmSpa.CustomComponents
{
    /// <summary>
    /// Класс определения blazor компонента работы с авторизацией.
    /// </summary>
    public class CheckAuthorizationBase : ComponentBase
    {
        /// <summary>
        /// Пермишен, который определяет будет ли отрендерен
        /// фрагмент кода внутри RenderFragment в зависимости
        /// от роли пользователя.
        /// </summary>
        [Parameter]
        public UserRole PermToRender { get; set; }

        /// <summary>
        /// Фрагмент разметки, который будет отрендерен
        /// если роль пользователя удовлетворяет выставленному пермишену.
        /// </summary>
        [Parameter]
        public RenderFragment PermDependentRenderFragment { get; set; }
    }
}