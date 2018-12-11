using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.Common
{
    public class AppStyles
    {
        public ResourceDictionary LoadAppControlStyles()
        {
            ResourceDictionary resources = new ResourceDictionary();

            var gridViewLayout = new Style(typeof(Grid))
            {
                Setters =
                {
                    new Setter {Property=Grid.HorizontalOptionsProperty,Value=LayoutOptions.FillAndExpand},
                    new Setter {Property=Grid.VerticalOptionsProperty,Value=LayoutOptions.FillAndExpand}
                }
            };
            var customButtonViewLayout = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter {Property=Button.MarginProperty,Value=ConfigConstants.THICKNESS},
                    new Setter {Property=Button.CornerRadiusProperty,Value=ConfigConstants.BUTTON_RADIOUS},
                    new Setter {Property=Button.HeightRequestProperty,Value=ConfigConstants.BUTTON_HEIGHT},
                    new Setter {Property=Button.HorizontalOptionsProperty,Value=LayoutOptions.FillAndExpand},
                    new Setter {Property=Button.VerticalOptionsProperty,Value=LayoutOptions.End},
                    new Setter {Property=Button.BackgroundColorProperty,Value=Color.FromHex(ConfigConstants.BUTTON_BACKGROUND_COLOR)},
                    new Setter {Property=Button.TextColorProperty,Value=Color.FromHex(ConfigConstants.BUTTON_TEXT_COLOR)},
                    new Setter {Property=Button.FontSizeProperty,Value=ConfigConstants.FONT_SIZE_WITH_NORMAL},
                    new Setter {Property=Button.TextProperty,Value=ConfigConstants.LOGIN_BUTTON_TEXT}
                }
            };
            var customLabelViewLayout = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property=Label.HorizontalOptionsProperty,Value=LayoutOptions.FillAndExpand},
                    new Setter {Property=Label.VerticalOptionsProperty,Value=LayoutOptions.FillAndExpand},
                    new Setter {Property=Label.VerticalTextAlignmentProperty,Value=TextAlignment.Center}
                }
            };
            var customTextFieldViewLayout = new Style(typeof(Entry))
            {
                Setters =
                {
                    new Setter {Property=Entry.HorizontalOptionsProperty,Value=LayoutOptions.FillAndExpand},
                    new Setter {Property=Entry.VerticalOptionsProperty,Value=LayoutOptions.FillAndExpand}
                }
            };
            resources.Add(ConfigConstants.TEXTFIELD_VIEW_LAYOUT, customTextFieldViewLayout);
            resources.Add(ConfigConstants.GRID_VIEW_LAYOUT, gridViewLayout);
            resources.Add(ConfigConstants.BUTTON_VIEW_LAYOUT, customButtonViewLayout);
            resources.Add(ConfigConstants.LABEL_VIEW_LAYOUT, customLabelViewLayout);
            return resources;
        }
    }
}
