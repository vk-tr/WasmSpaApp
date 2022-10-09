using System.Collections.Generic;
using WasmSpa.Interfaces;

namespace WasmSpa.Models
{
    /// <summary>
    /// Набор данных с пользователями внутри.
    /// </summary>
    public class UsersStore : IHaveItemsSet<User>
    {
        public List<User> ItemsSet { get; set; }
    }
}