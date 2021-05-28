using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.MotionUI
{
    
    public enum Animation
    {
        SpinCW,
        SpinCCW,
        Wiggle,
        Shake


    }
    public enum AnimationSpeed{
    Fast,
    Slow
    }
    public interface IMotionUIController
    {
        /// <summary>
        /// Show hidden element using a specified animation
        /// </summary>
        /// <param name="element">ID of HTML/Blazor element.</param>
        /// <param name="animation">
        /// fade-in, slide-in-up<b>*</b>, slide-in-left<b>*</b>, slide-in-down<b>*</b>, slide-in-right<b>*</b> <br/>
        /// hinge-in-from-top, hinge-in-from-right, hinge-in-from-up, hinge-in-from-down <br/>
        /// hinge-in-from-middle-x, hinge-in-from-middle-y, scale-in-up, scale-in-down <br/>
        /// spin-in, spin-in-ccw<br/><br/>
        /// * slide-x is not recommended for on load page animation.
        /// </param>
        /// <param name="preventSpam">
        /// Block double trigger before animation is done. Does not prevent when animation is hiding. <br/>
        /// therefore, if animation is currently hiding it will cancel and start anim to show.
        /// </param>
        /// <returns></returns>
        Task AnimateAndShow(string element, string animation, bool forceClear = true, int timeout = 1000);

        /// <summary>
        /// Hide visible element using a specified animation
        /// </summary>
        /// <param name="element">ID of HTML/Blazor element.</param>
        /// <param name="animation">
        /// fade-out, slide-out-up<b>*</b>, slide-out-left<b>*</b>, slide-out-down<b>*</b>, slide-out-right<b>*</b><br/>
        /// hinge-out-from-top, hinge-out-from-right, hinge-out-from-up, hinge-out-from-down<br/>
        /// hinge-out-from-middle-x, hinge-out-from-middle-y, scale-out-up, scale-out-down<br/>
        /// spin-out, spin-out-ccw <br/><br/>
        /// * slide-x is not recommended for on load page animation.
        /// </param>
        /// <param name="preventSpam">
        /// Block double trigger before animation is done. Does not prevent when animation is showing. <br/>
        /// therefore, if animation is currently showing it will cancel and start anim to hide.
        /// </param>
        /// <returns></returns>
        Task AnimateAndHide(string element, string animation, bool forceClear = true, int timeout = 1000);

        /// <summary>
        /// Trigger an animation on a visible element. Those are action animation, like wiggle, vibrate etc...
        /// </summary>
        /// <param name="element">ID of HTML/Blazor element</param>
        /// <param name="animation">SpinCW, SpinCCW, Wiggle, Shake</param>
        /// <param name="speed">Slow, Fast</param>
        /// <returns></returns>
        Task AnimateOnce(string element, Animation animation, AnimationSpeed speed = AnimationSpeed.Fast);

        /// <summary>
        /// Check if Show or Hide specified element's animation is currently playing
        /// </summary>
        /// <param name="element">ID of the HTML/Blazor Element</param>
        /// <returns>True when playing a OUT or IN animation</returns>
        Task<bool> IsRunningAsync(string element);

        /// <summary>
        /// Check if Show or Hide specified element's animation is currently playing
        /// </summary>
        /// <param name="element">ID of the HTML/Blazor Element</param>
        /// <returns>True when playing a OUT or IN animation</returns>
        bool IsRunning(string element);

    }
}
