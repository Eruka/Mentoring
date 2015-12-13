using NotePad.Plugins.Common;
using System;
using System.Text;

namespace NotePad.Plugins
{
    public class ToMadConverter : MarshalByRefObject, INotePadPlagin
    {
        public string TransformText(string text)
        {
            var stringBuilder = new StringBuilder();
            for(var i = 0; i < text.Length; i++){
                stringBuilder.Append(text[i]).Append(i);
            }
            return stringBuilder.ToString();
        }
    }
}
