### My Mark Down Test Oh!
# **Header** 1
## *Header* 2
### Header 3
#### Header 4ee
##### Header 5
###### Header 6
#### Header 4

[--Button(Class="")--]
The Button
[--/Button--]
> blockquotesdasd
	
1. First item
2. Second item
3. Third item

- First item
   - First item
- Second item
- Third item

` This is a inline code with multiple lines `

--- 

[This is a link](https://www.example.com)

![Here's an image](image.jpg)


This is a paragraph without any stuff [This is a link](https://www.example.com) but this should be great.

```
This is a code block.
{
    "firstName": "John",
    "lastName": "Smith",
    "age": 25
}
```

~~The world is flat.~~

[--HighlightCode(type="", testing="")--]
namespace OpenCodeDev.Blazor.Foundation.Doc.Core.Plugins
{
    public static class MDILoader{
        public static async Task LoadMdi(HttpClient client, NavigationManager navigator)
        {
            try
            {
                byte[] licenseResult = await client.GetByteArrayAsync($"{navigator.BaseUri}/_content/OpenCodeDev.Blazor.Foundation.Doc.Core/mdi_database.txt");
                string MDIRawDB = Encoding.UTF8.GetString(licenseResult);
                string pattern = @"(?<=\}\.)(.*?)(?=\:)";
                Regex rg = new Regex(pattern);
                MatchCollection matches = rg.Matches(MDIRawDB);
                Mdi.MdiDatabase = matches.Select(p => p.Value).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

        }


    }
}
[--/HighlightCode--]

