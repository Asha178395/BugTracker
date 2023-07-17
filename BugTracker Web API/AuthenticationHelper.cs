using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BugTracker_Web_API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BugTracker_Web_API
{
    public static class AuthenticationHelper
    {

        /// <summary>
        /// Azure Client Id.
        /// </summary>
        private static readonly string ClientIdConfigurationSettingsKey = "AuthOptions:ClientId";



        /// <summary>
        /// Azure Tenant Id.
        /// </summary>
        private static readonly string TenantIdConfigurationSettingsKey = "AuthOptions:TenantId";



        /// <summary>
        /// Azure Application Id URI.
        /// </summary>
        private static readonly string ApplicationIdURIConfigurationSettingsKey = "AuthOptions:ApplicationIdURI";



        /// <summary>
        /// Retrieve Valid Audiences.
        /// </summary>
        /// <param name="configuration">IConfiguration instance.</param>
        /// <returns>Valid Audiences.</returns>
        public static IEnumerable<string> GetValidAudiences(IConfiguration configuration)
        {
            var clientId = configuration[ClientIdConfigurationSettingsKey];
            var applicationIdURI = configuration[ApplicationIdURIConfigurationSettingsKey];
            var validAudiences = new List<string> { clientId, applicationIdURI.ToLower() };
            return validAudiences;
        }



        /// <summary>
        /// Retrieve Valid Issuers.
        /// </summary>
        /// <param name="configuration">IConfiguration instance.</param>
        /// <returns>Valid Issuers.</returns>
        public static IEnumerable<string> GetValidIssuers(IConfiguration configuration)
        {
            var tenantId = configuration[TenantIdConfigurationSettingsKey];



            IEnumerable<string> validIssuers = new List<string>
            {
                $"https://login.microsoftonline.com/{tenantId}/",
                $"https://sts.windows.net/{tenantId}/",
            };
            return validIssuers;
        }



        /// <summary>
        /// Audience Validator.
        /// </summary>
        /// <param name="tokenAudiences">Token audiences.</param>
        /// <param name="securityToken">Security token.</param>
        /// <param name="validationParameters">Validation parameters.</param>
        /// <returns>Audience validator status.</returns>
        public static bool AudienceValidator(
            IEnumerable<string> tokenAudiences,
            SecurityToken securityToken,
            TokenValidationParameters validationParameters)
        {
            if (tokenAudiences == null || tokenAudiences.Count() == 0)
            {
                throw new ApplicationException("No audience defined in token!");
            }



            var validAudiences = validationParameters.ValidAudiences;
            if (validAudiences == null || validAudiences.Count() == 0)
            {
                throw new ApplicationException("No valid audiences defined in validationParameters!");
            }



            foreach (var tokenAudience in tokenAudiences)
            {
                if (validAudiences.Any(validAudience => validAudience.Equals(tokenAudience, StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

