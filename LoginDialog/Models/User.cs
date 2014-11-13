namespace LoginDialog.Models
{
    #region Referenceing

    using System;
    using System.Xml.Serialization;

    #endregion

    [Serializable]
    public class User
    {
        [XmlAttribute("username")]
        public string Username { get; set; }

        [XmlElement("hash")]
        public string Hash { get; set; }
    }
}