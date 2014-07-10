namespace CoreWeb.Sys.Web.UI
{
    /// <summary>
    /// Base class to be inherited from by all WebForms pages.
    /// </summary>
    public abstract class PageBase : System.Web.UI.Page
    {
        /// <summary>
        /// Title tag value to be rendered in the &lt;head&gt;.
        /// </summary>
        /// <remarks>
        /// Hides the default implementation which requires &lt;head runat="server"&gt;.
        /// </remarks>
        public new string Title;

        /// <summary>
        /// Css Class rendered into the body tag.
        /// </summary>
        public string BodyCssClass;

        /// <summary>
        /// Author meta tag value to be rendered in the head tag.
        /// </summary>
        public string MetaAuthor;

        /// <summary>
        /// Description meta tag value to be rendered in the head tag.
        /// </summary>
        /// <remarks>
        /// Hides the default implementation which requires &lt;head runat="server"&gt;.
        /// </remarks>
        public new string MetaDescription;

        /// <summary>
        /// Keywords meta tag value to be rendered in the head tag.
        /// </summary>
        /// <remarks>
        /// Hides the default implementation which requires &lt;head runat="server"&gt;.
        /// </remarks>
        public new string MetaKeywords;

        // Meta tags
        public string MetaLastModified { get; set; }
        public string MetaExpires { get; set; }
        public string Robots { get; set; }
        public string MetaCopyright { get; set; }
        public string MetaPublishedData { get; set; }
        public string MetaCanonical { get; set; }

        // Open Graph tags
        public string MetaOGTitle { get; set; }
        public string MetaOGDescription { get; set; }
        public string MetaOGSiteName { get; set; }
        public string MetaOGType { get; set; }
        public string MetaOGUrl { get; set; }
        public string MetaOGImage { get; set; }

        /// <summary>
        /// Creates a new instance of LayoutBase.
        /// </summary>
        public PageBase()
            : base()
        {

        }
    }
}