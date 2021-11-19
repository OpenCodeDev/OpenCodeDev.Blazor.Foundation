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
            Console.WriteLine("Setup the base style.");
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

            Set("--body-ft-color", "var(--black-color)", "Default body text color.");
            Set("--body-bg-color", "var(--white-color)", "Default body background color.");


            // Navigation Top Bar
            Set("--top-bar-ft-color", "var(--white-color)", "Top bar font color.");
            Set("--top-bar-bg-color", "var(--black-color)", "Top bar background color.");
            Set("--top-bar-link--active-ft-color", "var(--white-color)", "Top Bar Active Link Font Color.");
            Set("--top-bar-link-hover-ft-color", "var(--white-color)", "Top Bar Hover Font Color.");
            Set("--top-bar-link-ft-color", "var(--white-color)", "Top Bar Link Font Color.");

            Set("--off-canvas-bg-color", "var(--white-smoke-color)", "Off-Canvas backgound color.");

            Set("--form-input-border-radius", "0.2em", "Input border radius (text, textarea, email)...");
            Set("--form-input-font-size", "1rem", "Input font size.");
            Set("--form-input-border", "1px solid #cacaca", "Input border style.");
            Set("--form-input-background-color", "var(--white-color)", "Input background color.");
            Set("--form-input-color", "var(--blackish-color)", "Input font color.");

            Set("--hljs-header-title-ft-color", "var(--white-color)", "Code Highlighter (HighlightCS) font color of header's title.");
            Set("--hljs-header-copy-ft-color", "var(--white-color)", "Code Highlighter (HighlightCS) font color of header's copy button.");
            Set("--hljs-header-bg-color", "var(--dark-gray-color)", "Code Highlighter (HighlightCS) background color of header.");
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
