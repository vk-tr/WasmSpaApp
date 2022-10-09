using System.Security.Cryptography;
using System.Text;

namespace WasmSpa.Extensions
{
    /// <summary>
    /// Методы расширения строки (тип string).
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Получение хэша строки алогоритмом Sha256.
        /// </summary>
        /// <param name="stringToHash">Входная строка.</param>
        /// <returns>Хэш строки в формате Sha256.</returns>
        public static string ComputeSha256Hash(this string stringToHash)
        {
            using var sha256Hash = SHA256.Create();

            var bytes = sha256Hash
                .ComputeHash(
                    Encoding.UTF8.GetBytes(stringToHash));

            var builder = new StringBuilder();

            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}