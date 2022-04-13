using System;

namespace BoilerPlate.Service.Contract
{
    public interface IEncryptionService
    {
        string Encrypt(int input);
        string Encrypt(string input);
        string Decrypt(string cipherText);
        T Decrypt<T>(string cipherText) where T : struct;
    }
}
