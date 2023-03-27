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
			string shared = AdditionalAttributes != null && 
				AdditionalAttributes.ContainsKey("disabled") && 
				AdditionalAttributes["disabled"] != null ? 
				(string)AdditionalAttributes["disabled"] : "False";
			bool.TryParse(shared, out bool result);
			return result || !HasPrerendered;
		}

		protected virtual void AfterRenderWrap(bool isFirstRender) { 
		
		}
	}
}
