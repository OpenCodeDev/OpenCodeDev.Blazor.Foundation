/*
 * Copyright (c) FlawlessLoop Studios, Inc.
 * Created in 2021
 * Last Update in 2021
 * Version: 0.1-alpha
 * JS Version: ES6+
 * Contributors:
 *      Max Samson (m.samson@opencodedev.com)
 *
 */


function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}
async function MotionUIAnimateOnce(Guid, Animation, Speed) {
    await (async () => {
        return new Promise(resolve => {
            let elem = document.getElementById(`${Guid}`);
            
            elem.classList.add(Animation);
            elem.classList.add(Speed);

            setTimeout(() => {
                elem.classList.remove(Animation);
                elem.classList.remove(Speed);
                resolve(true);
            }, Speed.toLowerCase() == "fast" ? 250 : 750);

        })
    })();
}

async function MotionUIAnimateIn(Guid, Animation) {
    await (async () => {
        return new Promise(resolve => {
            let elem = document.getElementById(`${Guid}`);
            Motion.animateIn(elem, Animation, () => resolve(true));

        })
    })();
}

async function MotionUIAnimateOut(Guid, Animation) {
    await (async () => {
        return new Promise(resolve => {
            let elem = document.getElementById(`${Guid}`);
            Motion.animateOut(elem, Animation, () => resolve(true));
        })
    })();
}


var BlazorFoundationInifiteLoadHelperFunc =
    (offset) => Foundation.util.throttle(function (e) {
    window.requestAnimationFrame(function () {
        // Get Maximum of Y value of the page.
        var scrollMaxY = window.scrollMaxY || (document.scrollingElement.scrollHeight - document.scrollingElement.clientHeight);
        let scroll = window.scrollY;
        if ((scrollMaxY - offset) <= scroll) {
            try {
                DotNet.invokeMethodAsync("OpenCodeDev.Blazor.Foundation", "InfiniteLoadHelperReachedEnd");
            } catch (e) {
                // Silent
            }
            
        }
    });
}, 300);

function BlazorFoundationInitInfiniteLoadHelper(offset) {
    // Remove Listen if exist
    try {
        document.removeEventListener(`scroll`, BlazorFoundationInifiteLoadHelperFunc(offset));
    }
    catch(e) {}
    document.addEventListener(`scroll`, BlazorFoundationInifiteLoadHelperFunc(offset));
}


async function ClipboardCopyText(base64) {
    let base = atob(base64);
    //console.log(base);
    let perm = await navigator.permissions.query({ name: "clipboard-write" });
    if (perm.state == "granted" || perm.state == "prompt") {
        try {
            await navigator.clipboard.writeText(base);
        } catch (e) {
            throw "Unknown error occured during copy.";
        }
    } else {
        throw "You must grant permission to access to your clipboard to copy.";
    }
}

async function ClipboardReadText() {
    return await navigator.clipboard.readText();
}

function HighlightJSInit(element) {
    hljs.highlightElement($(`#${element}`).get(0));
}

function FoundationDestroy(element) {
    $(`#${element}`).foundation('_destroy');
}

function OffCanvasRegister(element, options) {
    if (typeof window.OffCanvasList == 'undefined') {
        window.OffCanvasList = [];
    }
    console.log(`Register ${element}`);
    let optionsCanvas = options == null ? null : JSON.parse(options);
    window.OffCanvasList.push(new Foundation.OffCanvas($(`#${element}`), optionsCanvas));
}

function ElementToggle(element) {
    $(`#${element}`).foundation('toggle', null, null);
}

function ElementOpen(element) {
    console.log("Open " + element);
    $(`#${element}`).foundation('open', null, null);
}

function ElementClose(element) {
    console.log("Close " + element);
    $(`#${element}`).foundation('close', null, null);
}

function ElementShow(element) {
    $(`#${element}`).foundation('show');
}

function ElementHide(element) {
    $(`#${element}`).foundation('hide');
}

function ElementShowAll(element) {
    $(`#${element}`).foundation('showAll');
}

function ElementHideAll(element) {
    $(`#${element}`).foundation('hideAll');
}

// Called when system uses bf-event-*
function UseBfData() {
    // Search for all reveal link
    $(`[bf-event]`).each((index, value) => {
        let event = $(value).attr('bf-event');
        let event_id = $(value).attr('bf-event-id');
        if (typeof event == undefined || event == null || typeof event_id == undefined || event_id == null) {
            console.error(`To use bf-event, bf-event=${event} and bf-event-id=${event_id} must both be defined.`);
            return;
        }
        if (event == 'open') {
            $(value).click(() => {
              
                ElementOpen(event_id);
            });
        }else if (event == 'close') {
            $(value).click(() => {
          
                ElementClose(event_id);
            });
        }else if (event == 'toggle') {
            $(value).click(() => {
       
                ElementToggle(event_id);
            });
        }
    })
}


function AccordionRegister(element, options) {
    if (typeof window.AccordionList == 'undefined') {
        window.AccordionList = [];
    }
    console.log(`Register ${element}`);
    let optionsCanvas = options == null ? {} : JSON.parse(options);
    window.AccordionList.push(new Foundation.ResponsiveAccordionTabs($(`#${element}`), optionsCanvas));
}

function TabRegister(element, options) {
    if (typeof window.TabList == 'undefined') {
        window.TabList = [];
    }
    console.log(`Register ${element}`);
    let optionsCanvas = options == null ? {} : JSON.parse(options);
    window.TabList.push(new Foundation.ResponsiveAccordionTabs($(`#${element}`), optionsCanvas));
}

function DropdownRegister(element, options) {
    if (typeof window.DropdownList == 'undefined') {
        window.DropdownList = [];
    }

    let optionsCanvas = options == null ? {} : JSON.parse(options);
    window.DropdownList.push(new Foundation.Dropdown($(`#${element}`), optionsCanvas));
}

function RevealRegister(element, options) {
    if (typeof window.RevealList == 'undefined') {
        window.RevealList = [];
    }

    let optionsCanvas = options == null ? {} : JSON.parse(options);
    window.RevealList.push(new Foundation.Reveal($(`#${element}`), optionsCanvas));
}


function RevealOnClosedListener(element) {
   
    $(`#${element}`).on('closed.zf.reveal', function () {
        console.log(element);
        DotNet.invokeMethodAsync('OpenCodeDev.Blazor.Foundation', 'RSREVEALCLOSED', element);
        
    });
}


function DrilldownRegister(element, options) {
    if (typeof window.DrilldownList == 'undefined') {
        window.DrilldownList = [];
    }

    let optionsCanvas = options == null ? {} : JSON.parse(options);
    window.DrilldownList.push(new Foundation.Drilldown($(`#${element}`), optionsCanvas));
}

function AccordionMenuRegister(element, options) {
    if (typeof window.AccordionMenuList == 'undefined') {
        window.AccordionMenuList = [];
    }

    let optionsCanvas = options == null ? {} : JSON.parse(options);
    window.AccordionMenuList.push(new Foundation.AccordionMenu($(`#${element}`), optionsCanvas));
}

function DropdownMenuRegister(element, options) {
    if (typeof window.DropdownMenuList == 'undefined') {
        window.DropdownMenuList = [];
    }

    let optionsCanvas = options == null ? {} : JSON.parse(options);
    window.DropdownMenuList.push(new Foundation.DropdownMenu($(`#${element}`), optionsCanvas));
}

function TooltipRegister(element, options) {
    if (typeof window.TooltipList == 'undefined') {
        window.TooltipList = [];
    }

    let optionsCanvas = options == null ? {} : JSON.parse(options);
    window.TooltipList.push(new Foundation.Tooltip($(`#${element}`), optionsCanvas));
}

function StickyRegister(element, options) {
    if (typeof window.StickyList == 'undefined') {
        window.StickyList = [];
    }

    let optionsCanvas = options == null ? {} : JSON.parse(options);
    window.StickyList.push(new Foundation.Sticky($(`#${element}`), optionsCanvas));
}

function FoundationReflow(element) {
    $('#' + element).foundation('_reflow');
}
function SliderRegister(element, options) {
    if (typeof window.SliderList == 'undefined') {
        window.SliderList = [];
    }

    let optionsCanvas = options == null ? {} : JSON.parse(options);
    let foundationElement = $(`#${element}`);
    let slider = new Foundation.Slider(foundationElement, optionsCanvas);
    slider.BFResetOption = optionsCanvas;
    foundationElement.on('changed.zf.slider', async () => {
        let elInput = $(`#${element}-input`);

        let value = elInput.val();
        if (elInput != null && typeof elInput != 'undefined' && elInput != undefined && typeof value != 'undefined' && value != undefined) {
            await DotNet.invokeMethodAsync('OpenCodeDev.Blazor.Foundation', 'UpdateSingleSliderValue', element, parseFloat(value));
            //window.SliderList.forEach(async function (el) {
            //    el.$element.foundation('_reflow'); // Caculate Slider to Synchronize multiple binding (This triggers onchange resulting in infinite reflow loop)
            //})
        }

    });
    window.SliderList.push(slider);
}

function AbideRegister(element, options) {
    if (typeof window.AbideList == 'undefined') {
        window.AbideList = [];
    }

    let optionsCanvas = options == null ? {} : JSON.parse(options);
    window.AbideList.push(new Foundation.Abide($(`#${element}`), optionsCanvas));
}


