### SunamoStringGetLines

A lightweight .NET library for splitting strings into lines, handling all common newline formats (`\r\n`, `\n`, `\r`, `\n\r`).

Part of PlatformIndependentNuGetPackages:

- [nuget.org](https://www.nuget.org/profiles/sunamo)
- [github.org](https://github.com/sunamo/PlatformIndependentNuGetPackages)

Another links:

- [Developer site](https://sunamo.cz)

Request for new features / bug report / etc: [Mail](mailto:radek.jancik@sunamo.cz) or on GitHub

## Features

- Split strings by any combination of newline characters
- Optionally remove empty or whitespace-only lines
- Handle single-element lists that contain multiline text

## Usage

```csharp
// Split text into lines
var lines = SHGetLines.GetLines("line1\r\nline2\nline3");

// Remove empty lines
var args = new GetLinesArgs { IsRemovingEmptyOrWhitespaceLines = true };
var filteredLines = SHGetLines.GetLines("line1\n\nline2", args);
```

## Target Frameworks

**TargetFrameworks:** `net10.0;net9.0;net8.0`
