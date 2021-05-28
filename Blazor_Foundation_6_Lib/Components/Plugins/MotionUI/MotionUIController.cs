using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.MotionUI
{
    public class MotionUIController : IMotionUIController
    {

        public IJSRuntime JS { get; set; }

        /// <summary>
        /// Currently Executing an Animation Once
        /// </summary>
        public static List<string> CurrentlyOnceActive { get; set; } = new List<string>();

        /// <summary>
        /// Currently Executing an Animation In
        /// </summary>
        public static List<string> CurrentlyInActive { get; set; } = new List<string>();

        /// <summary>
        /// Currently Executing an Animation Out
        /// </summary>
        public static List<string> CurrentlyOutActive { get; set; } = new List<string>();



        public MotionUIController(IJSRuntime js){
            JS = js;
        }

        private string ConvertAnimToString (Animation animation)
        {
            switch (animation)
            {
                case Animation.SpinCW:
                    return "spin-cw";
                case Animation.SpinCCW:
                    return "spin-ccw";
                case Animation.Wiggle:
                    return "wiggle";
                default:
                    return "shake";
            }
        }

        public async Task AnimateOnce(string element, Animation animation, AnimationSpeed speed = AnimationSpeed.Fast)
        {
            string anim = ConvertAnimToString(animation);
            if (CurrentlyOnceActive.Contains(element)) { return; }
            CurrentlyOnceActive.Add(element);
            await JS.InvokeVoidAsync("MotionUIAnimateOnce", element, anim, speed.ToString());
            CurrentlyOnceActive.Remove(element);
        }
        public async Task AnimateAndShow(string element, string animation, bool forceClear = true, int timeout = 1000)
        {
            if (forceClear) { CurrentlyInActive.Remove(element); }
            CurrentlyInActive.Add(element);

            // Set Anim Timeout (Remove Tracking from List)
            Task.Run(async () => {
                await Task.Delay(timeout);
                CurrentlyInActive.Remove(element);
            });
            await JS.InvokeVoidAsync("MotionUIAnimateIn", element, animation);
            CurrentlyInActive.Remove(element);
        }

        public async Task AnimateAndHide(string element, string animation, bool forceClear = true, int timeout = 1000)
        {
            if (forceClear) { CurrentlyOutActive.Remove(element); }
            CurrentlyOutActive.Add(element);

            // Set Anim Timeout (Remove Tracking from List)
            Task.Run(async () => {
                await Task.Delay(timeout);
                CurrentlyOutActive.Remove(element);
            });
            await JS.InvokeVoidAsync("MotionUIAnimateOut", element, animation);
            CurrentlyOutActive.Remove(element);
        }

      


        public async Task<bool> IsRunningAsync( string element)
        {
            return await Task.Run(()=> IsRunning(element));
        }

        public bool IsRunning(string element)
        {
            return (CurrentlyInActive.Contains(element) || CurrentlyOutActive.Contains(element));
        }

    }
}
