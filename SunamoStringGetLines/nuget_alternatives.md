# NuGet Alternatives to SunamoStringGetLines

This document lists popular NuGet packages that provide similar functionality to SunamoStringGetLines.

## Overview

String line manipulation

## Alternative Packages

### System.String
- **NuGet**: System.Runtime
- **Purpose**: Split by line breaks
- **Key Features**: Split("\n"), Environment.NewLine

### System.IO.StringReader
- **NuGet**: System.Runtime
- **Purpose**: Read string as lines
- **Key Features**: ReadLine() method

### MoreLINQ
- **NuGet**: morelinq
- **Purpose**: Line operations
- **Key Features**: Split, batch operations

## Comparison Notes

StringReader.ReadLine() or string.Split() handle most line operations.

## Choosing an Alternative

Consider these alternatives based on your specific needs:
- **System.String**: Split by line breaks
- **System.IO.StringReader**: Read string as lines
- **MoreLINQ**: Line operations
