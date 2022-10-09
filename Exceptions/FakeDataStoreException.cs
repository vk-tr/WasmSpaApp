using System;

namespace WasmSpa.Exceptions
{
    public class FakeDataStoreException : Exception
    {
        public FakeDataStoreException(string message)
            : base($"DataStore Exception: {message}")
        {

        }
    }
}