using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;

using Acontep.Seguranca;
using Acontep.Dado;
using System.Data;
using Acontep.AD.Manutencao.Seguranca;

namespace Acontep.Manutencao.Seguranca
{
    public class AcontepMembershipProvider : MembershipProvider
    {
        private bool _EnablePasswordRetrieval;
        private bool _EnablePasswordReset;
        private bool _RequiresQuestionAndAnswer;
        private string _AppName;
        private bool _RequiresUniqueEmail;
        //private string _DatabaseFileName;
        private string _HashAlgorithmType;
        //private int _ApplicationId;
        private int _MaxInvalidPasswordAttempts;
        private int _PasswordAttemptWindow;
        //private int _inRequiredPasswordLength;
        private int _MinRequiredPasswordLength;
        private int _MinRequiredNonalphanumericCharacters;
        private string _PasswordStrengthRegularExpression;
        //private DateTime _ApplicationIDCacheDate;
        private MembershipPasswordFormat _PasswordFormat;

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (String.IsNullOrEmpty(name))
            {
                name = "CustomMembershipProvider";
                config.Add("name", name);

            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "AcontepMembership - Acontep 2006");
            }
            base.Initialize(name, config);


            _EnablePasswordRetrieval = ConversaoValor.ObterValorBoolean(config, "enablePasswordRetrieval", false);
            _EnablePasswordReset = ConversaoValor.ObterValorBoolean(config, "enablePasswordReset", true);
            _RequiresQuestionAndAnswer = ConversaoValor.ObterValorBoolean(config, "requiresQuestionAndAnswer", true);
            _RequiresUniqueEmail = ConversaoValor.ObterValorBoolean(config, "requiresUniqueEmail", true);
            _MaxInvalidPasswordAttempts = ConversaoValor.ObterValorInt32(config, "maxInvalidPasswordAttempts", 5, false, 0);
            _PasswordAttemptWindow = ConversaoValor.ObterValorInt32(config, "passwordAttemptWindow", 10, false, 0);
            _MinRequiredPasswordLength = ConversaoValor.ObterValorInt32(config, "minRequiredPasswordLength", 7, false, 128);
            _MinRequiredNonalphanumericCharacters = ConversaoValor.ObterValorInt32(config, "minRequiredNonalphanumericCharacters", 1, true, 128);

            _HashAlgorithmType = config["hashAlgorithmType"];
            if (String.IsNullOrEmpty(_HashAlgorithmType))
            {
                _HashAlgorithmType = "SHA1";
            }

            _PasswordStrengthRegularExpression = config["passwordStrengthRegularExpression"];
            if (_PasswordStrengthRegularExpression != null)
            {
                _PasswordStrengthRegularExpression = _PasswordStrengthRegularExpression.Trim();
                if (_PasswordStrengthRegularExpression.Length != 0)
                {
                    try
                    {
                        Regex regex = new Regex(_PasswordStrengthRegularExpression);
                    }
                    catch (ArgumentException e)
                    {
                        throw new ProviderException(e.Message, e);
                    }
                }
            }
            else
            {
                _PasswordStrengthRegularExpression = string.Empty;
            }

            _AppName = config["applicationName"];
            if (string.IsNullOrEmpty(_AppName))
                _AppName = ConversaoValor.ObterDefaultAppName();

            if (_AppName.Length > 255)
            {
                throw new ProviderException("Provider application name is too long, max length is 255.");
            }

            string strTemp = config["passwordFormat"];
            if (strTemp == null)
                strTemp = "Hashed";

            switch (strTemp)
            {
                case "Clear":
                    _PasswordFormat = MembershipPasswordFormat.Clear;
                    break;
                case "Encrypted":
                    _PasswordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Hashed":
                    _PasswordFormat = MembershipPasswordFormat.Hashed;
                    break;
                default:
                    throw new ProviderException("Bad password format");
            }

            if (_PasswordFormat == MembershipPasswordFormat.Hashed && _EnablePasswordRetrieval)
                throw new ProviderException("Provider cannot retrieve hashed password");

            /*Não precisa estamos usando DataSet             
            _DatabaseFileName = config["connectionStringName"];
            if (_DatabaseFileName == null || _DatabaseFileName.Length < 1)
                throw new ProviderException("Connection name not specified");


            string temp = AccessConnectionHelper.GetFileNameFromConnectionName(_DatabaseFileName, true);
            if (temp == null || temp.Length < 1)
                throw new ProviderException("Connection string not found: " + _DatabaseFileName);
            _DatabaseFileName = temp;

            // Make sure connection is good
            AccessConnectionHelper.CheckConnectionString(_DatabaseFileName);
             */

            config.Remove("connectionStringName");
            config.Remove("enablePasswordRetrieval");
            config.Remove("enablePasswordReset");
            config.Remove("requiresQuestionAndAnswer");
            config.Remove("applicationName");
            config.Remove("requiresUniqueEmail");
            config.Remove("maxInvalidPasswordAttempts");
            config.Remove("passwordAttemptWindow");
            config.Remove("passwordFormat");
            config.Remove("name");
            config.Remove("description");
            config.Remove("minRequiredPasswordLength");
            config.Remove("minRequiredNonalphanumericCharacters");
            config.Remove("passwordStrengthRegularExpression");
            config.Remove("hashAlgorithmType");
            config.Remove("inRequiredPasswordLength");
            if (config.Count > 0)
            {
                string attribUnrecognized = config.GetKey(0);
                if (!String.IsNullOrEmpty(attribUnrecognized))
                    throw new ProviderException("Provider unrecognized attribute: " + attribUnrecognized);
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            //TODO: Adicionar Criptografia na senha
            // password = CriptSenha.EmCript(password);

            BdUtil bdUtil = new BdUtil(@"SELECT * 
  FROM Permissao.USUARIO 
 WHERE Login = @Login AND
       Senha = @Senha");
            bdUtil.AdicionarParametro("@Login", DbType.String, 50, username);
            bdUtil.AdicionarParametro("@Senha", DbType.AnsiString, 4000, password);
            DataTable loginTable = bdUtil.ObterDataTable();
            if (loginTable.Rows.Count != 1)
            {
                return false;
            }
            else
            {
                DataRow login = loginTable.Rows[0];
                string ipCliente = HttpContext.Current.Request.UserHostAddress;
                string ipProxy = ConfigurationManager.AppSettings["IPProxy"];
                AuthenticationTicket.Criar((int)login["IDUsuario"], (string)login["Login"], (string)login["Login"], ipCliente == ipProxy);
                return true;
            }
        }

        public override MembershipUser CreateUser(string username, string
          password, string email, string passwordQuestion, string
          passwordAnswer, bool isApproved, object providerUserKey,
          out MembershipCreateStatus status)
        {
            throw new NotSupportedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { return _EnablePasswordRetrieval; }
        }

        public override bool EnablePasswordReset
        {
            get { return _EnablePasswordReset; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return _RequiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return _RequiresUniqueEmail; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _PasswordFormat; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _MaxInvalidPasswordAttempts; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _PasswordAttemptWindow; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _MinRequiredPasswordLength; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _MinRequiredNonalphanumericCharacters; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _PasswordStrengthRegularExpression; }
        }

        public override string ApplicationName
        {
            get { return _AppName; }
            set
            {
                if (_AppName != value)
                {
                    //_ApplicationId = 0;
                    _AppName = value;
                }
            }
        }

        public override MembershipUser GetUser(string username,
            bool userIsOnline)
        {
            throw new NotSupportedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex,
            int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotSupportedException();
        }

        public override bool ChangePassword(string username,
            string oldPassword, string newPassword)
        {
            throw new NotSupportedException();
        }

        public override bool
            ChangePasswordQuestionAndAnswer(string username,
            string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotSupportedException();
        }

        public override bool DeleteUser(string username,
            bool deleteAllRelatedData)
        {
            throw new NotSupportedException();
        }

        public override MembershipUserCollection
            FindUsersByEmail(string emailToMatch, int pageIndex,
            int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override MembershipUserCollection
            FindUsersByName(string usernameToMatch, int pageIndex,
            int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser GetUser(object providerUserKey,
            bool userIsOnline)
        {
            throw new NotSupportedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotSupportedException();
        }

        public override string ResetPassword(string username,
            string answer)
        {
            throw new NotSupportedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotSupportedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotSupportedException();
        }
    }
}