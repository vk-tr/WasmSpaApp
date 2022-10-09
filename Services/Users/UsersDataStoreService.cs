using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WasmSpa.Enums;
using WasmSpa.Exceptions;
using WasmSpa.Helpers;
using WasmSpa.Models;
using WasmSpa.Services.Generic;

namespace WasmSpa.Services.Users
{
    /// <summary>
    /// Сервис работы с хранилищем объектов пользователей.
    /// </summary>
    public class UsersDataStoreService
    {
        private readonly FakeDataStoreService<UsersStore, User> _dataStore;
        private readonly JsAlertService _jsAlertService;

        public UsersDataStoreService(
            FakeDataStoreService<UsersStore, User> dataStore,
            JsAlertService jsAlertService)
        {
            _dataStore = dataStore;
            _jsAlertService = jsAlertService;

            Initialize();
        }

        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        public async Task<UsersStore> GetAllUsers()
        {
            try
            {
                return await _dataStore.GetAll();
            }
            catch (FakeDataStoreException dsEx)
            {
                await _jsAlertService.Alert(dsEx.Message);
                return null;
            }
        }

        /// <summary>
        /// Получение отдельного пользователя
        /// </summary>
        /// <param name="userName">Логин пользователя.</param>
        /// <returns>Объект пользователя.</returns>
        public async Task<User> GetUser(string userName)
        {
            try
            {
                var usersSet = await _dataStore.GetAll();

                return usersSet
                    .ItemsSet
                    .FirstOrDefault(x => x.Name == userName);
            }
            catch (FakeDataStoreException dsEx)
            {
                await _jsAlertService.Alert(dsEx.Message);
                return null;
            }
        }

        /// <summary>
        /// Сохранение пользователя в хранилище.
        /// </summary>
        /// <param name="user">Объект пользователя.</param>
        public async Task SaveUser(User user)
        {
            await _dataStore.Save(user);
        }

        /// <summary>
        /// Удаление пользователя из хранилища.
        /// </summary>
        /// <param name="userName">Логин пользователя.</param>
        public async Task DeleteUser(string userName)
        {
            var user = await GetUser(userName);

            await _dataStore.Delete(user);
        }

        /// <summary>
        /// Обновление пользователя в хранилище.
        /// </summary>
        /// <param name="user">Объект пользователя.</param>
        public async Task UpdateUser(User user)
        {
            await _dataStore.Update(user);
        }

        /// <summary>
        /// Инициализация.
        /// </summary>
        /// <remarks>
        /// см. документацию метода Initialize интерфейса IFakeDataStore.
        /// </remarks>
        private async Task Initialize()
        {
            var rootUser = new User
            {
                Id = 1,
                Name = RootUserHelper.RootLogin,
                Password = RootUserHelper.RootPassword,
                Role = UserRole.Admin
            };

            var rootStore = new UsersStore
            {
                ItemsSet = new List<User>()
            };

            await _dataStore.Initialize(rootStore, rootUser);
        }
    }
}