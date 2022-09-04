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
        public event Action OnStateChanged;

        /// <summary>
        /// Currently Active Reveals
        /// </summary>
        public Dictionary<RenderFragment, Func<Task>> CurrentItems { get; set; } = new Dictionary<RenderFragment, Func<Task>>();
        /// <summary>
        /// List of Reveal (Non-Active and Currently-Active)
        /// </summary>
        public Dictionary<RenderFragment, Func<Task>> Registry = new Dictionary<RenderFragment, Func<Task>>();

        /// <summary>
        /// Track reveal fragment with id.
        /// </summary>
        public Dictionary<string, RenderFragment> RegistryTracker = new Dictionary<string, RenderFragment>();

        /// <summary>
        /// Reveal Reference Storage;
        /// </summary>
        public Dictionary<string, Containers.Reveal> RegistryReference { get; private set; } = new Dictionary<string, Containers.Reveal>();

        [Inject]
        IJSRuntime JS { get; set; }

        public async Task<string> Register(string element, RenderFragment fragment, Func<Task> callback)
        {
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

        public async Task ComplexMessageCloseConditional(string id, string title, RenderFragment message, Func<Task<bool>> hasConditionMet)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ComplexTwoWayAnswerMessageAsync(string id, string title, RenderFragment message, string option1Label, string option2label, Func<Task<bool>> option1Clbk = null, Func<Task<bool>> option2Clbk = null, string option1style = null, string option2style = null, string titleIcon = null, string optionwrapperstyle = "justify-content:flex-end;")
        {
            throw new NotImplementedException();
        }



        private async Task OnRevealOpenedCallback(string element)
        {
            await JS.InvokeVoidAsync("RevealOnClosedListener", element);
        }


        public async Task SimpleMessageAsync(string title, string message)
        {
            string elementGen = System.Guid.NewGuid().HTMLCompliant().ToString();

            Containers.Reveal tReference = null; // Temporary Reference of Reveal
            MarkupString messageHTML = (MarkupString)message;
            RenderFragment fragment = new RenderFragment(tree =>
            {
                tree.OpenComponent<Containers.Reveal>(0);
                tree.AddAttribute(1, "OnOpened", EventCallback.Factory.Create(this, (arg) => OnRevealOpenedCallback((string)arg)));
                tree.AddAttribute(2, "Id", elementGen);
                tree.AddAttribute(3, "ref", tReference);
                tree.AddAttribute(4, "OpenOnStart", true);
                tree.AddAttribute(5, "Title", title);
                tree.AddContent(6, @messageHTML);
                tree.CloseComponent();
            });

            string element = await Register(elementGen, fragment, null);
            RegistryReference[elementGen] = tReference; // Set reference to reveal as global access;

            CurrentItems.Add(RegistryTracker[element], Registry[RegistryTracker[element]]);
            OnStateChanged?.Invoke();

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

        public async Task<int> SimpleTwoWayAnswerMessageAsync(string id, string title, string message, string option1Label, string option2label, Func<Task<bool>> option1Clbk = null, Func<Task<bool>> option2Clbk = null, string option1style = null, string option2style = null, string titleIcon = null, string optionwrapperstyle = "justify-content:flex-end;")
        {
            throw new NotImplementedException();
        }
    }
}
