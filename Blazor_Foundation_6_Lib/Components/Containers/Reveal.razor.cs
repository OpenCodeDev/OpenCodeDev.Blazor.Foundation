﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OpenCodeDev.Blazor.Foundation.Extensions;

namespace OpenCodeDev.Blazor.Foundation.Components.Containers
{
    public partial class Reveal : ComponentBase, IDisposable
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter] 
        public Func<Reveal, Task<RenderFragment>> ContentFunction { get; set;}

        
        /// <summary>
        /// Unique HTML Identifier.
        /// </summary>
        [Parameter]
        public string Id { get; set; }

        /// <summary>
        /// Custom Class to Append at the end of default Foundation Class.
        /// </summary>
        [Parameter]
        public string Class { get; set; }


        /// <summary>
        /// fade-in, slide-in-up, slide-in-left, slide-in-down, slide-in-right
        /// hinge-in-from-top, hinge-in-from-right, hinge-in-from-up, hinge-in-from-down
        /// hinge-in-from-middle-x, hinge-in-from-middle-y, scale-in-up, scale-in-down
        /// spin-out, spin-out-ccw
        /// </summary>
        [Parameter]
        public string AnimationIn { get; set; } // fade-in, slide-in-down

        /// <summary>
        /// fade-out, slide-out-up, slide-out-left, slide-out-down, slide-out-right
        /// hinge-out-from-top, hinge-out-from-right, hinge-out-from-up, hinge-out-from-down
        /// hinge-out-from-middle-x, hinge-out-from-middle-y, scale-out-up, scale-out-down
        /// spin-out, spin-out-ccw
        /// </summary>
        [Parameter]
        public string AnimationOut { get; set; }

        /// <summary>
        /// True: Close when click outside modal (Default: True)
        /// </summary>
        [Parameter]
        public bool CloseOnClick { get; set; } = true;

        /// <summary>
        /// False to hive the close button inside the reveal (check also CloseOnClick if you tryin' to disable closing of the reveal)
        /// </summary>
        [Parameter]
        public bool CloseXButton { get; set; } = true;
        /// <summary>
        /// Define Custom Class for Close Button
        /// </summary>
        [Parameter]
        public string CloseClass { get; set; }

        /// <summary>
        /// Define Window Title
        /// </summary>
        [Parameter]
        public string Title { get; set; }

        /// <summary>
        /// Title Icon if not define no icon will be display.
        /// mdi-XXXXX
        /// <br/><a href="https://pictogrammers.github.io/@mdi/font/5.4.55/">Possible Options</a>
        /// </summary>
        [Parameter]
        public string TitleIcon { get; set; }

        /// <summary>
        /// You can put any CSS inline style for the button wrapper.<br/>
        /// Default: justify-content:flex-end;
        /// </summary>
        [Parameter]
        public string ButtonGroupStyle { get; set; } = "justify-content:flex-end";

        /// <summary>
        /// Unique HTML Identifier.
        /// </summary>
        [Parameter]
        public string PrimaryButtonId { get; set; }

        /// <summary>
        /// Custom Class to Append at the end of default Foundation Class.
        /// </summary>
        [Parameter]
        public string PrimaryButtonClass { get; set; } = "primary right";

        /// <summary>
        /// Inline css XXX:XXX; XXT:XXO;
        /// </summary>
        [Parameter]
        public string PrimaryButtonStyle { get; set; }

        /// <summary>
        /// id of Foundation element to toggle can be anything. Modal, Switch, Off-Canvas....
        /// </summary>
        [Parameter]
        public string PrimaryButtonDataToggle { get; set; }

        /// <summary>
        /// Called when Primary button is clicked. <br/>
        /// Note: if you need to do an operation to force closing the reveal based on the result and without external TriggerClose(), use <b>PrimaryButtonOnclickCT</b>
        /// </summary>
        [Parameter]
        public Func<Task> PrimaryButtonOnClick { get; set; } = null;

        /// <summary>
        /// Primary Button Onclick with Close Trigger True/False <br/>
        /// True = Page is ready to automatically close. False = do not close just yet
        /// </summary>
        [Parameter]
        public Func<Task<bool>> PrimaryButtonOnClickCT { get; set; } = null;

        /// <summary>
        /// Title of the Primary Button;
        /// </summary>
        [Parameter]
        public string PrimaryButtonTitle { get; set; } = null;

        /// <summary>
        /// Unique HTML Identifier.
        /// </summary>
        [Parameter]
        public string SecondaryButtonId { get; set; }

        /// <summary>
        /// Custom Class to Append at the end of default Foundation Class.
        /// </summary>
        [Parameter]
        public string SecondaryButtonClass { get; set; } = "secondary right";

        /// <summary>
        /// Inline css XXX:XXX; XXT:XXO;
        /// </summary>
        [Parameter]
        public string SecondaryButtonStyle { get; set; }

        /// <summary>
        /// id of Foundation element to toggle can be anything. Modal, Switch, Off-Canvas....
        /// </summary>
        [Parameter]
        public string SecondaryButtonDataToggle { get; set; }

        /// <summary>
        /// Called when secondary button is clicked. <br/>
        /// Note: if you need to do an operation to force closing the reveal based on the result and without external TriggerClose(), use <b>SecondaryButtonOnclickCT</b>
        /// </summary>
        [Parameter]
        public Func<Task> SecondaryButtonOnClick { get; set; }

        /// <summary>
        /// Secondary Button Onclick with Close Trigger True/False <br/>
        /// True = Page is ready to automatically close. False = do not close just yet
        /// </summary>
        [Parameter]
        public Func<Task<bool>> SecondaryButtonOnClickCT { get; set; } = null;

        /// <summary>
        /// Title of the Secondary Button;
        /// </summary>
        [Parameter]
        public string SecondaryButtonTitle { get; set; } = null;

        /// <summary>
        /// Default: true <br/>
        /// True: Background Overlay Visible
        /// </summary>
        [Parameter]
        public bool Overlay { get; set; } = true;

        /// <summary>
        /// True: Will Trigger Foundation Script Registration Automatically Without any Options.
        /// </summary>
        [Parameter]
        public bool AutoManaged { get; set; } = true;

        /// <summary>
        /// List of Options to pass when initilization is handled by Blazor. Leave blank if AutoManaged = false.
        /// </summary>
        [Parameter]
        public Dictionary<string, object> DataOptions { get; set; }

        /// <summary>
        /// Default: false <br/>
        /// Set to true if you wish to open the reveal on initialized.
        /// </summary>
        [Parameter]
        public bool OpenOnStart { get; set; } = false;


        [Parameter]
        public EventCallback<string> OnOpened { get; set; }


        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        protected override void OnInitialized()
        {
            if (Id == null) { Id = Guid.NewGuid().HTMLCompliant().ToString(); }
            if (PrimaryButtonOnClickCT == null)
            {
                Console.WriteLine("The Primary Callback is null");
            }
            if (CloseClass == null) { CloseClass = "close-button"; }


        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (AutoManaged)
                {
                    await JSRuntime.InvokeVoidAsync("RevealRegister", Id, DataOptions != null ? DataOptions.ToJSObjectString() : null);
                    if (OpenOnStart)
                    {
                        await TriggerOpen(); // Open Automatically
                    }
                }
            }

        }


        public void Dispose()
        {
            if (AutoManaged)
            {
                JSRuntime.InvokeVoidAsync("FoundationDestroy", Id);
            }
        }

        /// <summary>
        /// Change the Title Dynamically
        /// </summary>
        public void ChangeTitle(string title)
        {
            Title = title;
        }

        /// <summary>
        /// Change Title Icon Dynamically
        /// </summary>
        /// <param name="icon"></param>
        public void ChangeTitleIcon(string icon)
        {
            TitleIcon = icon;
        }


        private async Task OnSecondaryClick()
        {
            if (SecondaryButtonOnClick == null && SecondaryButtonOnClickCT == null)
            {
                await TriggerClose();
                return;
            }

            if (SecondaryButtonOnClick != null)
            {
                await SecondaryButtonOnClick.Invoke();
            }

            if (SecondaryButtonOnClickCT != null)
            {
                bool close = await SecondaryButtonOnClickCT.Invoke();
                if (close)
                {
                    await TriggerClose();
                }
            }

        }

        private async Task OnPrimaryClick()
        {
            if (PrimaryButtonOnClick == null && PrimaryButtonOnClickCT == null)
            {
                await TriggerClose();
                return;
            }
            if (PrimaryButtonOnClick != null)
            {
                await PrimaryButtonOnClick.Invoke();
            }

            if (PrimaryButtonOnClickCT != null)
            {
                bool close = await PrimaryButtonOnClickCT.Invoke();
                if (close)
                {
                    await TriggerClose();
                }
            }

        }

        /// <summary>
        /// Toggle Panel On/Off (Warning: Might Break Grid Cell (Maybe Blazor Issue?)
        /// </summary>
        public async Task TriggerToggle()
        {
            await JSRuntime.InvokeVoidAsync("ElementToggle", Id);
        }

        /// <summary>
        /// Toggle Panel On (Warning: Might Break Grid Cell (Maybe Blazor Issue?)
        /// </summary>
        public async Task TriggerOpen()
        {
            await JSRuntime.InvokeVoidAsync("ElementOpen", Id);
            await OnOpened.InvokeAsync(Id);
        }

        /// <summary>
        /// Toggle Panel Off (Warning: Might Break Grid Cell (Maybe Blazor Issue?)
        /// </summary>
        public async Task TriggerClose()
        {
            await JSRuntime.InvokeVoidAsync("ElementClose", Id);
        }
    }
}
