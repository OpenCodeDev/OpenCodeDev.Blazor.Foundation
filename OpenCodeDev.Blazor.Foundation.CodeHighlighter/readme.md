# OpenCodeDev.Blazor.Foundation.Plugins.HighlightCS
Provides Code Highlighter for Blazor Foundation.

Requires OpenCodeDev.Blazor.Foundation.Extensions to work.

## Installation
The highligther's underlying engine is highlightJS.

index.html
```
<link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/highlight.js/10.7.1/styles/vs.min.css">
<script src="//cdnjs.cloudflare.com/ajax/libs/highlight.js/10.7.1/highlight.min.js"></script>
```

Insert in footer to preserve code tab.
```
<script>
    hljs.configure({ tabReplace: '    ' });
</script>
```

to see use cases and example see [documentation](https://bf.opencodedev.com/code-highlighter).