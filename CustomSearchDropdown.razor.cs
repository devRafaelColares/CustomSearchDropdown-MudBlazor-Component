using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DlinkSistemas.Gerencial.Web.Components.ComponentsUI.CustomSearchDropdown;
public partial class CustomSearchDropdownBase<T> : ComponentBase
{
    [Parameter] public string Label { get; set; } = "";
    [Parameter] public string PluralLabel { get; set; } = "";
    [Parameter] public string DropdownId { get; set; } = "";
    [Parameter] public List<T> Items { get; set; } = new();
    [Parameter] public EventCallback<List<T>> SelectedItemsChanged { get; set; }
    [Parameter] public RenderFragment<T> ItemTemplate { get; set; }

    [Inject] protected IJSRuntime JS { get; set; }

    public bool IsAllSelected { get; set; } = true;
    public string SearchText { get; set; } = string.Empty;

    [Parameter]
    [EditorRequired]
    public List<T> SelectedItems { get; set; } = new();   
    public bool IsDropdownVisible { get; set; }
    public bool ShowChipsInDropdown { get; set; }
    public ElementReference dropdownRef;

public List<T> FilteredItems => string.IsNullOrWhiteSpace(SearchText)
    ? Items.ToList()
    : Items.Where(item => item.ToString().ToLowerInvariant()
        .Contains(SearchText.ToLowerInvariant()))
        .ToList();
    // protected override void OnInitialized()
    // {
    //     base.OnInitialized();
        
    //     // Garante que ao iniciar, tudo esteja limpo e "Todos" selecionado
    //     if (IsAllSelected)
    //     {
    //         SelectedItems.Clear();
    //     }
    // }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Aguardar um pequeno delay para garantir que o elemento exista no DOM
            await Task.Delay(100);
            await JS.InvokeVoidAsync("addClickOutsideListener", dropdownRef, DropdownId, DotNetObjectReference.Create(this));
        }
    }

    public void ShowDropdown()
    {
        IsDropdownVisible = true;
    }

    public void ToggleDropdownVisibility()
    {
        IsDropdownVisible = !IsDropdownVisible;
    }

    public async Task SelectAll()
    {
        IsAllSelected = !IsAllSelected;
        SelectedItems.Clear();
        await NotifySelectionChanged();
    }

    public async Task SelectItem(T item)
    {
        if (IsAllSelected)
            IsAllSelected = false;

        if (SelectedItems.Contains(item))
            SelectedItems.Remove(item);
        else
            SelectedItems.Add(item);

        await NotifySelectionChanged();
    }

    public async Task RemoveItem(T item)
    {
        SelectedItems.Remove(item);
        await NotifySelectionChanged();
    }

    public async Task ClearSelection()
    {
        IsAllSelected = false;
        SelectedItems.Clear();
        await NotifySelectionChanged();
    }

    public void ToggleChipsVisibility()
    {
        ShowChipsInDropdown = !ShowChipsInDropdown;
    }

    public async Task NotifySelectionChanged()
    {
        await SelectedItemsChanged.InvokeAsync(SelectedItems);
    }

    [JSInvokable]
    public void CloseDropdown(string elementId)
    {
        if (elementId == DropdownId)
        {
            IsDropdownVisible = false;
            StateHasChanged();
        }
    }
}