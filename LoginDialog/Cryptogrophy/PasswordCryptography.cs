namespace LoginDialog.Cryptogrophy
{
    #region Referenceing

    using System;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Cryptography;

    #endregion

    /// <summary>
    ///     Crypto class for password validation
    /// </summary>
    public class PasswordCryptography
    {
        public const int SALT_BYTE_SIZE = 32;
        public const int HASH_BYTE_SIZE = 32;
        public const int PBKDF2_ITERATIONS = 1000;

        public const int ITERATION_INDEX = 0;
        public const int SALT_INDEX = 1;
        public const int PBKDF2_INDEX = 2;

        /// <summary>
        ///     Create hash from salted password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string CreateHash(string password)
        {
            var salt = new byte[SALT_BYTE_SIZE];
            (new RNGCryptoServiceProvider()).GetBytes(salt);

            // Hash the password and encode the parameters
            var hash = Pbkdf2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);

            return string.Format("{0}:{1}:{2}", PBKDF2_ITERATIONS, Convert.ToBase64String(salt),
                Convert.ToBase64String(hash));
        }

        /// <summary>
        ///     Validation entry method
        /// </summary>
        /// <param name="password"></param>
        /// <param name="correctHash"></param>
        /// <returns></returns>
        public static bool ValidatePassword(SecureString password, string correctHash)
        {
            // Extract the parameters from the hash
            char[] delimiter = {':'};
            var split = correctHash.Split(delimiter);
            var iterations = Int32.Parse(split[ITERATION_INDEX]);
            var salt = Convert.FromBase64String(split[SALT_INDEX]);
            var hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

            var testHash = Pbkdf2(DecryptStringValue(password), salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        /// <summary>
        ///     Hash validation
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint) a.Length ^ (uint) b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint) (a[i] ^ b[i]);
            return diff == 0;
        }

        /// <summary>
        ///     Generate the PBKDF2-SHA1 hash of a password.
        /// </summary>
        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt)
            {
                IterationCount = iterations
            };
            return pbkdf2.GetBytes(outputBytes);
        }

        private static string DecryptStringValue(SecureString value)
        {
            var valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}