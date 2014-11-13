namespace LoginDialog
{
    #region Referenceing

    using Bindings;

    #endregion

    /// <summary>
    ///     Interaction logic for LoginWindow
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