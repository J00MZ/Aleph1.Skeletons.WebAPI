using Aleph1.Security.Contracts;
using System;
using System.Security.Cryptography;
using System.Web.Script.Serialization;

namespace Aleph1.Skeletons.WebAPI.Security.Moq
{
    internal class JSON : ICipher
    {
        private static readonly JavaScriptSerializer TEXT_SERIALIZER = new JavaScriptSerializer();
        
        private class Storage<T>
        {
            public Storage() { }
            public Storage(T data, TimeSpan? timeSpan)
            {
                Data = data;
                if (timeSpan.HasValue)
                    ExpirationDate = DateTime.UtcNow + timeSpan.Value;
            }

            public T Data { get; set; }

            /// <summary>if the date is null - the ticket is unlimited</summary>
            public DateTime? ExpirationDate { get; set; }
        }

        /// <summary>Decrypts the specified data for a specific user in a spesific application.</summary>
        /// <typeparam name="T">Any</typeparam>
        /// <param name="appPrefix">The application prefix.</param>
        /// <param name="userUniqueID">The user unique identifier.</param>
        /// <param name="encryptedData">The encrypted data.</param>
        /// <returns>the decrypted data</returns>
        /// <exception cref="CryptographicException"></exception>
        public T Decrypt<T>(string appPrefix, string userUniqueID, string encryptedData)
        {
            Storage<T> store = TEXT_SERIALIZER.Deserialize<Storage<T>>(encryptedData);
            if (store.ExpirationDate.HasValue && store.ExpirationDate.Value < DateTime.UtcNow)
                throw new CryptographicException($"Data Expired {DateTime.UtcNow - store.ExpirationDate.Value} ago");
            return store.Data;
        }

        /// <summary>Encrypts the specified data for a specific user in a specific application.</summary>
        /// <typeparam name="T">Any</typeparam>
        /// <param name="appPrefix">The application prefix.</param>
        /// <param name="userUniqueID">The user unique identifier.</param>
        /// <param name="data">The data to encrypt</param>
        /// <param name="timeSpan">the duration of the ticket generated</param>
        /// <returns>the encrypted data</returns>
        public string Encrypt<T>(string appPrefix, string userUniqueID, T data, TimeSpan? timeSpan = null)
        {
            Storage<T> store = new Storage<T>(data, timeSpan);
            return TEXT_SERIALIZER.Serialize(store);
        }
    }
}
