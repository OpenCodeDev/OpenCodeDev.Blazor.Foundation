# Version 3.0.8.2 (June 2022)
## Changes
- Change Button Padding Style into Class (Performance, Size)
Allows the developer to use style attribute and override the margin/padding which was not the case prior this patch.

## Fixes
- Fix Orbital Button component, button was not constructed correctly.
Found while debugging failure to publish the Documentation 3.0.8.1
- Fix Tiny Button Problems from new Feature PAdding/Margin option in buttons.

# Version 3.0.8 (June 2022)
## Features
- **Beta** New InputSelectionList Component (Mobile Firendly List Input)
- **Beta** New Headline Component (-------- Headline ---------)
- New Padding and Margin to Button Component (See Doc, No Changes Affecting Existing)
- New HighlightCode Component (Replacement for HighlightCS | See OpenCodeDev.Blazor.Foundation.Plugins.HighlightCS).
- New CSS Variables (See Doc)

## Documentation
- Added Documentation for Headline
- Added Documentation for Input Selection List.
- Added Documentation for Padding and Margin Parameters on Button
- Added Documentation for LocalStorage Extension
- Added Searchable Documentation for Material Design Icons.
- Add Motion UI Documentation

## Fixes
- Fix Blazor Server removed Singleton to Scoped -- **Important**.
- Fix Bunch of Mission CSS Variable for Style Management System.
- Clean Debug Artifacts.
- Clean Old Menu System Artifacts from Code.

## Changes
- Move SCSS from Blazor_Foundation_6_SCSS to Blazor_Foundation_6_Lib/_SCSS
- Move JS from Blazor_Foundation_JS to Blazor_Foundation_6_Lib/_JS
- Visual Studio will launch node-scss at build to compile SCSS.
- Visual Studio will launch powershell to combine JS files.