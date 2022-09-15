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