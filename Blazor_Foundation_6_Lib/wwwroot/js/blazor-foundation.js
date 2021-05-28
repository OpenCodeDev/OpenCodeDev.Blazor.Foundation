/*
 * Copyright (c) FlawlessLoop Studios, Inc.
 * Created in 2021
 * Last Update in 2021
 * Version: 0.1-alpha
 * JS Version: ES5 && ES6
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


function MenuSystemIsOpen(id, position) {
    try {
        return MenuSystem.find(id).isOpen(position);
    } catch (e) {
        return false;
    }

}
function MenuSystemRegister(options) {
    new MenuSystem(JSON.parse(options)); // Create Menu System


}

function MenuSystemUnRegister(id) {
    MenuSystem.find(id).destroy();
}

function MenuSystemOpen(id, position) {
    MenuSystem.find(id).open(position);
}

function MenuSystemClose(id) {
    MenuSystem.find(id).close();
}



var BlazorFoundationInifiteLoadHelperFunc =
    (offset) => Foundation.util.throttle(function (e) {
    window.requestAnimationFrame(function () {
        // Get Maximum of Y value of the page.
        var scrollMaxY = window.scrollMaxY || (document.scrollingElement.scrollHeight - document.scrollingElement.clientHeight);
        let scroll = window.scrollY;
        if ((scrollMaxY - offset) <= scroll) {
            DotNet.invokeMethodAsync("Blazor_Foundation_6","InfiniteLoadHelperReachedEnd");
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
    console.log(JSON.parse(options));
    let optionsCanvas = options == null ? { 'data-transition-time': 500 } : JSON.parse(options);
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
        DotNet.invokeMethodAsync('Blazor_Foundation_6', 'RSREVEALCLOSED', element);
        
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

function StickyRegister(element, options) {
    if (typeof window.StickyList == 'undefined') {
        window.StickyList = [];
    }

    let optionsCanvas = options == null ? {} : JSON.parse(options);
    window.StickyList.push(new Foundation.Sticky($(`#${element}`), optionsCanvas));
}


function SliderRegister(element, options) {
    if (typeof window.SliderList == 'undefined') {
        window.SliderList = [];
    }

    let optionsCanvas = options == null ? {} : JSON.parse(options);
    window.SliderList.push(new Foundation.Slider($(`#${element}`), optionsCanvas));
}

function AbideRegister(element, options) {
    if (typeof window.AbideList == 'undefined') {
        window.AbideList = [];
    }

    let optionsCanvas = options == null ? {} : JSON.parse(options);
    window.AbideList.push(new Foundation.Abide($(`#${element}`), optionsCanvas));
}

function CreditCardRegister(element, number, fullname, expiredate) {
    if (typeof window.CreditCardList == 'undefined') {
        window.CreditCardList = [];
    }
    window.CreditCardList.push(new Card({ form: 'form#'+element, container: '.' + element, placeholders: { number: number, name: fullname, expiry: expiredate, cvc: '***' } }));
    CreditCardType(element, number);
}

function CreditCardType(element, cardnumber) {
    let cardclass;
    let patterns = [
    {
        cardname: "unknown",
        pattern: /^[0-9][0-9][0-9][0-9]/,
        classname: "jp-card-unknown"
    },{
        cardname: "amex",
        pattern: /^3[47]/,
        classname: "jp-card-amex"
    },{
        cardname: "dankort",
        pattern: /^5019/,
        classname: "jp-card-dankort"
    }, {
        cardname: "dinersclub",
        pattern: /^(36|38|30[0-5])/,
        classname: "jp-card-dinersclub"
    }, {
        cardname: "discover",
        pattern: /^(6011|65|64[4-9]|622)/,
        classname: "jp-card-discover"

    }, {
        cardname: "elo",
        pattern: /^401178|^401179|^431274|^438935|^451416|^457393|^457631|^457632|^504175|^627780|^636297|^636369|^636368|^(506699|5067[0-6]\d|50677[0-8])|^(50900\d|5090[1-9]\d|509[1-9]\d{2})|^65003[1-3]|^(65003[5-9]|65004\d|65005[0-1])|^(65040[5-9]|6504[1-3]\d)|^(65048[5-9]|65049\d|6505[0-2]\d|65053[0-8])|^(65054[1-9]|6505[5-8]\d|65059[0-8])|^(65070\d|65071[0-8])|^65072[0-7]|^(65090[1-9]|65091\d|650920)|^(65165[2-9]|6516[6-7]\d)|^(65500\d|65501\d)|^(65502[1-9]|6550[3-4]\d|65505[0-8])|^(65092[1-9]|65097[0-8])/,
        classname: "jp-card-elo"
    }, {
        cardname: "hipercard",
        pattern: /^(384100|384140|384160|606282|637095|637568|60(?!11))/,
        classname: "jp-card-hipercard"
    }, {
        cardname: "jcb",
        pattern: /^(308[8-9]|309[0-3]|3094[0]{4}|309[6-9]|310[0-2]|311[2-9]|3120|315[8-9]|333[7-9]|334[0-9]|352[8-9]|35[3-8][0-9])/,
        classname: "jp-card-jcb"
    }, {
        cardname: "laser",
        pattern: /^(6706|6771|6709)/,
        classname: "jp-card-laser"
    }, {
        cardname: "maestro",
        pattern: /^(5018|5020|5038|5078|5[6-9]|6304|6703|6708|6759|676[1-3])/,
        classname: "jp-card-maestro"
    }, {
        cardname: "mastercard",
        pattern: /^(5[1-5]|677189)|^(222[1-9]|2[3-6]\d{2}|27[0-1]\d|2720)/,
        classname: "jp-card-mastercard"
    }, {
        cardname: "mir",
        pattern: /^220[0-4][0-9][0-9]\d{10}$/,
        classname: "jp-card-mir"
    }, {
        cardname: "troy",
        pattern: /^9792/,
        classname: "jp-card-troy"
    }, {
        cardname: "unionpay",
        pattern: /^62/,
        classname: "jp-card-unionpay"
    }, {
        cardname: "visa",
        pattern: /^4/,
        classname: "jp-card-visa"
    }, {
        cardname: "visaelectron",
        pattern: /^4(026|17500|405|508|844|91[37])/,
        classname: "jp-card-visaelectron"
     }
    ];

    patterns.forEach(verifyPattern);

    function verifyPattern(item) {
        var result = cardnumber.match(item.pattern);

        if (result != null) {
            if (item.name == 'unknown') {
                cardclass = 'jp-card ' + item.classname;

            } else {
                cardclass = 'jp-card ' + item.classname + ' jp-card-identified';

            }
        }
        
        $('.' + element + ' > div.jp-card-container > div.jp-card').attr('class', cardclass );
    }
}



class MenuSystem {
    static SystemList = []; // All MenuSystem Available. (List of {})
    constructor(options) {
        this.uuid = MenuSystem.uuidv4(); // Create Unique ID (might be useful for later purpose)
        if (typeof options == 'undefined' || options === undefined) {
            throw "Null exception! You must defined options as JS Object in BFMenuSystem. See Doc on OCD's website :)";
        }

        // Check if id was defined
        if (options.id === undefined) {
            throw `You cannot create a MenuSystem without id for obvious reasons.`;
        }

        // Check if we found App;
        if (options.appId === undefined) {
            throw `BFMenuSystem couldn't find appId of null. To use BFMenuSystem you must define an ID for App. Sometimes it is simply the loader misplaced or loader loads before the app rendering.`;
        }

        this.appId = options.appId;
        this.id = options.id; // Set Unique ID (Dev Defined)

        // Defined Options (may need recursive update)
        // I was thinking of function then for each all elements from array [{name:'hasTop', default:false}...{name:'transitionType', default:'PushBoxed'}]
        // then do the logic below, each Capital would be replaced by its lowercase preceeded of '-' by convention.
        // so the code would like greatly shrinked, easier to add new option along the way and potentialy more performant overall.

        this.hasTop = options.hasTop === undefined ?
            (typeof $(`#${this.id}`).attr(`has-top`) !== typeof undefined && $(`#${this.id}`).attr(`has-top`) !== false) ?
                $(`#${this.id}`).attr(`has-top`) : true : options.hasTop;

        this.transitionType = options.transitionType === undefined ?
            (typeof $(`#${this.id}`).attr(`transition-type`) !== typeof undefined && $(`#${this.id}`).attr(`transition-type`) !== false) ?
                $(`#${this.id}`).attr(`transition-type`) : 'PushBoxed'.toLocaleUpperCase() : options.transitionType.toLocaleUpperCase();

        this.hasOverlay = options.hasOverlay === undefined ?
            (typeof $(`#${this.id}`).attr(`has-overlay`) !== typeof undefined && $(`#${this.id}`).attr(`has-overlay`) !== false) ?
                $(`#${this.id}`).attr(`has-overlay`) : true : options.hasOverlay;
        if (!this.hasOverlay) { console.warn(`MenuSystem ${this.uuid} has no overlay, this may create unstability depending on the functionnality of your app. Note: it is better to set an overlay to prevent user from click on other function in the background.`); }

        this.overlayBlur = options.overlayBlur === undefined ?
            (typeof $(`#${this.id}`).attr(`overlay-blur`) !== typeof undefined && $(`#${this.id}`).attr(`overlay-blur`) !== false) ?
                $(`#${this.id}`).attr(`overlay-blur`) : true : options.overlayBlur;

        this.overlayAutoClose = options.overlayAutoClose === undefined ?
            (typeof $(`#${this.id}`).attr(`overlay-auto-close`) !== typeof undefined && $(`#${this.id}`).attr(`overlay-auto-close`) !== false) ?
                $(`#${this.id}`).attr(`overlay-auto-close`) : true : options.overlayAutoClose;

        this.overlayColor = options.overlayColor === undefined ?
            (typeof $(`#${this.id}`).attr(`overlay-color`) !== typeof undefined && $(`#${this.id}`).attr(`overlay-color`) !== false) ?
                $(`#${this.id}`).attr(`overlay-color`) : 'none' : options.overlayColor;

        this.topHeight = options.topHeight === undefined ?
            (typeof $(`#${this.id}`).attr(`top-height`) !== typeof undefined && $(`#${this.id}`).attr(`top-height`) !== false) ?
                $(`#${this.id}`).attr(`top-height`) : '4em' : options.topHeight;

        this.topBGColor = options.topBGColor === undefined ?
            (typeof $(`#${this.id}`).attr(`top-bg-color`) !== typeof undefined && $(`#${this.id}`).attr(`top-bg-color`) !== false) ?
                $(`#${this.id}`).attr(`top-bg-color`) : 'var(--ms-panel-bg)' : options.topBGColor;

        this.leftWidth = options.leftWidth === undefined ?
            (typeof $(`#${this.id}`).attr(`left-width`) !== typeof undefined && $(`#${this.id}`).attr(`left-width`) !== false) ?
                $(`#${this.id}`).attr(`left-width`) : '15em' : options.leftWidth;

        this.leftBGColor = options.leftBGColor === undefined ?
            (typeof $(`#${this.id}`).attr(`left-bg-color`) !== typeof undefined && $(`#${this.id}`).attr(`left-bg-color`) !== false) ?
                $(`#${this.id}`).attr(`left-bg-color`) : 'var(--ms-panel-bg)' : options.leftBGColor;

        this.leftCloseButton = options.leftCloseButton === undefined ?
            (typeof $(`#${this.id}`).attr(`right-close-button`) !== typeof undefined && $(`#${this.id}`).attr(`right-close-button`) !== false) ?
                $(`#${this.id}`).attr(`right-close-button`) : true : options.leftCloseButton;

        this.rightWidth = options.rightWidth === undefined ?
            (typeof $(`#${this.id}`).attr(`right-width`) !== typeof undefined && $(`#${this.id}`).attr(`right-width`) !== false) ?
                $(`#${this.id}`).attr(`right-width`) : '15em' : options.rightWidth;

        this.rightBGColor = options.rightBGColor === undefined ?
            (typeof $(`#${this.id}`).attr(`right-bg-color`) !== typeof undefined && $(`#${this.id}`).attr(`right-bg-color`) !== false) ?
                $(`#${this.id}`).attr(`right-bg-color`) : 'var(--ms-panel-bg)' : options.rightBGColor;

        this.rightCloseButton = options.rightCloseButton === undefined ?
            (typeof $(`#${this.id}`).attr(`right-close-button`) !== typeof undefined && $(`#${this.id}`).attr(`right-close-button`) !== false) ?
                $(`#${this.id}`).attr(`right-close-button`) : true : options.rightCloseButton;

        // Add Overlay
        if (this.hasOverlay) {
            // If overlay not inserted prior to init then add it if necessary.
            if (!$(`#${this.id}>div[position="overlay"]`).length) {
                $(`#${this.id}`).append(`<div position="overlay" class="bf-menu-system-overlay"></div>`);
            }
            if (this.overlayAutoClose) {
                $(`#${this.id}>div[position="overlay"]`).click(() => {

                    this.close();
                });
            }
        }
        // find html elements
        // note: if not find, highly possible that dev didnt need the position so wont ever be called.
        this.overlay = $(`#${this.id}>div[position="overlay"]`);
        this.top = $(`#${this.id}>div[position="top"]`);
        this.left = $(`#${this.id}>div[position="left"]`);
        this.right = $(`#${this.id}>div[position="right"]`);
        this.app = $(`#${this.appId}`);

        // fix any hanging element that could move during closing and opening process due to sensitivty to height of the app 
        // eg: badly designed (Foundation) bottom bar.
        this.app.css('min-height', '100vh');

        if (this.left.length && this.leftCloseButton) {
            $(`#${this.id}>div[position="left"]`).prepend(`
                <div class="header-panel" >
                    <span data-id="close" class="mdi mdi-close-circle-outline close"></span>
                </div>
            `);
            let leftCloseButton = $(`#${this.id}>div[position="left"]>div>span[data-id="close"]`);
            leftCloseButton.click(() => {

                this.close();
            });
        }

        if (this.right.length && this.rightCloseButton) {
            $(`#${this.id}>div[position="right"]`).prepend(`
                <div class="header-panel" style="justify-content: flex-start;padding-right: 0;padding-left: 0.5em;">
                        <span data-id="close" class="mdi mdi-close-circle-outline close"></span>
                </div>
            `);
            let rightCloseButton = $(`#${this.id}>div[position="right"]>div>span[data-id="close"]`);
            rightCloseButton.click(() => {

                this.close();
            });
        }

        this.app.css('transition', '0.5s');
        this.status = {
            state: 'closed',
            position: null,
            panel: null,
            width: null,
            color: null
        }

        MenuSystem.SystemList.push(this); // Add self to global system registry
        let thisID = this.id;
        // Define all button hooking on this MS
        $(`[ms-data-linkto='${thisID}']`).each(function (element) {
            if ($(this).attr('ms-data-open') == 'left') {
                $(this).click(function () {
                    MenuSystem.find(thisID).open('left');
                });
            } else if ($(this).attr('ms-data-open') == 'right') {
                $(this).click(function () {
                    MenuSystem.find(thisID).open('right');
                });

            } else if ($(this).attr('ms-data-close').length) {
                $(this).click(function () {
                    MenuSystem.find(thisID).close();
                });
            } else {
                console.error(`MenuSystem '${thisID}': found ms-data-linkto with no valid ms-data-open which can be 'left' or 'right'.`);
            }
        });
    }

    static uuidv4() {
        return ([1e7] + 1e3 + 4e3 + 8e3 + 1e11).replace(/[018]/g, c =>
            (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
        );
    }

    // Universal Execution
    static find(id) {
        let object = MenuSystem.SystemList.find(p => p.id == id); // Find MenuSystem by id.
        if (object === undefined) {
            throw `Cannot find MenuSystem by ID '${id}'`;
        }
        return object; // return class
    }

    isOpen(position) {
        if (typeof this.status.state == 'undefined' || typeof this.status.position == 'undefined') {
            return false;
        }
        if (this.status.state == "opened" && this.status.position == position) {
            return true;
        }
        return false;
    }

    open(position) {
        if (this.status.state == 'opened') {
            this.close(); // Close Opened Panel
        }

        console.log(`Open Menu: ${position}`);
        // Open Logic
        let panel = null;
        let width = null;
        let color = null;
        if (position == 'left') {
            width = this.leftWidth;
            panel = this.left;
            color = this.leftBGColor;
        } else {
            panel = this.right;
            width = this.rightWidth;
            color = this.rightBGColor;
        }


        this.status = {
            state: "opened",
            position: position,
            panel: panel,
            width: width,
            color: color
        }


        // if not mobile top = visible
        if (this.top.css('visibility') != 'hidden' && this.hasTop) {
            if (this.transitionType == 'PUSHBOXED') {
                // PushBoxed means top is pushed as well
                this.app.css(`padding-top`, this.topHeight);
            }
            this.top.css('height', this.topHeight);
            this.top.css('background-color', this.topBGColor);
            this.top.css(`padding-${this.status.position}`, this.status.width);
        }

        if (this.transitionType == 'PUSH' || this.transitionType == 'PUSHBOXED') {
            this.app.css(`padding-${this.status.position}`, this.status.width);
        }

        this.status.panel.css('background-color', this.status.color);
        this.status.panel.css('width', this.status.width);
        this.app.css('overflow', 'hidden');
        this.app.css('height', '100vh');
        this.app.css('width', '100%');

        if (this.hasOverlay) {
            this.overlay.show();
            this.overlay.css('background-color', this.overlayColor);
            if (this.overlayBlur) {
                this.app.css('filter', 'blur(3px)');
            }
        }

    }

    destroy() {
        MenuSystem.SystemList = MenuSystem.SystemList.filter(e => e.id != this.id);

    }

    close() {
        console.log("Close Menu");
        if (this.top.css('visibility') != 'hidden' && this.hasTop) {
            if (this.transitionType == 'PUSHBOXED') {
                this.app.css(`padding-top`, 0);
            }
            this.top.css('height', 0);
            this.top.css(`padding-${this.status.position}`, 0);
        }

        if (this.transitionType == 'PUSH' || this.transitionType == 'PUSHBOXED') {
            this.app.css(`padding-${this.status.position}`, 0);
        }

        this.app.css('overflow', '');
        this.app.css('height', '');
        this.app.css('width', '');
        this.status.panel.css('width', 0);
        if (this.hasOverlay) {
            this.overlay.hide();
            if (this.overlayBlur) {
                this.app.css('filter', '');
            }
        }
        this.status = {
            state: 'closed',
            position: null,
            panel: null,
            width: null,
            color: null
        }
    }
}

