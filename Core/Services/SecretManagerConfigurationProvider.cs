using Google.Api.Gax.ResourceNames;
using Google.Cloud.SecretManager.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public static class GoogleProject
    {
        /// <summary>
        /// Get the Google Cloud Platform Project ID from the platform it is running on,
        /// or from the appsettings.json configuration if not running on Google Cloud Platform.
        /// </summary>
        /// <returns>
        /// The ID of the GCP Project this service is running on, or the AppConfig:GcpSettings:ProjectId
        /// from the configuration if not running on GCP.
        /// </returns>
        public static string GetProjectId()
        {
            var instance = Google.Api.Gax.Platform.Instance();
            var projectId = instance?.ProjectId;
            if (string.IsNullOrEmpty(projectId))
            {
                return null;
            }
            return projectId;
        }
    }
    public class SecretManagerConfigurationProvider : ConfigurationProvider
    {
        private readonly SecretManagerServiceClient _client;
        private readonly string _projectId;

        public SecretManagerConfigurationProvider()
        {
            //_projectId = "recordinservice";
            _projectId = "cas-prod-env";
                GoogleProject.GetProjectId();
            //var text = File.ReadAllText(@"C:\GoogleAuthFile\cas-prod-env-20199499a56a.json");
            //SecretManagerServiceClientBuilder secretManagerServiceClientBuilder = new SecretManagerServiceClientBuilder()
            //{
            //    JsonCredentials = text,
            //};
            //SecretManagerServiceClient client = secretManagerServiceClientBuilder.Build();
            //SecretVersionName secretVersionName = new SecretVersionName(_projectId, "RecordingAPISecret", "1");
            //_client = client;
           
            // Call the API.
           // AccessSecretVersionResponse result = _client.AccessSecretVersion(secretVersionName);

            // Convert the payload to a string. Payloads are bytes by default.
           // String payload = result.Payload.Data.ToStringUtf8();
            _client = SecretManagerServiceClient.Create();
            
        }

        public SecretManagerConfigurationProvider(SecretManagerServiceClient client, string projectId)
        {
            _client = client;
            _projectId = projectId;
        }

        /// <summary>
        /// Load Secrets from Google Secret Manager
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "N/A")]
        public override void Load()
        {
            if (string.IsNullOrEmpty(_projectId))
            {
                return; // skip for local debug
            }

            var secrets = _client.ListSecrets(new ProjectName(_projectId));
            foreach (var secret in secrets)
            {
                try
                {
                    var secretVersionName = new SecretVersionName(secret.SecretName.ProjectId, secret.SecretName.SecretId, "latest");
                    var secretVersion = _client.AccessSecretVersion(secretVersionName);
                    Set(NormalizeDelimiter(secret.SecretName.SecretId), secretVersion.Payload.Data.ToStringUtf8());
                }
                catch (Grpc.Core.RpcException)
                {
                    // Ignore. This might happen if secret is created but it has no versions available
                }
            }
        }

        private static string NormalizeDelimiter(string key)
        {
            return key.Replace("__", ConfigurationPath.KeyDelimiter);
        }
    }

    public class SecretManagerConfigurationSource : IConfigurationSource
    {
        /// <summary>
        /// Creates new SecretsManagerConfigurationProvider
        /// </summary>
        /// <param name="builder"></param>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new SecretManagerConfigurationProvider();
        }
    }

    public static class SecretManagerConfigurationExtensions
    {
        /// <summary>
        /// Add SecretManagerConfigurationSource to the build configuration
        /// </summary>
        /// <param name="configurationBuilder"></param>
        public static IConfigurationBuilder AddGoogleSecretsManager(this IConfigurationBuilder configurationBuilder)
        {
            if (configurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }

            configurationBuilder.Add(new SecretManagerConfigurationSource());

            return configurationBuilder;
        }
    }
}
