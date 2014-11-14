namespace LoginDialog.Bindings
{
    #region Referenceing

    using System.Security;
    using Properties;

    #endregion

    /// <summary>
    ///     Login ViewModel, Contains Properties
    /// </summary>
    internal sealed partial class LoginViewModel : ViewModelBase
    {
        private bool _authenticationFailed;
        private bool _authenticationSuccess;
        private string _authenticationFailedMessage;
        private string _authenticationMessage;
        private string _authenticationSuccessMessage;
        private string _loginButtonText;
        private string _resetButtonText;
        private string _username;
        private string _windowTitle;


        #region unused

        private SecureString _password;

        public SecureString Password
        {
            get { return _password; }
            set { SetField(ref _password, value); }
        }

        #endregion

        #region  Bound Properties

        public bool AuthenticationFailed
        {
            get { return _authenticationFailed; }
            set { SetField(ref _authenticationFailed, value); }
        }

        public string AuthenticationMessage
        {
            get { return _authenticationMessage; }
            set { SetField(ref _authenticationMessage, value); }
        }

        public string WindowTitle
        {
            get { return _windowTitle ?? (_windowTitle = Settings.Default.WINDOWTITLE); }
        }

        public string Username
        {
            get { return _username; }
            set { SetField(ref _username, value); }
        }

        #endregion

        public string AuthenticationSuccessMessage
        {
            get
            {
                return _authenticationSuccessMessage ?? (_authenticationSuccessMessage = Settings.Default.AUTHSUCCESS);
            }
        }

        public string AuthenticationFailedMessage
        {
            get { return _authenticationFailedMessage ?? (_authenticationFailedMessage = Settings.Default.AUTHFAILED); }
        }

        public string LoginButtonText
        {
            get
            {
                return _loginButtonText ?? (_loginButtonText = Settings.Default.LOGINBUTTONTEXT);
                ;
            }
        }

        public string ResetButtonText
        {
            get
            {
                return _resetButtonText ?? (_resetButtonText = Settings.Default.RESETBUTTONTEXT);
                ;
            }
        }
    }
}