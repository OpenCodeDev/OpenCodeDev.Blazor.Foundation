# Version 3.4.0 (October 2024)
## Features
- Migrated to Foundation 6.8.1 (Latest)
- Updated Documentation Page.
- Added String Extension support for the new .NET6+ CRC32 and CRC64.
- Added Markdown Reader System (EXPERIMENTAL)


## Fixes
- Added --table-head-bg-color
- Added --table-head-color
- Added --table-body-bg-color
- Added --table-body-hover-bg-color
- Added --table-body-eh-bg-color
- Added --table-unstriped-body-bg-color
- Added --table-unstriped-tr-bg-color
- Added --table-unstriped-tr-bot-border
- Added --table-body-border
- Added --code-border
- Added --code-bg-color
- Added --code-ft-color
- Adjusted Code Padding
- Remove Code Highlighter's border.
- Rewrite of the copy to clipboard function in attempting to fix error on IOS.
- Added Intellisense Documentation for BF6 Logo
- Added --breadcrumb-ft-link-color

# Version 3.3.3 (January 2024)
## Features
- Migrated to .NET 8.0

# Version 3.3.2 (June 2023)
## Fixes
- Fixed Button MarginTop property not adding margin-top

# Version 3.3.0 (June 2023)
## Features
- Added Lightweight CSS for Emails (bf6_email.css) (89kb instead of 700+kb)
- Added AdditionalAttributes on PaginationNext and PaginationPrevious.
- Added Render Fragment Extension Class.
- Added Render Fragment AutoIndexer for Attributes.

## Fixes
- CSS Variables for Paginations, Close Button, Input Hover/Focus, Select, Reveal Border Radius/Color
- Fixed Reveal Controller Indexing.

# Version 3.2 (March 2023)
## Features
- Added small/large/medium-justify-content-left/center/right... screen size.
- Added InputSelectorFragment to INovelRevealController.
- Added PreRendering on All Controls... The controls are now disabled when prerendering.

## Documentation
- Documentation is now prerendered
- Added Imports in Doc.
- Changed Code to Language for HighlightCS Doc.
- Fixed some outdated doc for some components.
- Added AppWrapper Documentation.
- Updated Installation Process.
- Updated LocalStorage New Features Documentation.

## Fixes
- Fixed Button Group Border Radius when Disabled.
- Fixed Justify-Content CSS;

# Version 3.1 (September 2022)
## Features
- New AppWrapper Component to Wrap the Shared Layout... It will automatically add required service components when service is available.
- HighlightCS now support string byte array (Performance).

## Documentation
- Removed References to HighlightCS in favor of HighlightCode (HighlightCS Still exist tho).
- Removed References to RevealController in favor of NovelRevealController (RevealController still exist but no longer supported and should not be used on Blazor Server).

 
## Fixes
- Maximize Compatibility by Supporting to WebAssembly 6.0.0+.
- Fix Border Radius on Button Group.
- Fix Table Striped Background (CSS Variable)
- Fix IStyleManagement for Blazor Server.


## Changes
- Services must by added as Scoped for Blazor Server using AddBlazorFoundationServices(false) or else they will be singleton.

## Breaking Changes
- We have ditched the Blazor.Foundation.Extensions package and embedded everything into this package... the namespace doesn't change but if you use the package Blazor.Foundation.Extensions you may encounter ambiguity! just remove the Blazor.Foundation.Extensions from nuget and nothing else change.

## Low-Impact Breaking Changes
- Removed Vertical Param from MenuAccordion.
- Removed data-submenu-toggle from MenuAccordion.

# Version 3.0.8.3 (June 2022)
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

# Version 3.0.7 (2022 Q1 Update 3)
## Features
- Add Countries' Flag Icons

## Documentation
- Add Documentation Support for Extra Icons
- Add Motion UI Documentation

## Fixes
- Fix Off Canvas Z-Icon (CSS)

# Version 3.0.6
- Hot Fixes
# Version 3.0.5
- Hot Fixes
# Version 3.0.4 (2022 Q1 Update 2)
## Fixes
- Code Highlither Support: razor.
- Switch Reveal Primary and Secondary Buttons.
- Add BG Color and Radius of Reveal in Style Manager.
- Fixed Slow Documentation due to Github.
- Fixed Foundation 6 Quoteblock Icon or Author -- not showing.
- Reset Slider Bottom Margin to be same and top.

# Version 3.0.3 (2022 Q1 Update 1)

## Deprecated
- File: blazor-foundation.js
- File: jquery.js
- File: what-input.js

Will be removed in the by v3.1.0

## Changes
- Added bf6_only.js (jquery.js, what-input.js and blazor-foundation.js combined).
- Added bf6_standard.js (jquery.js, what-input.js, foundation.js and blazor-foundation.js combined).

## Fixes
- Fix Top Bar CSS Variable
- Removed .DS_store preventing publishing of WASM app.
- Removed .map files preventing publishing of WASM app.


# Version 3.0
## Breaking Change Warning
Well Blazor Foundation 2 was a very early test run and no actual project implements it that wont be an issue be know that Version 3 LTS fully break Version 2.x.

Version 3 LTS is production ready and fully documented.

## New Features
- Added Support for .NET 5
- Add More Options to Customize Style.
- Added Code Highlighter Plugin.
- Added Clipboard Plugin.
- Added Credit Card field with Auto-Formating.
- Added Payment Icons (Visa, Mastercard...).
- Added Basic Infinite Scroll.
- Added MotionUI Controlled Anims.
- Added Reveal Controller.
- Added Orbit Media (Carousel).
- Added 100% Documentation.


## Changes
- Rename Files
- Reorganise Files
- Cleaned Overall Project.
- Remove Menu System.
- Use SCSS instead of CSS (Compiled Available)

## Fixes
- Fix Slider and Switches (Totally Broken in V2).
- Fix Tones of CSS Issues.
- Fix SingleSlider Issues
- Fix Sticky Not Resizing on Window size change.

# Version 2.x
Move to Version 3, The BF6 Version 2 has critical bug it was a test run NOT FOR PRODUCTION.

# Version 1.x
Not even a Release Candidate.