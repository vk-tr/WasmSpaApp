namespace WasmSpa.Interfaces
{
    /// <summary>
    /// Интерфейс записи с идентификатором.
    /// </summary>
    public interface IHaveId
    {
        /// <summary>
        /// Идентификатор записи.
        /// </summary>
        public long Id { get; set; }
    }
}