using NotePad.Plugins.Common;
using System;

namespace NotePad.Plugins
{
    public class ToErrorConverter : MarshalByRefObject, INotePadPlagin
    {
        public string TransformText(string text)
        {
            throw new NotImplementedException("Ahahaha! It's a error from plugin for notepad!");
        }
    }
}
