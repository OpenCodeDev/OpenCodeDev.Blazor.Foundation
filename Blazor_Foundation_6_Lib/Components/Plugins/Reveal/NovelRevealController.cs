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
using Microsoft.AspNetCore.Components.Rendering;
using static OpenCodeDev.Blazor.Foundation.Extensions.RenderFragmentExt;
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
                tree.OpenComponent<Containers.Reveal>(AutoIndex());
                tree.AddAttribute(AutoIndex(), nameof(Containers.Reveal.OnOpened), EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(AutoIndex(), nameof(Containers.Reveal.Id), elementGen);
                tree.AddAttribute(AutoIndex(), nameof(Containers.Reveal.OpenOnStart), true);
                tree.AddAttribute(AutoIndex(), nameof(Containers.Reveal.CloseOnClick), canclose);
                tree.AddAttribute(AutoIndex(), nameof(Containers.Reveal.CloseXButton), canclose);
                tree.AddAttribute(AutoIndex(), nameof(Containers.Reveal.Title), title);
                tree.AddAttribute(AutoIndex(), nameof(Containers.Reveal.ContentFunction), functionContent);
                tree.AddComponentReferenceCapture(AutoIndex(), (value) => { tReference = value as Containers.Reveal; });
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
                tree.OpenComponent<Containers.Reveal>(AutoIndex());
                tree.AddAttribute(AutoIndex(), "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(AutoIndex(), "Id", elementGen);
                tree.AddAttribute(AutoIndex(), "ref", tReference);
                
                tree.AddAttribute(AutoIndex(), "OpenOnStart", true);
                tree.AddAttribute(AutoIndex(), "CloseOnClick", false);
                tree.AddAttribute(AutoIndex(), "CloseXButton", false);
                tree.AddAttribute(AutoIndex(), "Title", title);
                tree.AddAttribute(AutoIndex(), "ChildContent", message);
                tree.AddComponentReferenceCapture(AutoIndex(), (value) => { tReference = value as Containers.Reveal; });
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
                tree.OpenComponent<Containers.Reveal>(AutoIndex());
                tree.AddAttribute(AutoIndex(), "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(AutoIndex(), "Id", elementGen);
                tree.AddAttribute(AutoIndex(), "OpenOnStart", true);
                tree.AddAttribute(AutoIndex(), "CloseOnClick", false);
                tree.AddAttribute(AutoIndex(), "CloseXButton", false);
                tree.AddAttribute(AutoIndex(), "PrimaryButtonTitle", option1Label);
                tree.AddAttribute(AutoIndex(), "SecondaryButtonTitle", option2label);
                tree.AddAttribute(AutoIndex(), "PrimaryButtonOnClickCT", async () => { return true; });
                tree.AddAttribute(AutoIndex(), "SecondaryButtonOnClickCT", async () => { value = currentValue; return true; });
                tree.AddAttribute(AutoIndex(), "Title", title);
                tree.AddAttribute(AutoIndex(), "ChildContent", (RenderFragment)((subtree) =>
                {
                    subtree.OpenElement(AutoIndex(), "input");
                    subtree.AddAttribute(AutoIndex(), "type", "text");
                    subtree.AddAttribute(AutoIndex(), "placeholder", "");
                    subtree.AddAttribute(AutoIndex(), "value", BindConverter.FormatValue(value));
                    subtree.AddAttribute(AutoIndex(), "oninput", EventCallback.Factory.CreateBinder(this, __value => value = __value, value));
                    subtree.CloseElement();
                }));
                tree.AddComponentReferenceCapture(AutoIndex(), (value) => { tReference = value as Containers.Reveal; });
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
                tree.OpenComponent<Containers.Reveal>(AutoIndex());
                tree.AddAttribute(AutoIndex(), "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(AutoIndex(), "Id", elementGen);
                tree.AddAttribute(AutoIndex(), "OpenOnStart", true);
                tree.AddAttribute(AutoIndex(), "CloseOnClick", false);
                tree.AddAttribute(AutoIndex(), "CloseXButton", false);
                tree.AddAttribute(AutoIndex(), "PrimaryButtonTitle", option1Label);
                tree.AddAttribute(AutoIndex(), "SecondaryButtonTitle", option2label);
                tree.AddAttribute(AutoIndex(), "PrimaryButtonOnClickCT", async () => { return true; });
                tree.AddAttribute(AutoIndex(), "SecondaryButtonOnClickCT", async () => { value = currentValue; return true; });
                tree.AddAttribute(AutoIndex(), "Title", title);
                tree.AddAttribute(AutoIndex(), "ChildContent", (RenderFragment)((subtree) =>
                {
                    subtree.OpenElement(AutoIndex(), "input");
                    subtree.AddAttribute(AutoIndex(), "type", "number");
                    subtree.AddAttribute(AutoIndex(), "placeholder", "");
                    subtree.AddAttribute(AutoIndex(), "min", minValue);
                    subtree.AddAttribute(AutoIndex(), "max", maxValue);
                    subtree.AddAttribute(AutoIndex(), "step", step);
                    subtree.AddAttribute(AutoIndex(), "value", BindConverter.FormatValue(value));
                    subtree.AddAttribute(AutoIndex(), "oninput", EventCallback.Factory.CreateBinder(this, __value => value = __value, value));
                    subtree.CloseElement();
                }));
                tree.AddComponentReferenceCapture(AutoIndex(), (value) => { tReference = value as Containers.Reveal; });
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
                tree.OpenComponent<Containers.Reveal>(AutoIndex());
                tree.AddAttribute(AutoIndex(), "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(AutoIndex(), "Id", elementGen);
                tree.AddAttribute(AutoIndex(), "OpenOnStart", true);
                tree.AddAttribute(AutoIndex(), "CloseOnClick", false);
                tree.AddAttribute(AutoIndex(), "CloseXButton", false);
                tree.AddAttribute(AutoIndex(), "PrimaryButtonTitle", option1Label);
                tree.AddAttribute(AutoIndex(), "SecondaryButtonTitle", option2label);
                tree.AddAttribute(AutoIndex(), "PrimaryButtonOnClickCT", async () => { return true; });
                tree.AddAttribute(AutoIndex(), "SecondaryButtonOnClickCT", async () => { value = currentValue; return true; });
                tree.AddAttribute(AutoIndex(), "Title", title);
                tree.AddAttribute(AutoIndex(), "ChildContent", (RenderFragment)((subtree) =>
                {
                    subtree.OpenElement(AutoIndex(), "textarea");
                    subtree.AddAttribute(AutoIndex(), "oninput", EventCallback.Factory.CreateBinder(this, __value => value = __value, value));
                    subtree.AddContent(AutoIndex(), value);
                    subtree.CloseElement();
                }));
                tree.AddComponentReferenceCapture(AutoIndex(), (value) => { tReference = value as Containers.Reveal; });
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
                tree.OpenComponent<Containers.Reveal>(AutoIndex());
                tree.AddAttribute(AutoIndex(), "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(AutoIndex(), "Id", elementGen);
                tree.AddAttribute(AutoIndex(), "OpenOnStart", true);
                tree.AddAttribute(AutoIndex(), "CloseOnClick", false);
                tree.AddAttribute(AutoIndex(), "CloseXButton", false);
                tree.AddAttribute(AutoIndex(), "PrimaryButtonTitle", option1Label);
                tree.AddAttribute(AutoIndex(), "SecondaryButtonTitle", option2label);
                tree.AddAttribute(AutoIndex(), "PrimaryButtonOnClickCT", async () => { return true; });
                tree.AddAttribute(AutoIndex(), "SecondaryButtonOnClickCT", async () => { value = currentValue; return true; });
                tree.AddAttribute(AutoIndex(), "Title", title);
                tree.AddAttribute(AutoIndex(), "ChildContent", (RenderFragment)((subtree) =>
                {
                    subtree.OpenComponent(AutoIndex(), typeof(Controls.SingleSlider));
                    subtree.AddAttribute(AutoIndex(), "Min", minValue);
                    subtree.AddAttribute(AutoIndex(), "Max", maxValue);
                    subtree.AddAttribute(AutoIndex(), "Step", step);
                    subtree.AddAttribute(AutoIndex(), "Value", value);
                    subtree.AddAttribute(AutoIndex(), "ValueChanged", EventCallback.Factory.Create(this, (float __value) => value = __value));
                    subtree.CloseComponent();
                }));
                tree.AddComponentReferenceCapture(AutoIndex(), (value) => { tReference = value as Containers.Reveal; });
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
                tree.OpenComponent<Containers.Reveal>(AutoIndex());
               
                tree.AddAttribute(AutoIndex(), "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(AutoIndex(), "Id", elementGen);
                tree.AddAttribute(AutoIndex(), "OpenOnStart", true);
                tree.AddAttribute(AutoIndex(), "CloseOnClick", false);
                tree.AddAttribute(AutoIndex(), "CloseXButton", false);
                tree.AddAttribute(AutoIndex(), "PrimaryButtonTitle", option1Label);
                tree.AddAttribute(AutoIndex(), "SecondaryButtonTitle", option2label);
                tree.AddAttribute(AutoIndex(), "PrimaryButtonOnClickCT", async () => { return true; });
                tree.AddAttribute(AutoIndex(), "SecondaryButtonOnClickCT", async () => { value = currentValue; return true; });
                tree.AddAttribute(AutoIndex(), "Title", title);
                tree.AddAttribute(AutoIndex(), "ChildContent", (RenderFragment)((subtree) =>
                {
                    subtree.OpenElement(AutoIndex(), "select");
                    subtree.AddAttribute(AutoIndex(), "onchange", 
                        EventCallback.Factory.CreateBinder(this, (string __value) => value = __value, value));
                    subtree.AddContent(AutoIndex(), (RenderFragment)((suboptions) =>
                    {
                        if (currentValue == null)
                        {
                            suboptions.OpenElement(AutoIndex(), "option");
                            suboptions.AddContent(AutoIndex(), noSelection);
                            suboptions.CloseElement();
                        }

                        foreach (var item in selectable)
                        {
                            suboptions.OpenElement(AutoIndex(), "option");
                            suboptions.AddAttribute(AutoIndex(), "value", item.Key);
                            if (currentValue != null && currentValue.Equals(item.Key))
                            {
                                suboptions.AddAttribute(AutoIndex(), "selected");

                            }
                            suboptions.AddContent(AutoIndex(), item.Value);
                            suboptions.CloseElement();
                        }
                    }));

                    subtree.CloseElement();
                }));
                tree.AddComponentReferenceCapture(AutoIndex(), (value) => { tReference = value as Containers.Reveal; });
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
                tree.OpenElement(AutoIndex(), "div");
                tree.AddContent(AutoIndex(), message);
                tree.CloseElement();
            });
            return await ComplexTwoAnswerMessage(title, fragment, option1Label, option2label, option1Clbk, 
                option2Clbk, option1style, option2style, 
                titleIcon, (id) => {
                    if (setId != null) setId.Invoke(id);
                }, optionwrapperstyle);
        }

        public async Task<string> InputSelectorFragment(string title, string currentValue, Func<Func<Containers.Reveal>, string, EventCallback<string>, RenderFragment> getFragmentSelector, string option1Label = "Confirm", string option2label = "Cancel", Action<string> setId = null)
        {
            string elementGen = System.Guid.NewGuid().HTMLCompliant().ToString();
            if (setId != null) setId.Invoke(elementGen);

            Containers.Reveal tReference = null; // Temporary Reference of Reveal

            string value = currentValue;
            RenderFragment fragment = new RenderFragment(tree =>
            {
                // (option1Clbk != null ? option1Clbk : async () => { selectedOption = 0; return true; })
                tree.OpenComponent<Containers.Reveal>(AutoIndex());

                tree.AddAttribute(AutoIndex(), "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(AutoIndex(), "Id", elementGen);
                tree.AddAttribute(AutoIndex(), "OpenOnStart", true);
                tree.AddAttribute(AutoIndex(), "CloseOnClick", false);
                tree.AddAttribute(AutoIndex(), "CloseXButton", false);
                tree.AddAttribute(AutoIndex(), "PrimaryButtonTitle", option1Label);
                tree.AddAttribute(AutoIndex(), "SecondaryButtonTitle", option2label);
                tree.AddAttribute(AutoIndex(), "PrimaryButtonOnClickCT", async () => { return true; });
                tree.AddAttribute(AutoIndex(), "SecondaryButtonOnClickCT", async () => { value = currentValue; return true; });
                tree.AddAttribute(AutoIndex(), "Title", title);
                tree.AddAttribute(AutoIndex(), "ChildContent",
                    getFragmentSelector(() => tReference, currentValue, EventCallback.Factory.Create(this, (string arg) => value = arg)));
                tree.AddComponentReferenceCapture(AutoIndex(), (value) => { tReference = value as Containers.Reveal; });
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
                tree.OpenComponent<Containers.Reveal>(AutoIndex());
                tree.AddAttribute(AutoIndex(), "OnOpened", EventCallback.Factory.Create(this, (string arg) => OnRevealOpenedCallback(arg)));
                tree.AddAttribute(AutoIndex(), "Id", elementGen);
                tree.AddAttribute(AutoIndex(), "OpenOnStart", true);
                tree.AddAttribute(AutoIndex(), "TitleIcon", titleIcon);
                tree.AddAttribute(AutoIndex(), "CloseOnClick", false);
                tree.AddAttribute(AutoIndex(), "CloseXButton", false);
                tree.AddAttribute(AutoIndex(), "PrimaryButtonTitle", option1Label);
                tree.AddAttribute(AutoIndex(), "PrimaryButtonStyle", option1style);
                tree.AddAttribute(AutoIndex(), "ButtonGroupStyle", optionwrapperstyle);
                tree.AddAttribute(AutoIndex(), "SecondaryButtonTitle", option2label);
                tree.AddAttribute(AutoIndex(), "SecondaryButtonStyle", option2style);
                tree.AddAttribute(AutoIndex(), "PrimaryButtonOnClickCT", (option1Clbk != null ? option1Clbk : async () => { selectedOption = 0; return true; })); 
                tree.AddAttribute(AutoIndex(), "SecondaryButtonOnClickCT", (option2Clbk != null ? option2Clbk : async () => { selectedOption = 1; return true; }));
                tree.AddAttribute(AutoIndex(), "Title", title);
                tree.AddAttribute(AutoIndex(), "ChildContent", message);
                tree.AddComponentReferenceCapture(AutoIndex(), (value) => { tReference = value as Containers.Reveal; });
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
