﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using System.Resources;

namespace Test.WebApi.Middleware
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly TokenValidationParameters _validationParameters;
        private readonly IEnumerable<IIssuerSecurityTokenProvider> _issuerCredentialProviders;
        private readonly ISigningCredentialsProvider _signingCredentialsProvider;
        private readonly TimeSpan _defaultJwtExpireTimeSpan;
        private JwtSecurityTokenHandler _tokenHandler;

        /// <summary>
        /// Creates a new JwtFormat with TokenHandler and UseTokenLifetime enabled by default.
        /// </summary>
        protected CustomJwtFormat()
        {
            TokenHandler = new JwtSecurityTokenHandler();

            UseTokenLifetime = true;
        }

        public CustomJwtFormat(ISigningCredentialsProvider signingCredentialsProvider, TimeSpan defaultJwtExpireTimeSpan)
        {
            _signingCredentialsProvider = signingCredentialsProvider;
            _defaultJwtExpireTimeSpan = defaultJwtExpireTimeSpan;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtFormat"/> class.
        /// </summary>
        /// <param name="allowedAudience">The allowed audience for JWTs.</param>
        /// <param name="issuerCredentialProvider">The issuer credential provider.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the <paramref name="issuerCredentialProvider"/> is null.</exception>
        public CustomJwtFormat(string allowedAudience, IIssuerSecurityTokenProvider issuerCredentialProvider)
            : this()
        {
            if (string.IsNullOrWhiteSpace(allowedAudience))
            {
                throw new ArgumentNullException("allowedAudience");
            }
            if (issuerCredentialProvider == null)
            {
                throw new ArgumentNullException("issuerCredentialProvider");
            }

            _validationParameters = new TokenValidationParameters()
            {
                ValidAudience = allowedAudience,
                AuthenticationType = "JWT",
            };
            _issuerCredentialProviders = new[] { issuerCredentialProvider };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtFormat"/> class.
        /// </summary>
        /// <param name="allowedAudiences">The allowed audience for JWTs.</param>
        /// <param name="issuerCredentialProviders">The issuer credential provider.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the <paramref name="issuerCredentialProviders"/> is null.</exception>
        public CustomJwtFormat(IEnumerable<string> allowedAudiences, IEnumerable<IIssuerSecurityTokenProvider> issuerCredentialProviders)
            : this()
        {
            if (allowedAudiences == null)
            {
                throw new ArgumentNullException("allowedAudiences");
            }
            var audiences = new List<string>(allowedAudiences);
            if (!audiences.Any())
            {
                throw new ArgumentOutOfRangeException("allowedAudiences");
            }

            if (issuerCredentialProviders == null)
            {
                throw new ArgumentNullException("issuerCredentialProviders");
            }
            var credentialProviders = new List<IIssuerSecurityTokenProvider>(issuerCredentialProviders);
            if (!credentialProviders.Any())
            {
                throw new ArgumentOutOfRangeException("issuerCredentialProviders");
            }

            _validationParameters = new TokenValidationParameters()
            {
                ValidAudiences = audiences,
                AuthenticationType = "JWT",
            };
            _issuerCredentialProviders = issuerCredentialProviders;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtFormat"/> class.
        /// </summary>
        /// <param name="validationParameters"> <see cref="TokenValidationParameters"/> used to determine if a token is valid.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the <paramref name="validationParameters"/> is null.</exception>
        public CustomJwtFormat(TokenValidationParameters validationParameters)
            : this()
        {
            if (validationParameters == null)
            {
                throw new ArgumentNullException("validationParameters");
            }

            _validationParameters = validationParameters;

            if (string.IsNullOrWhiteSpace(_validationParameters.AuthenticationType))
            {
                _validationParameters.AuthenticationType = "JWT";
            }
        }

        public CustomJwtFormat(TokenValidationParameters validationParameters, IIssuerSecurityTokenProvider issuerCredentialProvider)
            : this(validationParameters)
        {
            if (issuerCredentialProvider == null)
            {
                throw new ArgumentNullException("issuerCredentialProvider");
            }

            _issuerCredentialProviders = new[] { issuerCredentialProvider };
        }

        /// <summary>
        /// Gets or sets a value indicating whether JWT issuers should be validated.
        /// </summary>
        /// <value>
        /// true if the issuer should be validate; otherwise, false.
        /// </value>
        public bool ValidateIssuer
        {
            get { return _validationParameters.ValidateIssuer; }
            set { _validationParameters.ValidateIssuer = value; }
        }

        /// <summary>
        /// A System.IdentityModel.Tokens.SecurityTokenHandler designed for creating and validating Json Web Tokens.
        /// </summary>
        public JwtSecurityTokenHandler TokenHandler
        {
            get { return _tokenHandler; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _tokenHandler = value;
            }
        }

        /// <summary>
        /// Indicates that the authentication session lifetime (e.g. cookies) should match that of the authentication token.
        /// If the token does not provide lifetime information then normal session lifetimes will be used.
        /// This is enabled by default.
        /// </summary>
        public bool UseTokenLifetime { get; set; }

        /// <summary>
        /// Transforms the specified authentication ticket into a JWT.
        /// </summary>
        /// <param name="data">The authentication ticket to transform into a JWT.</param>
        /// <returns></returns>
        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            string issuer;
            string audience;
            data.Properties.Dictionary.TryGetValue("issuer", out issuer);
            data.Properties.Dictionary.TryGetValue("audience", out audience);

            if (issuer == null) throw new InvalidOperationException("AuthenticationTicket.Properties does not include 'Issuer' value.");
            if (audience == null) throw new InvalidOperationException("AuthenticationTicket.Properties does not include 'Audience' value.");

            DateTime issuedUtc = data.Properties.IssuedUtc.HasValue
                ? GetUtcDateTime(data.Properties.IssuedUtc.Value)
                : DateTime.UtcNow;

            DateTime expiresUtc = data.Properties.ExpiresUtc.HasValue
                ? GetUtcDateTime(data.Properties.ExpiresUtc.Value)
                : DateTime.UtcNow.Add(_defaultJwtExpireTimeSpan);

            SigningCredentials signingCredentials = _signingCredentialsProvider.GetSigningCredentials(issuer, audience);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: data.Identity.Claims,
                expires: expiresUtc,
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Validates the specified JWT and builds an AuthenticationTicket from it.
        /// </summary>
        /// <param name="protectedText">The JWT to validate.</param>
        /// <returns>An AuthenticationTicket built from the <paramref name="protectedText"/></returns>
        /// <exception cref="System.ArgumentNullException">Thrown if the <paramref name="protectedText"/> is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the <paramref name="protectedText"/> is not a JWT.</exception>
        public AuthenticationTicket Unprotect(string protectedText)
        {
            if (string.IsNullOrWhiteSpace(protectedText))
            {
                throw new ArgumentNullException("protectedText");
            }

            var token = TokenHandler.ReadToken(protectedText) as JwtSecurityToken;

            if (token == null)
            {
                throw new ArgumentOutOfRangeException("protectedText");
            }

            TokenValidationParameters validationParameters = _validationParameters;
            if (_issuerCredentialProviders != null)
            {
                // Lazy augment with issuers and tokens. Note these may be refreshed periodically.
                validationParameters = validationParameters.Clone();

                IEnumerable<string> issuers = _issuerCredentialProviders.Select(provider => provider.Issuer);
                if (validationParameters.ValidIssuers == null)
                {
                    validationParameters.ValidIssuers = issuers;
                }
                else
                {
                    validationParameters.ValidIssuers = validationParameters.ValidAudiences.Concat(issuers);
                }

                IEnumerable<SecurityToken> tokens = _issuerCredentialProviders.Select(provider => provider.SecurityTokens)
                    .Aggregate((left, right) => left.Concat(right));
                if (validationParameters.IssuerSigningTokens == null)
                {
                    validationParameters.IssuerSigningTokens = tokens;
                }
                else
                {
                    validationParameters.IssuerSigningTokens = validationParameters.IssuerSigningTokens.Concat(tokens);
                }
            }

            SecurityToken validatedToken;
            ClaimsPrincipal claimsPrincipal = TokenHandler.ValidateToken(protectedText, validationParameters, out validatedToken);
            var claimsIdentity = (ClaimsIdentity)claimsPrincipal.Identity;

            // Fill out the authenticationProperties issued and expires times if the equivalent claims are in the JWT
            var authenticationProperties = new AuthenticationProperties();

            if (UseTokenLifetime)
            {
                // Override any session persistence to match the token lifetime.
                DateTime issued = validatedToken.ValidFrom;
                if (issued != DateTime.MinValue)
                {
                    authenticationProperties.IssuedUtc = issued.ToUniversalTime();
                }
                DateTime expires = validatedToken.ValidTo;
                if (expires != DateTime.MinValue)
                {
                    authenticationProperties.ExpiresUtc = expires.ToUniversalTime();
                }

                authenticationProperties.AllowRefresh = false;
            }

            return new AuthenticationTicket(claimsIdentity, authenticationProperties);
        }

        private DateTime GetUtcDateTime(DateTimeOffset dateTime)
        {
            return new DateTime(
                    dateTime.Year,
                    dateTime.Month,
                    dateTime.Day,
                    dateTime.Hour,
                    dateTime.Minute,
                    dateTime.Second,
                    DateTimeKind.Utc
                );
        }
    }
}