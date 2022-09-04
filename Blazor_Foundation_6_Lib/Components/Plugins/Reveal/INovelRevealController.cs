using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Reveal
{
    public interface INovelRevealController
    {
        Task<string> Register(string element, RenderFragment fragment, Func<Task> callback);
        Task SimpleMessageAsync(string title, string message);
        /// <summary>
        /// Complex Message with a Automatic closing condition (useful for loading reveal)
        /// </summary>
        /// <param name="id">Unique identifier (recommended: System.Guid.NewGuid().ToString() to ensure uniqueness) </param>
        /// <param name="title">Reveal's Title</param>
        /// <param name="message">Fragment Message</param>
        /// <param name="hasConditionMet">Delegate with Bool return value. True = Ready to close reveal (Warning: use lightweight operation as it is called every 100ms)</param>
        /// <returns></returns>
        Task ComplexMessageCloseConditional(string id, string title, RenderFragment message, Func<Task<bool>> hasConditionMet);

        Task<int> SimpleTwoWayAnswerMessageAsync(string id, string title, string message, string option1Label, string option2label,
        Func<Task<bool>> option1Clbk = null, Func<Task<bool>> option2Clbk = null, string option1style = null, string option2style = null,
        string titleIcon = null, string optionwrapperstyle = "justify-content:flex-end;");

        Task<int> ComplexTwoWayAnswerMessageAsync(string id, string title, RenderFragment message, string option1Label, string option2label,
        Func<Task<bool>> option1Clbk = null, Func<Task<bool>> option2Clbk = null, string option1style = null, string option2style = null,
        string titleIcon = null, string optionwrapperstyle = "justify-content:flex-end;");
    }
}
