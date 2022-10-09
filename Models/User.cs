using WasmSpa.Enums;
using WasmSpa.Interfaces;

namespace WasmSpa.Models
{
    /// <summary>
    /// Модель пользователя.
    /// </summary>
    public class User : IHaveId
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }
    }
}