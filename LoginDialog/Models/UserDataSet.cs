namespace LoginDialog.Models
{
    #region Referenceing

    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    #endregion

    [Serializable, XmlRoot("userdataset")]
    public class UserDataSet
    {
        [XmlArray("users"), XmlArrayItem("user")]
        public List<User> Users { get; set; }
    }
}