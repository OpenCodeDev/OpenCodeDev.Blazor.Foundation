using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Reflection;
namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.InfiniteScrollHelper
{
    public interface IInfiniteLoadHelper
    {
        /// <summary>
        /// Called whenever the scroll reaches the end.
        /// </summary>
        Func<Task> OnScrollEndReached { get; set; }


        /// <summary>
        /// Initialize the Listener of Scroll Events.
        /// </summary>
        /// <returns></returns>
        void Init(int offset);

    }

    public class InfiniteLoadHelper : IInfiniteLoadHelper
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly IJSInProcessRuntime _jsInProcessRuntime;
        private readonly string _assemblyName;

        protected static InfiniteLoadHelper _instance { get; set; }
        public Func<Task> OnScrollEndReached { get; set; }
        public InfiniteLoadHelper(IJSRuntime JS)
        {
            _assemblyName = Assembly.GetExecutingAssembly().FullName;
            _jsRuntime = JS;
            _jsInProcessRuntime = _jsRuntime as IJSInProcessRuntime;

            //Console.WriteLine($"Initialized InifiteLoadHelper in Assembly: {_assemblyName}");
        }        
        
        /// <summary>
        /// Offset from the bottom to detect the end of scroll
        /// </summary>
        /// <param name="offset"></param>
        public void Init(int offset)
        {
            if (_instance != null)
            { throw new Exception("You cannot initialise 2 InfiniteLoadHelper;"); }
            _instance = this;
            //_jsInProcessRuntime.InvokeVoid("BlazorFoundationInitInfiniteLoadHelper", offset);
        }

        [JSInvokable("InfiniteLoadHelperReachedEnd")]
        public static async Task InvokeScrollEnd() 
        {
            if (_instance == null) { Console.WriteLine("InfiniteScrollHelper instance is not defined."); return; }
            if (_instance.OnScrollEndReached == null) {  return; }
            await _instance.OnScrollEndReached.Invoke();
        }




    }
}
