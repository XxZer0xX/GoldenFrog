namespace LoginDialog
{
    #region Referenceing

    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Threading;
    using System.Xml.Serialization;
    using Cryptogrophy;
    using Models;
    using Properties;

    #endregion

    /// <summary>
    ///     Interaction logic for App
    /// </summary>
    public partial class App
    {
        private const string _defaultUserName = "admin";
        private string _dataPath;
        public static UserDataSet UsersData { get; set; }

        /// <summary>
        ///     DataPath for Users stored
        /// 
        ///     The default user
        ///         Username: admin
        ///         Password: nimda
        /// </summary>
        private string dataPath
        {
            get
            {
                return _dataPath ?? (_dataPath = string.Format(@"{0}\{1}",
                    Path.Combine(
                        Path.GetDirectoryName(
                            Assembly.GetExecutingAssembly().Location)
                        , Settings.Default.DATAPATH), "RegisteredUsers.xml"));
            }
        }

        public App()
        {
            DispatcherUnhandledException += onDispatcherUnhandledException;
            loadUserData();
        }

        /// <summary>
        ///     Load User data or create it if it doesn't exist.
        /// </summary>
        private void loadUserData()
        {
            if (!Directory.Exists(Path.GetDirectoryName(dataPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(dataPath));

            if (!File.Exists(dataPath))
            {
                using (var stream = new FileStream(dataPath, FileMode.CreateNew, FileAccess.Write))
                {
                    using (var writer = new StreamWriter(stream))
                        writer.WriteLine(
                            "<?xml version=\"1.0\"?>\n<userdataset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"></userdataset>");
                }
            }

            using (var stream = new FileStream(dataPath, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof (UserDataSet));
                UsersData = serializer.Deserialize(stream) as UserDataSet;
                if (!UsersData.Users.Any())
                    createDefaultUser(stream);
            }
        }

        /// <summary>
        ///     Display Unhandled exceptions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void onDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            MessageBox.Show(MainWindow, string.Format(Settings.Default.UNHANDLEDEXC, args.Exception.Message));
        }

        /// <summary>
        ///     Create the default user and write it to the data file.
        /// </summary>
        /// <param name="fileStream"></param>
        private void createDefaultUser(Stream fileStream)
        {
            UsersData.Users.Add(new User
            {
                Username = _defaultUserName,
                Hash = PasswordCryptography.CreateHash(Settings.Default.PASSWORD)
            });

            fileStream.Position = 0;
            var serializer = new XmlSerializer(typeof (UserDataSet));
            serializer.Serialize(fileStream, UsersData);
        }
    }
}