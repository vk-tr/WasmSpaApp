using System.Threading.Tasks;
using WasmSpa.Models;
using WasmSpa.Services.Generic;

namespace WasmSpa.Services.Users
{
    /// <summary>
    /// Сервис для работы с аутентификацией.
    /// </summary>
    public class AuthenticationService
    {
        private readonly LocalStorageService<User> _localStorage;

        public AuthenticationService(LocalStorageService<User> localStorage)
        {
            _localStorage = localStorage;
        }

        /// <summary>
        /// Проверка аутентификации текущего пользователя.
        /// </summary>
        /// <returns>Авторизован ли текущий пользватель.</returns>
        public async Task<bool> CheckUserAuthenticated()
        {
            var currentUser = await GetCurrentUser();

            return currentUser != null;
        }

        /// <summary>
        /// Установка текущего пользователя в локальное хранилище
        /// </summary>
        /// <param name="user">Объект пользователя для установки в локальное хранилище.</param>
        public async Task SetCurrentUser(User user)
        {
            await _localStorage.Set(user);
        }

        /// <summary>
        /// Получение пользователя из локального хранилища.
        /// </summary>
        /// <returns>Текущий пользователь в локальном хранилище.</returns>
        public async Task<User> GetCurrentUser()
        {
            var currentUser = await _localStorage.Get();

            return currentUser;
        }
    }
}