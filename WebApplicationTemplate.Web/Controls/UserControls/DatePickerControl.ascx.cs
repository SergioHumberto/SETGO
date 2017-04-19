using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTemplate.Web.Controls.UserControls
{
    public partial class DatePickerControl : System.Web.UI.UserControl
    {
        public string Text { get; set; }
        public bool IsRequired { get; set; }
        public string ErrorMessage { get; set; }
        public bool SetFocusOnError { get; set; }

        public string Value
        {
            get { return datepicker.Text; }
            set { datepicker.Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            reqValidator.Enabled = IsRequired;
            reqValidator.ErrorMessage = ErrorMessage;
            reqValidator.ForeColor = System.Drawing.Color.Red;
            reqValidator.SetFocusOnError = SetFocusOnError;

            base.OnInit(e);
        }

    }
}