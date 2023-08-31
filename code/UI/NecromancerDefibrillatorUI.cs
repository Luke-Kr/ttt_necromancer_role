using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using Sandbox.Razor;
using Sandbox.UI;

namespace TerrorTown
{
    public class NecromancerDefibrillatorUI : Panel
    {
        public int Progress;

        public NecromancerDefibrillator OwnerDefib;

        public static Panel BarPart1 { get; set; }

        public static Panel BarPart2 { get; set; }

        protected override string GetRenderTreeChecksum()
        {
            return "8417aeeaf74247286f11d973c8c0b8b6739c0b5a";
        }

        protected override void BuildRenderTree(RenderTreeBuilder __builder)
        {
            __builder.AddLocation("E:\\S&box Addons\\3tTx3\\code\\TerrorTown\\Equipment\\DefibrillatorUI.razor", 8, 0);
            __builder.AddStyleDefinitions(0, "\r\n\t.root { \r\n\t\tbottom: -86px;\r\n\t\twidth: 100%;\r\n\t\tjustify-content: center;\r\n\t\talign-items: center;\r\n\t\talign-self: center;\r\n\t\tfont-family: Poppins-SemiBold;\r\n\t\tcolor: white;\r\n\t\tz-index: 950;\r\n\t}\r\n\r\n\t.defibpanel {  \r\n\t\toverflow: hidden;\r\n\t\ttext-stroke: 4px rgba(64,64,64,0.7);   \r\n\t\tbackground-color: rgba(16,16,16,0.4);\r\n\t\tbackdrop-filter: blur(32px);\r\n\t\tmax-width:300px;\r\n\t}\r\n\t.bartext { \r\n\t\ttext-stroke: 4px rgba(64,64,64,0.7);\r\n\t\twidth:300px;\r\n\t\ttext-transform: uppercase;\r\n\t\theight: 48px;\r\n\t\tposition: absolute;\r\n\t\tcolor: white;\r\n\t\tfont-family: Poppins-SemiBold;\r\n\t\tfont-weight: 800;\r\n\t\tbottom: 0px;\r\n\t\tpadding: 8px;\r\n\t\talign-items: center;\r\n\t\tjustify-content:space-between;\r\n\t}\r\n\t.bardesc {\r\n\t\tfont-size: 24px;\r\n\t}\r\n\t.value {\r\n\t\tfont-size: 32px;\r\n\t}\r\n\t.bar1 {\r\n\t\tcolor: white;\r\n\t\tbackground-color: rgba(16,16,16,0.4);\r\n\t\theight: 48px;\r\n\t}\r\n\r\n\t.bar2 { \r\n\t\tcolor: white;\r\n\t\theight: 48px; \r\n\t}\r\n");
            __builder.AddLocation("E:\\S&box Addons\\3tTx3\\code\\TerrorTown\\Equipment\\DefibrillatorUI.razor", 57, 8);
            __builder.AddMarkupContent(1, "\r\n");
            __builder.AddLocation("E:\\S&box Addons\\3tTx3\\code\\TerrorTown\\Equipment\\DefibrillatorUI.razor", 58, 0);
            __builder.OpenElement(2, "root");
            __builder.AddAttribute(3, "class", "root");
            __builder.AddLocation("E:\\S&box Addons\\3tTx3\\code\\TerrorTown\\Equipment\\DefibrillatorUI.razor", 59, 1);
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "class", "simplepanel defibpanel");
            __builder.AddLocation("E:\\S&box Addons\\3tTx3\\code\\TerrorTown\\Equipment\\DefibrillatorUI.razor", 60, 2);
            __builder.OpenElement(6, "div");
            __builder.AddAttribute(7, "class", "bar");
            __builder.AddLocation("E:\\S&box Addons\\3tTx3\\code\\TerrorTown\\Equipment\\DefibrillatorUI.razor", 61, 3);
            __builder.OpenElement(8, "div");
            __builder.AddAttribute(9, "class", "bar1");
            __builder.AddReferenceCapture(10, BarPart1, delegate (Panel _v)
            {
                BarPart1 = _v;
            });
            __builder.CloseElement();
            __builder.AddLocation("E:\\S&box Addons\\3tTx3\\code\\TerrorTown\\Equipment\\DefibrillatorUI.razor", 61, 41);
            __builder.AddMarkupContent(11, "\r\n\t\t\t");
            __builder.AddLocation("E:\\S&box Addons\\3tTx3\\code\\TerrorTown\\Equipment\\DefibrillatorUI.razor", 62, 3);
            __builder.OpenElement(12, "div");
            __builder.AddAttribute(13, "class", "bar2");
            __builder.AddReferenceCapture(14, BarPart2, delegate (Panel _v)
            {
                BarPart2 = _v;
            });
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddLocation("E:\\S&box Addons\\3tTx3\\code\\TerrorTown\\Equipment\\DefibrillatorUI.razor", 63, 8);
            __builder.AddMarkupContent(15, "\r\n\t\t");
            __builder.AddLocation("E:\\S&box Addons\\3tTx3\\code\\TerrorTown\\Equipment\\DefibrillatorUI.razor", 64, 2);
            __builder.OpenElement(16, "div");
            __builder.AddAttribute(17, "class", "bartext");
            __builder.AddLocation("E:\\S&box Addons\\3tTx3\\code\\TerrorTown\\Equipment\\DefibrillatorUI.razor", 65, 3);
            __builder.AddMarkupContent(18, "<label class=\"bardesc\">Reviving...</label>\r\n\t\t\t");
            __builder.AddLocation("E:\\S&box Addons\\3tTx3\\code\\TerrorTown\\Equipment\\DefibrillatorUI.razor", 66, 3);
            __builder.OpenElement(19, "div");
            __builder.AddAttribute(20, "class", "value");
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 1);
            defaultInterpolatedStringHandler.AppendFormatted(Progress);
            defaultInterpolatedStringHandler.AppendLiteral(" %");
            __builder.AddContent(21, defaultInterpolatedStringHandler.ToStringAndClear());
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
        }

        public override void Tick()
        {
            base.Tick();
            BarPart1.Style.Width = MathF.Round(OwnerDefib.TimeSinceMouseDown.Relative.Clamp(0f, OwnerDefib.ChargeTime) / OwnerDefib.ChargeTime * 100f * 3f).CeilToInt();
            BarPart2.Style.Width = MathF.Round((1f - OwnerDefib.TimeSinceMouseDown.Relative.Clamp(0f, OwnerDefib.ChargeTime) / OwnerDefib.ChargeTime) * 100f * 3f).CeilToInt();
            Progress = MathF.Round(OwnerDefib.TimeSinceMouseDown.Relative.Clamp(0f, OwnerDefib.ChargeTime) / OwnerDefib.ChargeTime * 100f).CeilToInt();
            base.Style.Display = ((Progress <= 0) ? DisplayMode.None : DisplayMode.Flex);
        }

        protected override int BuildHash()
        {
            return HashCode.Combine(OwnerDefib.TimeSinceMouseDown.Relative);
        }
    }
}
