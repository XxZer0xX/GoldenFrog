namespace LoginDialog.Helpers
{
    #region Referenceing

    using System.Security;
    using System.Windows;
    using System.Windows.Controls;

    #endregion

    /// <summary>
    ///     work around for binding the Password property of a PasswordBox
    /// </summary>
    public static class PasswordBoxHelper
    {
        public static readonly DependencyProperty BoundPassword =
            DependencyProperty.RegisterAttached("BoundPassword", typeof (SecureString), typeof (PasswordBoxHelper),
                new PropertyMetadata(null, OnBoundPasswordChanged));

        public static readonly DependencyProperty BindPassword = DependencyProperty.RegisterAttached(
            "BindPassword", typeof (bool), typeof (PasswordBoxHelper),
            new PropertyMetadata(false, OnBindPasswordChanged));

        private static readonly DependencyProperty UpdatingPassword =
            DependencyProperty.RegisterAttached("UpdatingPassword", typeof (bool), typeof (PasswordBoxHelper),
                new PropertyMetadata(false));

        private static void OnBoundPasswordChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs args)
        {
            var box = dependencyObject as PasswordBox;

            if (dependencyObject == null || !GetBindPassword(dependencyObject))
            {
                return;
            }

            // avoid recursive updating by ignoring the box's changed event
            box.PasswordChanged -= HandlePasswordChanged;

            var newPassword = (SecureString) args.NewValue;

            if (!GetUpdatingPassword(box))
            {
                box.Password = (newPassword == null) ? string.Empty : newPassword.ToString();
            }

            box.PasswordChanged += HandlePasswordChanged;
        }

        private static void OnBindPasswordChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs args)
        {
            var passwordBox = dependencyObject as PasswordBox;

            if (passwordBox == null)
                return;

            var wasBound = (bool) (args.OldValue);
            var needToBind = (bool) (args.NewValue);

            if (wasBound)
                passwordBox.PasswordChanged -= HandlePasswordChanged;

            if (needToBind)
                passwordBox.PasswordChanged += HandlePasswordChanged;
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs args)
        {
            var box = sender as PasswordBox;
            SetUpdatingPassword(box, true);
            SetBoundPassword(box, box.SecurePassword);
            SetUpdatingPassword(box, false);
        }

        public static void SetBindPassword(DependencyObject dependancyProperty, bool value)
        {
            dependancyProperty.SetValue(BindPassword, value);
        }

        public static bool GetBindPassword(DependencyObject dependancyProperty)
        {
            return (bool) dependancyProperty.GetValue(BindPassword);
        }

        public static string GetBoundPassword(DependencyObject dependancyProperty)
        {
            return (string) dependancyProperty.GetValue(BoundPassword);
        }

        public static void SetBoundPassword(DependencyObject dependancyProperty, SecureString value)
        {
            dependancyProperty.SetValue(BoundPassword, value);
        }

        private static bool GetUpdatingPassword(DependencyObject dependancyProperty)
        {
            return (bool) dependancyProperty.GetValue(UpdatingPassword);
        }

        private static void SetUpdatingPassword(DependencyObject dependancyProperty, bool value)
        {
            dependancyProperty.SetValue(UpdatingPassword, value);
        }
    }
}