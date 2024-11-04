using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

public class VnPayLibrary
{
    private readonly SortedDictionary<string, string> _requestData = new SortedDictionary<string, string>(StringComparer.Ordinal);

    public const string VERSION = "2.1.0";

    public void AddRequestData(string key, string value)
    {
        _requestData[key] = value;
    }

    public string CreateRequestUrl(string baseUrl, string vnp_HashSecret)
    {
        var queryString = new Dictionary<string, string>();
        foreach (var kvp in _requestData)
        {
            queryString[kvp.Key] = kvp.Value;
        }

        var query = QueryHelpers.AddQueryString(baseUrl, queryString);
        var signData = string.Join("&", _requestData.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        var vnpSecureHash = HmacSHA512(vnp_HashSecret, signData);
        query += "&vnp_SecureHash=" + vnpSecureHash;
        return query;
    }

    public bool ValidateSignature(string inputHash, string secretKey)
    {
        var signData = string.Join("&", _requestData.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        var generatedHash = HmacSHA512(secretKey, signData);
        return inputHash == generatedHash;
    }

    private static string HmacSHA512(string key, string input)
    {
        using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
        {
            var hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
            var hex = new StringBuilder(hashValue.Length * 2);
            foreach (var b in hashValue)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }
    }

    public string GetResponseData(string key)
    {
        return _requestData.TryGetValue(key, out var value) ? value : string.Empty;
    }

    public void AddResponseData(string key, string value)
    {
        _requestData[key] = value;
    }
}
