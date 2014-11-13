namespace LoginDialog.Bindings
{
    #region Referenceing

    using System;
    using System.Linq;
    using System.Windows.Controls;
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
            if (obj == null)
                return;
            var passBox = (PasswordBox) obj;
            passBox.Password = null;
            ;
        }

        private void loginButtonCommandAction(object obj)
        {
            var passBox = (PasswordBox) obj;
            var requestingUser =
                App.UsersData.Users.FirstOrDefault(
                    user => user.Username.Equals(Username, StringComparison.CurrentCultureIgnoreCase));

            if (requestingUser == null)
                displayLoginFailure();
            else if (hashIsAuthenicated(requestingUser, passBox.Password))
                displayLoginSuccess();
            else
                displayLoginFailure();
        }

        #endregion

        #region ICommand Predicates

        private bool resetButtonCommandPredicate(object obj)
        {
            // Because we are not binding to Password we can not add it to the CanExecute predicate.
            return (!string.IsNullOrEmpty(Username));
        }

        private bool loginButtonCommandPredicate(object obj)
        {
            return (!string.IsNullOrEmpty(Username));
        }

        #endregion

        #region Functional

        private void displayLoginSuccess()
        {
            AuthenticationMessage = AuthenticationSuccessMessage;
        }

        private void displayLoginFailure()
        {
            AuthenticationFailed = true;
            AuthenticationMessage = AuthenticationFailedMessage;
            resetButtonCommandAction(null);
            AuthenticationFailed = false;
        }

        private bool hashIsAuthenicated(User requestingUser, string password)
        {
            return PasswordCryptography.ValidatePassword(password, requestingUser.Hash);
        }

        #endregion
    }
}