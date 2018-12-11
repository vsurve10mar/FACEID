using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.Common
{
    public class ConfigConstants
    {
        public static Thickness THICKNESS = new Thickness(20, 0, 20, 10);

        public static object BUTTON_RADIOUS = 15;
        public static object BUTTON_HEIGHT = 40;
        public const string BUTTON_BACKGROUND_COLOR = "#41b9f0";
        public const string BUTTON_TEXT_COLOR = "#FFFFFF";
        public const double FONT_SIZE_WITH_BOLD = 20;
        public const double FONT_SIZE_WITH_NORMAL = 14;
        public const double FONT_SIZE_WITH_SMALL = 10;
        public const double FONT_SIZE_WITH_EXTRA_BOLD = 16;
        public const string LOGIN_BUTTON_TEXT = "Login";

        public static string GRID_VIEW_LAYOUT = "GridView";
        public static string BUTTON_VIEW_LAYOUT = "ButtonViewLayout";

        public static string LABEL_VIEW_LAYOUT = "customLabelViewLayout";

        public static string TEXTFIELD_VIEW_LAYOUT = "customTextFieldViewLayout";

        public static string USERNAME = "UserName";
        public static string PASSWORD = "Password";
    }
    public enum LocalAuthType
    {
        None,
        Passcode,
        TouchId,
        FaceId
    }
}
