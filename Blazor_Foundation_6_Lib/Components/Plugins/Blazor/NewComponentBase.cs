using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Blazor
{
	/// <summary>
	/// New Component Base with PreRender Controller Ideal for Controls.
	/// </summary>
	public class NewComponentBase : ComponentBase
	{
		/// <summary>
		/// Additional Attribute to link to control
		/// </summary>
		[Parameter(CaptureUnmatchedValues = true)]
		public IDictionary<string, object> AdditionalAttributes { get; set; }
		private bool HasPrerendered { get; set; }

		/// <summary>
		///  
		/// </summary>
		protected override void OnAfterRender(bool firstRender)
		{
			if (firstRender)
			{
				HasPrerendered = firstRender;
				StateHasChanged();
			}
			AfterRenderWrap(firstRender);
		}

		/// <summary>
		/// Check if control is disabled when prerendering.
		/// </summary>
		/// <returns></returns>
		protected bool IsDisabled()
		{
			object shared = AdditionalAttributes != null && AdditionalAttributes.ContainsKey("disabled") && AdditionalAttributes["disabled"] != null ? 
				AdditionalAttributes["disabled"] : "False";
			bool value = false;
			if (shared.GetType() == typeof(bool)) value = (bool)shared;
			else if (shared.GetType() == typeof(string))
			{
                bool.TryParse((string)shared, out bool result);
				value = result;
            } 

            return value || !HasPrerendered;
        }
            
			return value || !HasPrerendered;
		}

		protected virtual void AfterRenderWrap(bool isFirstRender) { 
		
		}
	}
}
