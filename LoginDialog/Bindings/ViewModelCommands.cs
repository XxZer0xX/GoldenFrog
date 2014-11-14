namespace LoginDialog.Bindings
{
    #region Referenceing

    using System;
    using System.Linq;
    using System.Security;
    using System.Windows.Input;
    using Cryptogrophy;
    using Models;

    #endregion

    /// <summary>
    ///     Login ViewModel. Contains Commands and Command Methods
    /// </summary>
    internal partial class LoginViewModel
    {
        #region ICommands

        private ICommand _loginButtonCommand;
        private ICommand _resetButtonCommand;


        public ICommand ResetButtonCommand
        {
            get
            {
                return _resetButtonCommand ??
                       (_resetButtonCommand = new RelayCommand(resetButtonCommandAction, resetButtonCommandPredicate));
            }
        }

        public ICommand LoginButtonCommand
        {
            get
            {
                return _loginButtonCommand ??
                       (_loginButtonCommand = new RelayCommand(loginButtonCommandAction, loginButtonCommandPredicate));
            }
        }

        public ICommand LoginFailureCommand
        {
            get { return new RelayCommand(s => { }); }
        }

        #endregion

        #region ICommand Actions

        private void resetButtonCommandAction(object obj)
        {
            Username = null;
            Password = null;
            _authenticationSuccess = false;
        }

        private void loginButtonCommandAction(object obj)
        {
            var requestingUser =
                App.UsersData.Users.FirstOrDefault(
                    user => user.Username.Equals(Username, StringComparison.CurrentCultureIgnoreCase));

            if (requestingUser == null)
                displayLoginFailure();
            else if (hashIsAuthenicated(requestingUser, Password))
            {
                displayLoginSuccess();
            }
            else
                displayLoginFailure();
        }

        #endregion

        #region ICommand Predicates

        private bool resetButtonCommandPredicate(object obj)
        {
            // Because we are not binding to Password we can not add it to the CanExecute predicate.
            return (!string.IsNullOrEmpty(Username) || Password != null);
        }

        private bool loginButtonCommandPredicate(object obj)
        {
            return (!string.IsNullOrEmpty(Username) && !_authenticationSuccess);
        }

        #endregion

        #region Functional

        private void displayLoginSuccess()
        {
            AuthenticationMessage = AuthenticationSuccessMessage;
            Password = null;
            _authenticationSuccess = true;
        }

        private void displayLoginFailure()
        {
            AuthenticationFailed = true;
            AuthenticationMessage = AuthenticationFailedMessage;
            resetButtonCommandAction(null);
            AuthenticationFailed = false;
        }

        private bool hashIsAuthenicated(User requestingUser, SecureString password)
        {
            using (password)
                return PasswordCryptography.ValidatePassword(password, requestingUser.Hash);
        }

        #endregion
    }
}