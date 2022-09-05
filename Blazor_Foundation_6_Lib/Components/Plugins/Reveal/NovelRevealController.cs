using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Web;
using OpenCodeDev.Blazor.Foundation.Components.Containers;
using OpenCodeDev.Blazor.Foundation.Extensions;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Reveal
{
    public class NovelRevealController : INovelRevealController
    {
        public DotNetObjectReference<NovelReveal> NovelRevealCtrlDotNet { get; set; }

        public Action OnStateChanged { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        public NovelRevealController(IJSRuntime js) => JS = js;

        /// <summary>
        /// Currently Active Reveals
        /// </summary>
        public Dictionary<RenderFragment, Func<Task>> CurrentItems { get; set; } = new Dictionary<RenderFragment, Func<Task>>();
        
        /// <summary>
        /// List of Reveal (Non-Active and Currently-Active)
        /// </summary>
        public Dictionary<RenderFragment, Func<Task>> Registry { get; set; } = new Dictionary<RenderFragment, Func<Task>>();

        /// <summary>
        /// Track reveal fragment with id.
        /// </summary>
        public Dictionary<string, RenderFragment> RegistryTracker { get; set; } = new Dictionary<string, RenderFragment>();

        /// <summary>
        /// Reveal Reference Storage;
        /// </summary>
        public Dictionary<string, Containers.Reveal> RegistryReference { get; set; } = new Dictionary<string, Containers.Reveal>();




        private async Task OnRevealOpenedCallback(string element)
        {
            
            await JS.InvokeVoidAsync("NovelRevealOnClosedListener", NovelRevealCtrlDotNet, element);
        }


        public async Task<string> Register(string element, RenderFragment fragment, Func<Task> callback)
        {
            if(JS == null) throw new ArgumentNullException("JS Runtime is not defined for Novel Reveal Controller.");
            if (RegistryTracker.ContainsKey(element)) { throw new Exception("Error while generating new id"); }
            // Define a new instance
            RenderFragment clone = (RenderFragment)fragment.Clone();
            if (!Registry.ContainsKey(clone))
            {
                Registry.Add(clone, callback);
                RegistryTracker.Add(element, clone);
                RegistryReference.Add(element, null); // Default Null until Reveal is called
                                                      //Console.WriteLine("Register Successfull");
                return element;
            }
            else { throw new Exception("Fragment Register must be unique probably a problem during cloning."); }
        }
        public async Task ComplexMessage(string title, RenderFragment message, Func<Task> onCloseCallback = null, bool canclose = true, Action<string> setId = null)
        {
            string elementGen = System.Guid.NewGuid().HTMLCompliant().ToString();
            if(setId != null) setId.Invoke(elementGen);
            Containers.Reveal tReference = null; // Temporary Reference of Reveal
            RenderFragment fragment = new RenderFragment(tree =>
            {
                tree.OpenComponent<Containers.Reveal>(0);
                tree.AddAttribute(1, "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(2, "Id", elementGen);
                tree.AddAttribute(4, "OpenOnStart", true);
                tree.AddAttribute(4, "CloseOnClick", canclose);
                tree.AddAttribute(4, "CloseXButton", canclose);
                tree.AddAttribute(5, "Title", title);
                tree.AddAttribute(6, "ChildContent", message);
                tree.AddComponentReferenceCapture(7, (value) => { tReference = value as Containers.Reveal; });
                tree.CloseComponent();
            });

            string element = await Register(elementGen, fragment, onCloseCallback);
            RegistryReference[elementGen] = tReference; // Set reference to reveal as global access;

            CurrentItems.Add(RegistryTracker[element], Registry[RegistryTracker[element]]);
            if (OnStateChanged != null) OnStateChanged.Invoke();

        }

        public async Task ComplexMessageCloseConditional(string title, RenderFragment message, Func<Task<bool>> hasConditionMet, Action<string> setId = null)
        {
            string elementGen = System.Guid.NewGuid().HTMLCompliant().ToString();
            if(setId != null) setId.Invoke(elementGen);
            Containers.Reveal tReference = null; // Temporary Reference of Reveal
            RenderFragment fragment = new RenderFragment(tree =>
            {
                tree.OpenComponent<Containers.Reveal>(0);
                tree.AddAttribute(1, "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(2, "Id", elementGen);
                tree.AddAttribute(3, "ref", tReference);
                
                tree.AddAttribute(4, "OpenOnStart", true);
                tree.AddAttribute(4, "CloseOnClick", false);
                tree.AddAttribute(4, "CloseXButton", false);
                tree.AddAttribute(5, "Title", title);
                tree.AddAttribute(6, "ChildContent", message);
                tree.AddComponentReferenceCapture(7, (value) => { tReference = value as Containers.Reveal; });
                tree.CloseComponent();
            });
            string element = await Register(elementGen, fragment, null);
            RegistryReference[elementGen] = tReference; // Set reference to reveal as global access;
            CurrentItems.Add(RegistryTracker[element], Registry[RegistryTracker[element]]);
            if(OnStateChanged != null) OnStateChanged.Invoke();

            await Task.Run(async () =>
            {
                bool close = false;
                do
                {
                    await Task.Delay(100);
                    close = await hasConditionMet.Invoke();
                } while (!close);
                await tReference.TriggerClose();
            });
        }

        public async Task SimpleMessage(string title, string message, Action<string> setId = null)
        {
            string elementGen = System.Guid.NewGuid().HTMLCompliant().ToString();
            if(setId != null) setId.Invoke(elementGen);

            Containers.Reveal tReference = null; // Temporary Reference of Reveal
            MarkupString messageHTML = (MarkupString)message;
            RenderFragment msg = new RenderFragment(tree =>
            {
                tree.OpenElement(0, "div");
                tree.AddContent(1, messageHTML);
                tree.CloseElement();
            });
            RenderFragment fragment = new RenderFragment(tree =>
            {
                tree.OpenComponent<Containers.Reveal>(0);
                tree.AddAttribute(1, "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(2, "Id", elementGen);
                tree.AddAttribute(4, "OpenOnStart", true);
                tree.AddAttribute(5, "Title", title);
                tree.AddAttribute(6, "ChildContent", msg);
                tree.AddComponentReferenceCapture(7, (value) => { tReference = value as Containers.Reveal; });
                tree.CloseComponent();
            });

            string element = await Register(elementGen, fragment, null);
            RegistryReference[elementGen] = tReference; // Set reference to reveal as global access;

            CurrentItems.Add(RegistryTracker[element], Registry[RegistryTracker[element]]);
            if(OnStateChanged != null) OnStateChanged.Invoke();

            // Await until message is closed
            await Task.Run(async () => {
                try
                {
                    do
                    {
                        await Task.Delay(100);
                    } while (CurrentItems.ContainsKey(RegistryTracker[element]));
                }
                catch
                {
                    // Ignore because error = reveal probably close
                }
            });
        }


        public async Task<int> TwoAnswerMessage(string title, string message, string option1Label, string option2label,
    Func<Task<bool>> option1Clbk = null, Func<Task<bool>> option2Clbk = null, string option1style = null, string option2style = null,
    string titleIcon = null, Action<string> setId = null, string optionwrapperstyle = "justify-content:flex-end;")
        {
            Console.WriteLine(message);
            RenderFragment fragment = new RenderFragment(tree =>
            {
                tree.OpenElement(0, "div");
                tree.AddContent(1, message);
                tree.CloseElement();
            });
            return await ComplexTwoAnswerMessage(title, fragment, option1Label, option2label, option1Clbk, 
                option2Clbk, option1style, option2style, 
                titleIcon, (id) => {
                    if (setId != null) setId.Invoke(id);
                }, optionwrapperstyle);
        }


        public async Task<int> ComplexTwoAnswerMessage(string title, RenderFragment message, string option1Label, string option2label,
 Func<Task<bool>> option1Clbk = null, Func<Task<bool>> option2Clbk = null, string option1style = null, string option2style = null,
 string titleIcon = null, Action<string> setId = null, string optionwrapperstyle = "justify-content:flex-end;")
        {
            string elementGen = System.Guid.NewGuid().HTMLCompliant().ToString();
            if(setId != null) setId.Invoke(elementGen);

            Containers.Reveal tReference = null; // Temporary Reference of Reveal

            int selectedOption = 2;
            RenderFragment fragment = new RenderFragment(tree =>
            {
                // (option1Clbk != null ? option1Clbk : async () => { selectedOption = 0; return true; })
                tree.OpenComponent<Containers.Reveal>(0);
                tree.AddAttribute(1, "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(2, "Id", elementGen);
                tree.AddAttribute(4, "OpenOnStart", true);
                tree.AddAttribute(5, "TitleIcon", titleIcon);
                tree.AddAttribute(6, "CloseOnClick", false);
                tree.AddAttribute(7, "CloseXButton", false);
                tree.AddAttribute(8, "PrimaryButtonTitle", option1Label);
                tree.AddAttribute(9, "PrimaryButtonStyle", option1style);
                tree.AddAttribute(10, "ButtonGroupStyle", optionwrapperstyle);
                tree.AddAttribute(11, "SecondaryButtonTitle", option2label);
                tree.AddAttribute(12, "SecondaryButtonStyle", option2style);
                tree.AddAttribute(13, "PrimaryButtonOnClickCT", (option1Clbk != null ? option1Clbk : async () => { selectedOption = 0; return true; })); 
                tree.AddAttribute(14, "SecondaryButtonOnClickCT", (option2Clbk != null ? option2Clbk : async () => { selectedOption = 1; return true; }));
                tree.AddAttribute(15, "Title", title);
                tree.AddAttribute(17, "ChildContent", message);
                tree.AddComponentReferenceCapture(18, (value) => { tReference = value as Containers.Reveal; });
                tree.CloseComponent();
            });

            string element = await Register(elementGen, fragment, null);
            CurrentItems.Add(RegistryTracker[element], Registry[RegistryTracker[element]]);
            if(OnStateChanged != null) OnStateChanged.Invoke();

            // Await until message is closed
            await Task.Run(async () => {
                try
                {
                    do
                    {
                        await Task.Delay(100);
                    } while (CurrentItems.ContainsKey(RegistryTracker[element]));
                }
                catch
                {
                    // Ignore because error = reveal probably close
                }
            });
            return selectedOption;
        }


    }
}
