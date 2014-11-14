namespace LoginDialog
{
    #region Referenceing

    using Bindings;

    #endregion

    /// <summary>
    ///     Interaction logic for LoginWindow
    ///     
    ///     Note: To successfully log-in using this application
    ///         Username: admin
    ///         Password: nimda
    /// </summary>
    internal partial class LoginWindow
    {
        private LoginViewModel _viewModel;

        public LoginWindow()
        {
            InitializeComponent();
        }

        internal LoginViewModel ViewModel
        {
            get { return _viewModel ?? (_viewModel = new LoginViewModel()); }
        }
    }
}