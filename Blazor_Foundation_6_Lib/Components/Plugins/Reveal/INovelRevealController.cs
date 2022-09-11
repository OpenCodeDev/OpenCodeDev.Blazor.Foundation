﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Reveal
{
    public interface INovelRevealController
    {
        DotNetObjectReference<NovelReveal> NovelRevealCtrlDotNet { get; set; }
        Action OnStateChanged { get; set; }
        Dictionary<RenderFragment, Func<Task>> CurrentItems { get; set; }
        Dictionary<RenderFragment, Func<Task>> Registry { get; set; }
        Dictionary<string, RenderFragment> RegistryTracker { get; set; }
        Dictionary<string, Containers.Reveal> RegistryReference { get; set; }
        Task SimpleMessage(string title, string message, Action<string> setId = null);
        Task ComplexMessageCloseConditional(string title, RenderFragment message, Func<Task<bool>> hasConditionMet, Action<string> setId = null);
        Task ComplexMessage(string title, RenderFragment message, Func<Task> onCloseCallback = null, bool canclose = true, Action<string> setId = null);
        Task<int> TwoAnswerMessage(string title, string message, string option1Label, string option2label,
            Func<Task<bool>> option1Clbk = null, Func<Task<bool>> option2Clbk = null, string option1style = null, string option2style = null,
            string titleIcon = null, Action<string> setId = null, string optionwrapperstyle = "justify-content:flex-end;");
        Task<int> ComplexTwoAnswerMessage(string title, RenderFragment message, string option1Label, string option2label,
             Func<Task<bool>> option1Clbk = null, Func<Task<bool>> option2Clbk = null, string option1style = null, string option2style = null,
             string titleIcon = null, Action<string> setId = null, string optionwrapperstyle = "justify-content:flex-end;");

        Task<string> InputText(string title, string currentValue, string option1Label = "Confirm", string option2label = "Cancel", Action<string> setId = null);
        Task<string> InputTextArea(string title, string currentValue, string option1Label = "Confirm", string option2label = "Cancel", Action<string> setId = null);

        Task<float> InputSliderFloat(string title, float currentValue, float minValue, float maxValue, float step, string option1Label = "Confirm", string option2label = "Cancel", Action<string> setId = null);
        Task<int> InputSliderInt(string title, int currentValue, int minValue, int maxValue, int step, string option1Label = "Confirm", string option2label = "Cancel", Action<string> setId = null);
        Task<T> InputSelect<T>(string title, T currentValue, Dictionary<T, string> selectable, string option1Label = "Confirm", string option2label = "Cancel", Action<string> setId = null);

        Task<int> InputIntNumber(string title, int currentValue, int minValue, int maxValue, int step, string option1Label = "Confirm", string option2label = "Cancel", Action<string> setId = null);
        Task<float> InputFloatNumber(string title, float currentValue, float minValue, float maxValue, float step, string option1Label = "Confirm", string option2label = "Cancel", Action<string> setId = null);
    }
}
