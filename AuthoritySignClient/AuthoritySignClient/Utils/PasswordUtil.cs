using System;
using AuthoritySignClient.Utils.ConfigSet;

namespace AuthoritySignClient.Utils
{
    internal class PasswordUtil
    {
        private Config _config = Config.GetInstance();
        private const string _salt = "57x89fjtprgcw3yw0oiey98l6rb0y263maqodft8";

        public string GetPassword(string cipherDataBasePassword = null)
        {
            string password = null;

            if (cipherDataBasePassword == null)
                cipherDataBasePassword = _config.CipherDataBasePassword;

            if (!string.IsNullOrEmpty(cipherDataBasePassword))
            {
                var skitalaBytes = System.Text.Encoding.ASCII.GetBytes(cipherDataBasePassword);
                var saltData = System.Text.Encoding.ASCII.GetBytes(_salt);

                int position, shift;
                GetParametersForPassword(out position, out shift);
                var bytes = new byte[40];

                for (int j = 0; j < 8; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        bytes[j * 5 + k] = skitalaBytes[j + k * 8];
                    }
                }

                var passData = new System.Collections.Generic.List<byte>();
                int i = 0;
                while (saltData[(position + i * shift) % bytes.Length] != bytes[(position + i * shift) % bytes.Length])
                {
                    byte b;
                    if (saltData[(position + i * shift) % bytes.Length] > bytes[(position + i * shift) % bytes.Length])
                    {
                        b = (byte)(128 + (int)bytes[(position + i * shift) % bytes.Length] - (int)saltData[(position + i * shift) % bytes.Length]);
                    }
                    else
                    {
                        b = (byte)(bytes[(position + i * shift) % bytes.Length] - saltData[(position + i * shift) % bytes.Length]);
                    }
                    passData.Add(b);
                    i++;
                }
                password = System.Text.Encoding.ASCII.GetString(passData.ToArray());
            }
            
            return password;
        }

        public void SetDataBasePassword(string password)
        {
            int position, shift;
            GetParametersForPassword(out position, out shift);

            var passwordData = System.Text.Encoding.ASCII.GetBytes(password);
            var saltData = System.Text.Encoding.ASCII.GetBytes(_salt);

            int i = 0;
            foreach (var p in passwordData)
            {
                var b = (byte)(((int)p + (int)saltData[(position + i * shift) % saltData.Length]) % 128);
                saltData[(position + i * shift) % saltData.Length] = b;
                i++;
            }

            byte[] skitalaBytes = new byte[40];

            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 8; k++)
                {
                    skitalaBytes[8 * j + k] = saltData[j + 5 * k];
                }
            }

            _config.CipherDataBasePassword = System.Text.Encoding.ASCII.GetString(skitalaBytes);
        }

        public void GenerateParametersForPassword()
        {
            var rand = new Random();
            _config.PositionIndex = rand.Next() % 40;
            _config.ShiftIndex = rand.Next() % 5;
        }

        private void GetParametersForPassword(out int position, out int shift)
        {
            if (_config.PositionIndex == null || _config.ShiftIndex == null)
                GenerateParametersForPassword();

            int[] arrPosition = new int[40]
            {
               36, 39, 38, 2, 15, 16, 8, 26, 31, 21, 28, 5, 25, 9, 27, 18, 4, 29, 33, 34, 14, 35, 24, 0, 6, 10, 7, 23, 11, 13, 22, 1, 19, 17, 32, 3, 20, 12, 30, 37
            };

            int[] arrShift = new int[] { 13, 7, 19, 17, 23 };

            position = arrPosition[_config.PositionIndex.Value];
            shift = arrShift[_config.ShiftIndex.Value];
        }
    }
}
