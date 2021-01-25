using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace FTH.Code
{
    public static class JsExtension
    {

        public static ValueTask BindPaypalButtons(this IJSRuntime js, string selector)
        {
            return js.InvokeVoidAsync("blazor.bindPaypalButton", selector);
        }

        public static ValueTask BindEventBriteFrame(this IJSRuntime js, string selector)
        {
            return js.InvokeVoidAsync("blazor.createEventBriteFrame", selector);
        }

    }
}
