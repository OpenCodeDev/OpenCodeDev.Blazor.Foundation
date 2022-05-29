
var combineFiles = require('combine-files');

// Merge including Foundation 6
combineFiles([
    './src/jquery.js', 
    './src/what-input.js', 
    './src/foundation.min.js', 
    './src/blazor-foundation.js'
    ], '../Blazor_Foundation_6_Lib/wwwroot/js/bf6_standard.js', '\n\n');

// 
combineFiles([
    './src/jquery.js', 
    './src/what-input.js', 
    './src/blazor-foundation.js'
], '../Blazor_Foundation_6_Lib/wwwroot/js/bf6_only.js', '\n\n');
/*
Get-Content  
&quot;$(ProjectDir)_JS\src\jquery.js&quot;, 
&quot;$(ProjectDir)_JS\src\newline.js&quot;, 
&quot;$(ProjectDir)_JS\src\what-input.js&quot; 
&quot;$(ProjectDir)_JS\src\newline.js&quot;, 
&quot;$(ProjectDir)_JS\src\foundation.min.js&quot; 
&quot;$(ProjectDir)_JS\src\newline.js&quot;, 
&quot;$(ProjectDir)_JS\src\blazor-foundation.js&quot; 
| Set-Content 
&quot;$(ProjectDir)\wwwroot\js\bf6_standard.js&quot;

Get-Content  
&quot;$(ProjectDir)_JS\src\jquery.js&quot;, 
&quot;$(ProjectDir)_JS\src\newline.js&quot;, 
&quot;$(ProjectDir)_JS\src\what-input.js&quot; 
&quot;$(ProjectDir)_JS\src\newline.js&quot;, 
&quot;$(ProjectDir)_JS\src\blazor-foundation.js&quot; 
| Set-Content 
&quot;$(ProjectDir)\wwwroot\js\bf6_only.js&quot;