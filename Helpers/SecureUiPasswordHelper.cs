namespace WasmSpa.Helpers
{
    /// <summary>
    /// Статический класс для засекречивания пароля
    /// при отображении в пользовательском интерфейсе.
    /// </summary>
    public static class SecureUiPasswordHelper
    {
        /// <summary>
        /// Сокрытие строки, которая содержит пароль, символами '*'.
        /// </summary>
        /// <param name="password">Строка с паролем.</param>
        /// <returns>Переписанная символоми '*' строка пароля.</returns>
        public static string Secure(string password)
        {
            var securedPassword = string.Empty;

            foreach (var ch in password)
            {
                securedPassword += "*";
            }

            return securedPassword;
        }
    }
}