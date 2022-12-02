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
            await ComplexMessage(title, async (item)=> message, onCloseCallback, canclose, setId);

        }

        public async Task ComplexMessage(string title, Func<Containers.Reveal, Task<RenderFragment>> functionContent, Func<Task> onCloseCallback = null, bool canclose = true, Action<string> setId = null)
        {
            string elementGen = System.Guid.NewGuid().HTMLCompliant().ToString();
            if (setId != null) setId.Invoke(elementGen);
            Containers.Reveal tReference = null; // Temporary Reference of Reveal

            RenderFragment fragment = new RenderFragment(tree =>
            {
                tree.OpenComponent<Containers.Reveal>(0);
                tree.AddAttribute(1, nameof(Containers.Reveal.OnOpened), EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(2, nameof(Containers.Reveal.Id), elementGen);
                tree.AddAttribute(3, nameof(Containers.Reveal.OpenOnStart), true);
                tree.AddAttribute(4, nameof(Containers.Reveal.CloseOnClick), canclose);
                tree.AddAttribute(5, nameof(Containers.Reveal.CloseXButton), canclose);
                tree.AddAttribute(6, nameof(Containers.Reveal.Title), title);
                tree.AddAttribute(7, nameof(Containers.Reveal.ContentFunction), functionContent);
                tree.AddComponentReferenceCapture(8, (value) => { tReference = value as Containers.Reveal; });
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

        public async Task<string> InputText(string title, string currentValue, string option1Label = "Confirm", string option2label = "Cancel", Action<string> setId = null)
        {
            string elementGen = System.Guid.NewGuid().HTMLCompliant().ToString();
            if (setId != null) setId.Invoke(elementGen);

            Containers.Reveal tReference = null; // Temporary Reference of Reveal

            string value = currentValue;
            RenderFragment fragment = new RenderFragment(tree =>
            {
                // (option1Clbk != null ? option1Clbk : async () => { selectedOption = 0; return true; })
                tree.OpenComponent<Containers.Reveal>(0);
                tree.AddAttribute(1, "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(2, "Id", elementGen);
                tree.AddAttribute(4, "OpenOnStart", true);
                tree.AddAttribute(6, "CloseOnClick", false);
                tree.AddAttribute(7, "CloseXButton", false);
                tree.AddAttribute(8, "PrimaryButtonTitle", option1Label);
                tree.AddAttribute(11, "SecondaryButtonTitle", option2label);
                tree.AddAttribute(13, "PrimaryButtonOnClickCT", async () => { return true; });
                tree.AddAttribute(14, "SecondaryButtonOnClickCT", async () => { value = currentValue; return true; });
                tree.AddAttribute(15, "Title", title);
                tree.AddAttribute(17, "ChildContent", (RenderFragment)((subtree) =>
                {
                    subtree.OpenElement(0, "input");
                    subtree.AddAttribute(1, "type", "text");
                    subtree.AddAttribute(2, "placeholder", "");
                    subtree.AddAttribute(3, "value", BindConverter.FormatValue(value));
                    subtree.AddAttribute(4, "oninput", EventCallback.Factory.CreateBinder(this, __value => value = __value, value));
                    subtree.CloseElement();
                }));
                tree.AddComponentReferenceCapture(18, (value) => { tReference = value as Containers.Reveal; });
                tree.CloseComponent();
            });

            string element = await Register(elementGen, fragment, null);
            CurrentItems.Add(RegistryTracker[element], Registry[RegistryTracker[element]]);
            if (OnStateChanged != null) OnStateChanged.Invoke();

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
            return value;
        }
        public async Task<int> InputIntNumber(string title, int currentValue, int minValue, int maxValue, int step, string option1Label = "Confirm", string option2label = "Cancel", Action<string> setId = null)
        {
            return (int)await InputFloatNumber(title, (float)currentValue, (float)minValue, (float)maxValue, (float)step, option1Label, option2label, setId);
        }
        public async Task<float> InputFloatNumber(string title, float currentValue, float minValue, float maxValue, float step, string option1Label = "Confirm", string option2label = "Cancel", Action<string> setId = null)
        {
            string elementGen = System.Guid.NewGuid().HTMLCompliant().ToString();
            if (setId != null) setId.Invoke(elementGen);

            Containers.Reveal tReference = null; // Temporary Reference of Reveal

            float value = currentValue;
            RenderFragment fragment = new RenderFragment(tree =>
            {
                // (option1Clbk != null ? option1Clbk : async () => { selectedOption = 0; return true; })
                tree.OpenComponent<Containers.Reveal>(0);
                tree.AddAttribute(1, "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(2, "Id", elementGen);
                tree.AddAttribute(4, "OpenOnStart", true);
                tree.AddAttribute(6, "CloseOnClick", false);
                tree.AddAttribute(7, "CloseXButton", false);
                tree.AddAttribute(8, "PrimaryButtonTitle", option1Label);
                tree.AddAttribute(11, "SecondaryButtonTitle", option2label);
                tree.AddAttribute(13, "PrimaryButtonOnClickCT", async () => { return true; });
                tree.AddAttribute(14, "SecondaryButtonOnClickCT", async () => { value = currentValue; return true; });
                tree.AddAttribute(15, "Title", title);
                tree.AddAttribute(17, "ChildContent", (RenderFragment)((subtree) =>
                {
                    subtree.OpenElement(0, "input");
                    subtree.AddAttribute(1, "type", "number");
                    subtree.AddAttribute(2, "placeholder", "");
                    subtree.AddAttribute(2, "min", minValue);
                    subtree.AddAttribute(2, "max", maxValue);
                    subtree.AddAttribute(2, "step", step);
                    subtree.AddAttribute(3, "value", BindConverter.FormatValue(value));
                    subtree.AddAttribute(4, "oninput", EventCallback.Factory.CreateBinder(this, __value => value = __value, value));
                    subtree.CloseElement();
                }));
                tree.AddComponentReferenceCapture(18, (value) => { tReference = value as Containers.Reveal; });
                tree.CloseComponent();
            });

            string element = await Register(elementGen, fragment, null);
            CurrentItems.Add(RegistryTracker[element], Registry[RegistryTracker[element]]);
            if (OnStateChanged != null) OnStateChanged.Invoke();

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
            return value;
        }
        public async Task<string> InputTextArea(string title, string currentValue, string option1Label = "Confirm", string option2label = "Cancel", Action<string> setId = null)
        {
            string elementGen = System.Guid.NewGuid().HTMLCompliant().ToString();
            if (setId != null) setId.Invoke(elementGen);

            Containers.Reveal tReference = null; // Temporary Reference of Reveal

            string value = currentValue;
            RenderFragment fragment = new RenderFragment(tree =>
            {
                // (option1Clbk != null ? option1Clbk : async () => { selectedOption = 0; return true; })
                tree.OpenComponent<Containers.Reveal>(0);
                tree.AddAttribute(1, "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(2, "Id", elementGen);
                tree.AddAttribute(4, "OpenOnStart", true);
                tree.AddAttribute(6, "CloseOnClick", false);
                tree.AddAttribute(7, "CloseXButton", false);
                tree.AddAttribute(8, "PrimaryButtonTitle", option1Label);
                tree.AddAttribute(11, "SecondaryButtonTitle", option2label);
                tree.AddAttribute(13, "PrimaryButtonOnClickCT", async () => { return true; });
                tree.AddAttribute(14, "SecondaryButtonOnClickCT", async () => { value = currentValue; return true; });
                tree.AddAttribute(15, "Title", title);
                tree.AddAttribute(17, "ChildContent", (RenderFragment)((subtree) =>
                {
                    subtree.OpenElement(0, "textarea");
                    subtree.AddAttribute(3, "oninput", EventCallback.Factory.CreateBinder(this, __value => value = __value, value));
                    subtree.AddContent(4, value);
                    subtree.CloseElement();
                }));
                tree.AddComponentReferenceCapture(18, (value) => { tReference = value as Containers.Reveal; });
                tree.CloseComponent();
            });

            string element = await Register(elementGen, fragment, null);
            CurrentItems.Add(RegistryTracker[element], Registry[RegistryTracker[element]]);
            if (OnStateChanged != null) OnStateChanged.Invoke();

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
            return value;
        }
        public async Task<float> InputSliderFloat(string title, float currentValue, float minValue, float maxValue, float step, string option1Label = "Confirm", string option2label = "Cancel", Action<string> setId = null)
        {
            return await InputSlider(title, currentValue, minValue, maxValue, step, option1Label, option2label, setId);
        }
        public async Task<int> InputSliderInt(string title,  int currentValue, int minValue, int maxValue, int step, string option1Label = "Confirm", string option2label = "Cancel", Action<string> setId = null)
        {
            return (int) await InputSlider(title, (float)currentValue, (float)minValue, (float)maxValue, (float)step, option1Label, option2label, setId);
        }
        public async Task<float> InputSlider(string title, float currentValue, float minValue, float maxValue, float step, string option1Label = "Confirm", string option2label = "Cancel", Action<string> setId = null) 
        {
            string elementGen = System.Guid.NewGuid().HTMLCompliant().ToString();
            if (setId != null) setId.Invoke(elementGen);

            Containers.Reveal tReference = null; // Temporary Reference of Reveal

            float value = currentValue;
            RenderFragment fragment = new RenderFragment(tree =>
            {
                // (option1Clbk != null ? option1Clbk : async () => { selectedOption = 0; return true; })
                tree.OpenComponent<Containers.Reveal>(0);
                tree.AddAttribute(1, "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(2, "Id", elementGen);
                tree.AddAttribute(4, "OpenOnStart", true);
                tree.AddAttribute(6, "CloseOnClick", false);
                tree.AddAttribute(7, "CloseXButton", false);
                tree.AddAttribute(8, "PrimaryButtonTitle", option1Label);
                tree.AddAttribute(11, "SecondaryButtonTitle", option2label);
                tree.AddAttribute(13, "PrimaryButtonOnClickCT", async () => { return true; });
                tree.AddAttribute(14, "SecondaryButtonOnClickCT", async () => { value = currentValue; return true; });
                tree.AddAttribute(15, "Title", title);
                tree.AddAttribute(17, "ChildContent", (RenderFragment)((subtree) =>
                {
                    subtree.OpenComponent(0, typeof(Controls.SingleSlider));
                    subtree.AddAttribute(1, "Min", minValue);
                    subtree.AddAttribute(2, "Max", maxValue);
                    subtree.AddAttribute(3, "Step", step);
                    subtree.AddAttribute(4, "Value", value);
                    subtree.AddAttribute(5, "ValueChanged", EventCallback.Factory.Create(this, (float __value) => value = __value));
                    subtree.CloseComponent();
                }));
                tree.AddComponentReferenceCapture(18, (value) => { tReference = value as Containers.Reveal; });
                tree.CloseComponent();
            });

            string element = await Register(elementGen, fragment, null);
            CurrentItems.Add(RegistryTracker[element], Registry[RegistryTracker[element]]);
            if (OnStateChanged != null) OnStateChanged.Invoke();

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
            return value;
        }
        public async Task<string> InputSelect(string title, string currentValue, Dictionary<string, string> selectable,string noSelection = "No Selection", string option1Label = "Confirm", string option2label = "Cancel", Action<string> setId = null)
        {
            string elementGen = System.Guid.NewGuid().HTMLCompliant().ToString();
            if (setId != null) setId.Invoke(elementGen);

            Containers.Reveal tReference = null; // Temporary Reference of Reveal

            string value = currentValue;
            RenderFragment fragment = new RenderFragment(tree =>
            {
                // (option1Clbk != null ? option1Clbk : async () => { selectedOption = 0; return true; })
                tree.OpenComponent<Containers.Reveal>(0);
               
                tree.AddAttribute(2, "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(3, "Id", elementGen);
                tree.AddAttribute(4, "OpenOnStart", true);
                tree.AddAttribute(5, "CloseOnClick", false);
                tree.AddAttribute(6, "CloseXButton", false);
                tree.AddAttribute(7, "PrimaryButtonTitle", option1Label);
                tree.AddAttribute(8, "SecondaryButtonTitle", option2label);
                tree.AddAttribute(9, "PrimaryButtonOnClickCT", async () => { return true; });
                tree.AddAttribute(10, "SecondaryButtonOnClickCT", async () => { value = currentValue; return true; });
                tree.AddAttribute(11, "Title", title);
                tree.AddAttribute(12, "ChildContent", (RenderFragment)((subtree) =>
                {
                    subtree.OpenElement(0, "select");
                    subtree.AddAttribute(2, "onchange", 
                        EventCallback.Factory.CreateBinder(this, (string __value) => value = __value, value));
                    subtree.AddContent(3, (RenderFragment)((suboptions) =>
                    {
                        if (currentValue == null)
                        {
                            suboptions.OpenElement(0, "option");
                            suboptions.AddContent(3, noSelection);
                            suboptions.CloseElement();
                        }

                        foreach (var item in selectable)
                        {
                            suboptions.OpenElement(0, "option");
                            suboptions.AddAttribute(1, "value", item.Key);
                            if (currentValue != null && currentValue.Equals(item.Key))
                            {
                                suboptions.AddAttribute(2, "selected");

                            }
                            suboptions.AddContent(3, item.Value);
                            suboptions.CloseElement();
                        }
                    }));

                    subtree.CloseElement();
                }));
                tree.AddComponentReferenceCapture(13, (value) => { tReference = value as Containers.Reveal; });
                tree.CloseComponent();
            });

            string element = await Register(elementGen, fragment, null);
            CurrentItems.Add(RegistryTracker[element], Registry[RegistryTracker[element]]);
            if (OnStateChanged != null) OnStateChanged.Invoke();

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
            return value;
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
