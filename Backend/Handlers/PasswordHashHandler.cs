﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Backend.Handlers;

public class PasswordHashHandler
{
    private static int _iterationCount = 100000;
    private static RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();

    public static string HashPassword(string password)
    {
        int saltSize = 128 / 8;
        var salt = new byte[saltSize];
        _randomNumberGenerator.GetBytes(salt);
        var subkey = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, _iterationCount, 256 / 8);

        var outputBytes = new byte[13 + salt.Length + subkey.Length];
        outputBytes[0] = 0x01;
        WriteNetworkByteOrder(outputBytes, 1, (uint)KeyDerivationPrf.HMACSHA512);
        WriteNetworkByteOrder(outputBytes, 5, (uint)_iterationCount);
        WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);
        Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
        Buffer.BlockCopy(subkey, 0, outputBytes, 13 + salt.Length, subkey.Length);

        return Convert.ToBase64String(outputBytes);
    }

    public static bool VerifyPassword(string password, string hash)
    {
        try
        {
            var hashedPassword = Convert.FromBase64String(hash);
            var keyDerivationPrf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedPassword, 1);
            var iterationCount = (int)ReadNetworkByteOrder(hashedPassword, 5);
            var saltLength = (int)ReadNetworkByteOrder(hashedPassword, 9);
            if (saltLength < 128 / 8) return false;

            var salt = new byte[saltLength];
            Buffer.BlockCopy(hashedPassword, 13, salt, 0, salt.Length);

            var subkeyLength = hashedPassword.Length - 13 - salt.Length;
            if (subkeyLength < 256 / 8) return false;

            var expectedSubkey = new byte[subkeyLength];
            Buffer.BlockCopy(hashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            var actualSubkey = KeyDerivation.Pbkdf2(password, salt, keyDerivationPrf, iterationCount, subkeyLength);

            return actualSubkey.SequenceEqual(expectedSubkey);
        }
        catch
        {
            return false;
        }
    }

    private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
    {
        buffer[offset + 0] = (byte)((value >> 24) & 0xff);
        buffer[offset + 1] = (byte)((value >> 16) & 0xff);
        buffer[offset + 2] = (byte)((value >> 8) & 0xff);
        buffer[offset + 3] = (byte)(value & 0xff);
    }

    private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
    {
        return ((uint)(buffer[offset + 0]) << 24)
             | ((uint)(buffer[offset + 1]) << 16)
             | ((uint)(buffer[offset + 2]) << 8)
             | ((uint)(buffer[offset + 3]));
    }
}
