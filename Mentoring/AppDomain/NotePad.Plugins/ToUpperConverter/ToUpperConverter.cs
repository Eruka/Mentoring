using NotePad.Plugins.Common;
using System;

namespace NotePad.Plugins
{
    public class ToUpperConverter : MarshalByRefObject, INotePadPlagin
    {
        public string TransformText(string text)
        {
            return text.ToUpper();
        }
    }
}
