# Fluent UI Quick Reference - XTWeb

## Common Components

### Layout Components
```razor
<FluentLayout>
    <FluentHeader>...</FluentHeader>
    <FluentBodyContent>...</FluentBodyContent>
</FluentLayout>
```

### Navigation
```razor
<FluentNavMenu Width="250" Collapsible="true">
    <FluentNavGroup>
        <FluentNavLink Href="/" Match="NavLinkMatch.All">Home</FluentNavLink>
    </FluentNavGroup>
</FluentNavMenu>
```

### Buttons
```razor
<!-- Accent (primary) button -->
<FluentButton Appearance="Appearance.Accent" OnClick="HandleClick">
    Click Me
</FluentButton>

<!-- Neutral button -->
<FluentButton Appearance="Appearance.Neutral">Cancel</FluentButton>

<!-- Lightweight button -->
<FluentButton Appearance="Appearance.Lightweight">Link</FluentButton>
```

### Labels & Text
```razor
<FluentLabel Typo="Typography.H3">Heading 3</FluentLabel>
<FluentLabel Typo="Typography.H6">Heading 6</FluentLabel>
<FluentLabel Typo="Typography.Body">Body text</FluentLabel>
<FluentLabel Typo="Typography.Caption">Caption text</FluentLabel>
```

### Cards
```razor
<FluentCard Style="padding: 16px; margin: 16px 0;">
    <FluentLabel Typo="Typography.H6">Card Title</FluentLabel>
    <FluentLabel>Card content goes here</FluentLabel>
</FluentCard>
```

### Message Bars (Alerts)
```razor
<!-- Success -->
<FluentMessageBar Intent="MessageIntent.Success">
    Operation completed successfully!
</FluentMessageBar>

<!-- Error -->
<FluentMessageBar Intent="MessageIntent.Error" Title="Error occurred">
    Something went wrong.
</FluentMessageBar>

<!-- Warning -->
<FluentMessageBar Intent="MessageIntent.Warning">
    Please check your input.
</FluentMessageBar>

<!-- Info -->
<FluentMessageBar Intent="MessageIntent.Info">
    Here's some information.
</FluentMessageBar>
```

### Stack Layout
```razor
<!-- Vertical stack -->
<FluentStack Orientation="Orientation.Vertical">
    <FluentLabel>Item 1</FluentLabel>
    <FluentLabel>Item 2</FluentLabel>
</FluentStack>

<!-- Horizontal stack -->
<FluentStack Orientation="Orientation.Horizontal">
    <FluentButton>Button 1</FluentButton>
    <FluentButton>Button 2</FluentButton>
</FluentStack>

<!-- With spacing -->
<FluentSpacer />
```

### Progress Indicators
```razor
<!-- Indeterminate progress -->
<FluentProgressRing />

<!-- With label -->
<FluentProgressRing />
<FluentLabel>Loading...</FluentLabel>
```

### Accordion
```razor
<FluentAccordion>
    <FluentAccordionItem Heading="Section 1">
        Content for section 1
    </FluentAccordionItem>
    <FluentAccordionItem Heading="Section 2">
        Content for section 2
    </FluentAccordionItem>
</FluentAccordion>
```

## Typography Variants

```razor
Typography.H1 - Largest heading
Typography.H2 - Second level heading
Typography.H3 - Third level heading
Typography.H4 - Fourth level heading
Typography.H5 - Fifth level heading
Typography.H6 - Sixth level heading
Typography.Subtitle - Subtitle text
Typography.Body - Regular body text
Typography.Caption - Small caption text
```

## Button Appearances

```razor
Appearance.Accent - Primary action (blue)
Appearance.Neutral - Secondary action (gray)
Appearance.Lightweight - Minimal button (no background)
Appearance.Outline - Outlined button
Appearance.Stealth - Invisible until hover
```

## Message Intent Types

```razor
MessageIntent.Success - Green (?)
MessageIntent.Error - Red (?)
MessageIntent.Warning - Yellow (?)
MessageIntent.Info - Blue (?)
```

## Alignment Options

```razor
<!-- Horizontal -->
HorizontalAlignment.Left
HorizontalAlignment.Center
HorizontalAlignment.Right

<!-- Vertical -->
VerticalAlignment.Top
VerticalAlignment.Center
VerticalAlignment.Bottom
```

## Common Patterns

### Error Display with Retry
```razor
@if (!string.IsNullOrEmpty(errorMessage))
{
    <FluentMessageBar Intent="MessageIntent.Error">
        @errorMessage
        <FluentButton Appearance="Appearance.Lightweight" OnClick="Retry">
            Retry
        </FluentButton>
    </FluentMessageBar>
}
```

### Loading State
```razor
@if (isLoading)
{
    <FluentStack Orientation="Orientation.Vertical">
        <FluentProgressRing />
        <FluentLabel>Loading data...</FluentLabel>
    </FluentStack>
}
else
{
    <!-- Display content -->
}
```

### Action Bar
```razor
<FluentStack Orientation="Orientation.Horizontal" Style="margin-top: 20px;">
    <FluentButton Appearance="Appearance.Accent" OnClick="Save">
        Save
    </FluentButton>
    <FluentButton Appearance="Appearance.Neutral" OnClick="Cancel">
        Cancel
    </FluentButton>
</FluentStack>
```

### Status Card
```razor
<FluentCard Style="padding: 16px; margin: 16px 0;">
    <FluentStack Orientation="Orientation.Vertical">
        <FluentLabel Typo="Typography.H6">Status</FluentLabel>
        <FluentLabel>Connected: ?</FluentLabel>
        <FluentLabel>Records: @recordCount</FluentLabel>
        <FluentLabel>Last Updated: @lastUpdate</FluentLabel>
    </FluentStack>
</FluentCard>
```

## Styling

### Inline Styles
```razor
<FluentCard Style="padding: 20px; background: #f5f5f5;">
    Content
</FluentCard>
```

### CSS Classes
```razor
<FluentLabel Class="my-custom-class">
    Styled text
</FluentLabel>
```

## Theme Customization

```razor
<!-- Add to Routes.razor -->
<FluentDesignTheme 
    StorageName="theme"
    Mode="DesignThemeModes.System"
    OfficeColor="OfficeColor.Default"
/>
```

## Common Events

```razor
<!-- Button click -->
<FluentButton OnClick="HandleClick">Click</FluentButton>

<!-- Navigation link -->
<FluentNavLink Href="/page" Match="NavLinkMatch.All">
    Link
</FluentNavLink>

@code {
    private void HandleClick()
    {
        // Handle click event
    }
}
```

## Quick Comparison: MudBlazor ? Fluent UI

| MudBlazor | Fluent UI |
|-----------|-----------|
| `MudText` | `FluentLabel` |
| `MudButton` | `FluentButton` |
| `MudPaper` | `FluentCard` |
| `MudAlert` | `FluentMessageBar` |
| `MudProgressCircular` | `FluentProgressRing` |
| `MudNavLink` | `FluentNavLink` |
| `MudContainer` | `FluentBodyContent` |
| `MudAppBar` | `FluentHeader` |
| `MudDrawer` | `FluentNavMenu` |

## Tips

1. **Use FluentStack** for layout instead of custom divs
2. **Typography enum** controls text styling
3. **Appearance enum** controls button styling
4. **MessageIntent enum** controls alert colors
5. **Emoji icons** work well as a simple icon solution
6. **FluentCard** for grouping related content
7. **FluentSpacer** for flexible spacing

## Resources

- Documentation: https://www.fluentui-blazor.net/
- Component Gallery: https://www.fluentui-blazor.net/Components
- GitHub: https://github.com/microsoft/fluentui-blazor
