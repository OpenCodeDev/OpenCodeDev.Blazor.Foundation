using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.StyleManager
{
    public interface IStyleManagement
    {
        void Set(string key, string value, bool update = false);
        void Set(string key, string value, string desc, bool update = false);
        void Set(string key, JObject value, bool update = false);

        void SetFromJson(JObject json);
        void SetFromJson(string json);

        string Get(string key);
        string Get(string key, string type);

        Func<Task> OnUpdate { get; set; }
        JObject GetAll();
    }
    public class BFStyleManagement : IStyleManagement
    {
        protected JObject Style { get; set; } = new JObject();

        public Func<Task> OnUpdate { get; set; }
        public BFStyleManagement()
        {
            //Console.WriteLine("Setup the base style.");
            // Foundation 6
            Set("--primary-color", "#2a76bb", "Global theme primary color.");
            Set("--secondary-color", "#656565", "Global theme secondary color.");
            Set("--alert-color", "#c60f13", "Alert color");
            Set("--success-color", "#5da423", "Success color");
            Set("--warning-color", "#ffae00", "Warning color");

            // Constrast Colors
            Set("--white-color", "#ffffff", "Global white color");
            Set("--white-smoke-color", "#f5f5f5", "Global white color");
            Set("--light-gray-color", "#e6e6e6", "Light gray color");
            Set("--light-warm-gray-color", "#f3f2ef", "Light warm gray color");

            Set("--medium-gray-color", "#cacaca", "Medium gray color");
            Set("--dark-gray-color", "#8a8a8a", "Dark gray color");
            Set("--blackish-color", "#5d5d5d", "blackish-grayish color");
            Set("--black-color", "black", "Black color");

            Set("--global-border-radius", "0.5em", "Global Border Radius");

            // Blazor Foundation

            // Button
            Set("--button-bg-color", "var(--primary-color)");
            Set("--button-ft-color", "var(--white-color)");
            Set("--button-border-radius", "var(--global-border-radius)");

            Set("--button-group-middle-border-radius", "0");
            Set("--button-group-first-border-radius", "0.5em 0em 0em 0.5em");
            Set("--button-group-last-border-radius", "0em 0.5em 0.5em 0em");

            Set("--table-stripe-bg", "#f1f1f1");

            Set("--button-primary-bg-color", "var(--primary-color)");
            Set("--button-primary-ft-color", "var(--white-color)");

            Set("--button-secondary-bg-color", "var(--secondary-color)");
            Set("--button-secondary-ft-color", "var(--white-color)");

            Set("--button-success-bg-color", "var(--success-color)");
            Set("--button-success-ft-color", "var(--white-color)");

            Set("--button-warning-bg-color", "var(--warning-color)");
            Set("--button-warning-ft-color", "var(--white-color)");

            Set("--button-alert-bg-color", "var(--alert-color)");
            Set("--button-alert-ft-color", "var(--white-color)");

            // Switch
            Set("--switch-border-radius", "var(--global-border-radius)", "Switch Border Radius");
            Set("--switch-paddle-border-radius", "var(--global-border-radius)", "Switch Paddle Border Radius");
            Set("--switch-bg-color", "var(--primary-color)", "Switch Background Color");

            Set("--slider-filling-bg-color", "var(--medium-gray-color)", "Slider Filling Background Color");
            Set("--slider-bg-color", "var(--light-gray-color)", "Slider Background Color");
            Set("--slider-handle-bg-color", "var(--primary-color)", "Slider Handler Background Color");
            Set("--slider-handle-border-radius", "var(--global-border-radius)", "Slider Handler Border Radius");

            Set("--link-ft-color", "var(--primary-color)", "Color of <a> font link.");
            Set("--link-active-ft-color", "var(--black-color)", "Color of <a> font active link.");

            Set("--body-ft-color", "var(--black-color)", "Default body text color.");
            Set("--body-bg-color", "var(--white-color)", "Default body background color.");

            // Accordion
            Set("--accordion-border-color", "var(--light-gray-color)", "Accordion border color.");
            Set("--accordion-bg-color", "var(--white-color)", "Accordion background color.");

            Set("--accordion-title-ft-color", "var(--primary-color)", "Accordion font color.");
            Set("--accordion-title-border-color", "var(--accordion-border-color)", "Accordion title border color.");

            Set("--accordion-content-ft-color", "var(--black-color)", "Accordion content font color.");
            Set("--accordion-content-border-color", "var(--accordion-border-color)", "Accordion content border color.");
            Set("--accordion-content-bg-color", "var(--accordion-bg-color)", "Accordion content background color.");

            //Headline
            Set("--headline-divider-color", "var(--primary-color)", "Headline divider color.");


            
            // Navigation Top Bar
            Set("--top-bar-ft-color", "var(--white-color)", "Top bar font color.");
            Set("--top-bar-bg-color", "var(--black-color)", "Top bar background color.");
            Set("--top-bar-link--active-ft-color", "var(--white-color)", "Top Bar Active Link Font Color.");
            Set("--top-bar-link-hover-ft-color", "var(--white-color)", "Top Bar Hover Font Color.");
            Set("--top-bar-link-ft-color", "var(--white-color)", "Top Bar Link Font Color.");

            Set("--off-canvas-bg-color", "var(--white-smoke-color)", "Off-Canvas backgound color.");

            // Inputs
            Set("--form-input-border-radius", "0.2em", "Input border radius (text, textarea, email)...");
            Set("--form-input-font-size", "1rem", "Input font size.");
            Set("--form-input-border", "1px solid #cacaca", "Input border style.");
            Set("--form-input-background-color", "var(--white-color)", "Input background color.");
            Set("--form-input-color", "var(--blackish-color)", "Input font color.");

            Set("--form-input-focus-border", "1px solid #8a8a8a", "Input focus border.");
            Set("--form-input-focus-bg-color", "#fefefe", "Input focus background color.");
            Set("--form-input-focus-bs", "0 0 5px #cacaca", "Input focus box shadow.");

            Set("--hljs-header-title-ft-color", "var(--white-color)", "Code Highlighter (HighlightCS) font color of header's title.");
            Set("--hljs-header-copy-ft-color", "var(--white-color)", "Code Highlighter (HighlightCS) font color of header's copy button.");
            Set("--hljs-header-bg-color", "var(--dark-gray-color)", "Code Highlighter (HighlightCS) background color of header.");

            Set("--reveal-border-radius", "var(--global-border-radius)", "Border radius for reveal panel.");
            Set("--reveal-border", "1px solid #cacaca", "Border for reveal panel.");
            Set("--reveal-bg-color", "var(--white-color)", "background color for reveal panel.");

            // Forms
            Set("--input-select-focus-border", "1px solid #8a8a8a", "Input select focus border");
            Set("--input-focus-bg-color", "var(--white-color)", "Input focus background");
            Set("--input-focus-bs", "0 0 5px #cacaca;", "Input focus box shadow");

            // Button Close
            Set("--close-button-color", "var(--dark-gray-color)", "Close button color.");
            Set("--close-button-hover-color", "var(--blackish-color)", "Close hover color.");
            Set("--close-button-focus-color", "var(--blackish-color)", "Close focus color.");

            // Pagination
            Set("--nav-current-bg-color", "var(--primary-color)", "Pagination background color.");
            Set("--nav-current-color", "var(--white-color)", "Pagination color.");
            Set("--nav-current-border-radius", "var(--global-border-radius)", "Pagination border radius.");
            
            Set("--nav-link-color", "var(--black-color)", "Pagination a color.");
            Set("--nav-hover-bg-color", "var(--light-warm-gray-color)", "Pagination hover background color.");
            Set("--nav-disabled-color", "var(--medium-gray-color)", "PAgination disabled color.");

			// Table
			Set("--table-head-bg-color", "var(--white-color)", "Table header background.");
			Set("--table-head-color", "var(--black-color)", "Table header font color.");
			Set("--table-body-border", "1px solid #f1f1f1", "Table body border");
			Set("--table-body-bg-color", "var(--white-color)", "Table body background color.");
			Set("--table-body-hover-bg-color", "var(--light-gray-color)", "Table body hover bg color.");
			Set("--table-body-eh-bg-color", "#ececec", "Table body even hover bg color.");
			Set("--table-unstriped-body-bg-color", "white", "Table body unstriped bg color.");
			Set("--table-unstriped-tr-bg-color", "white", "Table tr unstriped bg color.");
			Set("--table-unstriped-tr-bot-border", "1px solid #f1f1f1", "Table tr bottom border.");
            
            Set("--card-bg-color", "#fefefe", "card background color.");
			Set("--card-ft-color", "#0a0a0a", "card font color.");
            Set("--card-border", "1px solid #e6e6e6", "Card border ");

			// Code

			Set("--code-bg-color", "#c5c5c5", "Code background color.");
			Set("--code-ft-color", "#044f88", "Code font color.");
			Set("--code-border", "1px solid #8b8b8b", "Code border ");
		}

		/// <summary>
		/// Use to dynamically set the color of referenced element. 
		/// </summary>
		/// <param name="key">ref of the variable</param>
		/// <param name="value"> string value (color html)</param>
		/// <param name="update">Call update event, usually to trigger rerendering.</param>
		public virtual void Set(string key, string value, bool update = false)
        {
            string desc = "hide"; // Default Description
            if (Style.ContainsKey(key)) { desc = Style[key].ToObject<JObject>().GetValue("desc").ToString(); }
            Set(key, new JObject() { { "value", value }, { "desc", desc } }, update);
        }

        /// <summary>
        /// Set a single css variable with a description.
        /// </summary>
        /// <param name="key">ref of the variable</param>
        /// <param name="value"> string value (color html)</param>
        /// <param name="desc">What's the vaiable for ? (short description for documentation)</param>
        /// <param name="update">Call update event, usually to trigger rerendering.</param>
        public virtual void Set(string key, string value, string desc, bool update = false)
        {
            Set(key, new JObject() { { "value", value }, { "desc", desc } }, update);
        }

        /// <summary>
        /// Set a single entry with JSON value {"value":"", "desc":""}.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">JSON value reppresenting style object.</param>
        /// <param name="update">Call update event, usually to trigger rerendering.</param>
        public virtual void Set(string key, JObject value, bool update = false)
        {
            if (!Style.ContainsKey(key))
            {
                Style.Add(key, value);
            }
            else
            {
                Style[key] = value;
            }
            if (update) { OnUpdate?.Invoke(); }
        }

        /// <summary>
        /// Load template from json object.
        /// </summary>
        public virtual void SetFromJson(JObject json)
        {
            foreach (var item in json)
            {
                if (Style.ContainsKey(item.Key))
                {
                    Style[item.Key] = JObject.Parse(item.Value.ToString());
                }
                else
                {
                    Style.Add(item.Key, item.Key);
                }
            }
            OnUpdate?.Invoke();
        }

        /// <summary>
        /// Load template from json string.
        /// </summary>
        /// <param name="json"></param>
        public virtual void SetFromJson(string json)
        {
            SetFromJson(JObject.Parse(json));
        }

        /// <summary>
        /// Get Style Property
        /// </summary>
        /// <param name="key">name of key</param>
        public virtual string Get(string key)
        {
            if (Style.ContainsKey(key))
            {
                return Style.GetValue(key).ToObject<JObject>().GetValue("value").ToString();
            }
            return "";
        }
       
        /// <summary>
        /// Get Value Type, Key and desc or value.
        /// </summary>
        /// <param name="key">Variable name eg: --test-var</param>
        /// <param name="type">Type (desc or value)</param>
        /// <returns></returns>
        public virtual string Get(string key, string type)
        {
            if (Style.ContainsKey(key))
            {
                return Style.GetValue(key).ToObject<JObject>().GetValue(type).ToString();
            }
            return "";
        }

        /// <summary>
        /// Get JSON Object
        /// </summary>
        /// <returns></returns>
        public JObject GetAll()
        {
            return Style;
        }
    }
}
