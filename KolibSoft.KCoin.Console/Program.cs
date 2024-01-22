using System.Buffers.Text;
using System.Security.Cryptography;
using KolibSoft.KCoin.Core;

var rsa = RSA.Create();
Console.WriteLine(new KCoinAddress().Validate());
Console.WriteLine(KCoinAddress.None);
Console.WriteLine(KCoinAddress.Parse(Convert.ToBase64String(SHA256.HashData(rsa.ExportRSAPublicKey()))));
