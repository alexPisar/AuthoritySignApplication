using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Cryptography.WinApi;
using Cryptography;

namespace AuthoritySignClient.Utils
{
    public class CertUtil
    {
        private WinApiCryptWrapper _crypto = new WinApiCryptWrapper();
        private System.Net.WebClient _client = null;

        public CertUtil() { }

        public CertUtil(System.Net.WebClient client)
        {
            _client = client;
        }

        public string GetOrgInnFromCertificate(X509Certificate2 certificate)
        {
            var inn = ParseCertAttribute(certificate.Subject, "ИНН").TrimStart('0');

            if (string.IsNullOrEmpty(inn) || inn.Length == 12)
            {
                _crypto.InitializeCertificate(certificate);
                inn = _crypto.GetValueBySubjectOid("1.2.643.100.4");
            }

            return inn;
        }

        public string ParseCertAttribute(string certData, string attributeName)
        {
            string result = String.Empty;
            try
            {
                if (certData == null || certData == "") return result;

                attributeName = attributeName + "=";

                if (!certData.Contains(attributeName)) return result;

                int start = certData.IndexOf(attributeName);

                if (start > 0 && !certData.Substring(0, start).EndsWith(" "))
                {
                    attributeName = " " + attributeName;

                    if (!certData.Contains(attributeName)) return result;
                }

                start = certData.IndexOf(attributeName) + attributeName.Length;

                int length = certData.IndexOf('=', start) == -1 ? certData.Length - start : certData.IndexOf(", ", start) - start;

                if (length == 0) return result;
                if (length > 0)
                {
                    result = certData.Substring(start, length);

                }
                else
                {
                    result = certData.Substring(start);
                }
                return result;

            }
            catch (Exception)
            {
                return result;
            }
        }

        /// <summary>
        /// Проверка сертификата на валидность, и что он не содержится в списках отзыва
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public bool IsCertificateValid(X509Certificate2 certificate, Logger.UtilityLog log = null)
        {
            log?.Log($"IsCertificateValid : проверка на валидность сертификата с серийным номером {certificate.SerialNumber}");
            _crypto.InitializeCertificate(certificate);

            var isCertValid = _crypto.IsCertificateValid(_client);
            log?.Log($"Результат проверки на наличие в списках отзыва - {(!isCertValid).ToString()}");
            return isCertValid;
        }

        public List<X509Certificate2> GetAllGostPersonalCertificates()
        {
            return _crypto.GetAllGostPersonalCertificates();
        }

        public byte[] Sign(X509Certificate2 signerCertificate, byte[] fileBytes, bool isDetached)
        {
            _crypto.InitializeCertificate(signerCertificate);
            var signature = _crypto.Sign(fileBytes, isDetached);
            return signature;
        }
    }
}
