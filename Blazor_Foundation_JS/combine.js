
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