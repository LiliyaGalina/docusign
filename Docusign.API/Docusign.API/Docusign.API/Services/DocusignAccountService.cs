﻿using Docusign.API.Models.Utils;
using Docusign.API.Utils;
using DocuSign.eSign.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using static DocuSign.eSign.Client.Auth.OAuth;
using static DocuSign.eSign.Client.Auth.OAuth.UserInfo;

namespace Docusign.API.Services
{
    public class DocusignAccountService
    {

        private readonly DocusigJWTSettings _docusignJWTSettings;
        private readonly DocusignSettings _docusignSettings;

        public DocusignAccountService(
            IOptions<DocusigJWTSettings> docusignJWTSettings,
            IOptions<DocusignSettings> docusignSettings)
        {
            _docusignJWTSettings = docusignJWTSettings.Value;
            _docusignSettings = docusignSettings.Value;
        }

        public Account GetAccountInfo()
        {
            var access_token = RequestAccessTokenForDocusign();

            var apiClient = new ApiClient(_docusignJWTSettings.BasePath);
            apiClient.SetOAuthBasePath(_docusignJWTSettings.AuthServer);
            UserInfo userInfo = apiClient.GetUserInfo(access_token);
            Account acct = userInfo.Accounts.FirstOrDefault();
            if (acct == null)
            {
                throw new UnauthorizedAccessException("The user does not have access to account");
            }

            return acct;
        }

        public string RequestAccessTokenForDocusign()
        {
            var scopes = new List<string>
                {
                    "signature",
                    "impersonation",
                };
            var expiresInHours = 1;
            var privateKey = DSHelper.ReadFileContent(
                DSHelper.PrepareFullPrivateKeyFilePath(_docusignJWTSettings.PrivateKeyFile));

            var apiClient = new ApiClient(_docusignJWTSettings.BasePath);
            var tokenResult = apiClient.RequestJWTUserToken(
                _docusignJWTSettings.ClientId,
                _docusignJWTSettings.ImpersonatedUserId,
                _docusignJWTSettings.AuthServer,
                privateKey,
                expiresInHours, scopes);

            return tokenResult.access_token;
        }


    }
}