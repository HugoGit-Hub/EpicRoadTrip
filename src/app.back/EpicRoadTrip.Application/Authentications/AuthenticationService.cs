using EpicRoadTrip.Application.Options;
using EpicRoadTrip.Domain.Authentications;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace EpicRoadTrip.Application.Authentications;

public class AuthenticationService(
    IConfiguration configuration,
    IAuthenticationRepository authenticationRepository) : IAuthenticationService
{
    public async Task<bool> IsEmailAlreadyUse(string email, CancellationToken cancellationToken)
    {
        return await authenticationRepository.IsEmailAlreadyUse(email, cancellationToken);
    }

    public async Task<bool> AreCredentialscorrects(string email, string password, CancellationToken cancellationToken)
    {
        return await authenticationRepository.AreCredentialscorrects(email, password, cancellationToken);
    }

    public Result<string> Encrypt(string content)
    {
        try
        {
            var aes = Aes.Create();
            var key = configuration.GetSection(nameof(EncodingKey))[nameof(EncodingKey.Value)];
            if (string.IsNullOrEmpty(key))
            {
                return Result<string>.Failure(AuthenticationErrors.EncryptionKeyNotFoundError);
            }

            aes.Key = Encoding.UTF8.GetBytes(key);

            var iv = GenerateIv(content);
            var encryptor = aes.CreateEncryptor(aes.Key, iv);
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using (var streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(content);
            }

            var encrypted = Convert.ToBase64String(memoryStream.ToArray());

            return Result<string>.Success(encrypted);
        }
        catch (Exception e)
        {
            return Result<string>.Failure(AuthenticationErrors.EncryptionFailedError(e));
        }
    }

    public Result<string> HashWithSalt(string content)
    {
        try
        {
            var dataBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(content));
            var salt = configuration.GetSection(nameof(Salt))[nameof(Salt.Value)];
            if (string.IsNullOrEmpty(salt))
            {
                return Result<string>.Failure(AuthenticationErrors.SaltNotFoundError);
            }

            var saltBytes = Encoding.UTF8.GetBytes(salt);
            var combinedBytes = new byte[dataBytes.Length + saltBytes.Length];
            Buffer.BlockCopy(dataBytes, 0, combinedBytes, 0, dataBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, combinedBytes, dataBytes.Length, saltBytes.Length);
            var hashBytes = SHA512.HashData(combinedBytes);
            var hashedAndSalted = Convert.ToBase64String(hashBytes);

            return Result<string>.Success(hashedAndSalted);
        }
        catch (Exception e)
        {
            return Result<string>.Failure(AuthenticationErrors.HashingFailedError(e));
        }
    }

    public Result<string> GenerateToken(User user)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var issuer = configuration.GetSection(nameof(Jwt))[nameof(Jwt.Issuer)];
            var audience = configuration.GetSection(nameof(Jwt))[nameof(Jwt.Audience)];
            var key = configuration.GetSection(nameof(Jwt))[nameof(Jwt.Key)];
            if (string.IsNullOrEmpty(issuer) || 
                string.IsNullOrEmpty(audience) || 
                string.IsNullOrEmpty(key))
            {
                return Result<string>.Failure(AuthenticationErrors.TokenGenerationConfigurationError);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Result<string>.Success(tokenString);
        }
        catch (Exception e)
        {
            return Result<string>.Failure(AuthenticationErrors.TokenGenerationFailedError(e));
        }
    }

    private static byte[] GenerateIv(string content)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(content));
        var iv = new byte[16];
        Array.Copy(hash, iv, iv.Length);

        return iv;
    }
}