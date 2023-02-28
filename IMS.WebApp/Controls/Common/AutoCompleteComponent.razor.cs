using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace IMS.WebApp.Controls.Common;

public partial class AutoCompleteComponent : ComponentBase
{
    private readonly bool _stopPropagation = true;
    private ItemViewModel? _currentItem = null;
    private int _currentItemIndex = -1;
    private List<ItemViewModel>? _searchResults = null;
    private ItemViewModel? _selectedItem = null;
    private string _userInput = string.Empty;

    [Parameter]
    public string Label { get; set; } = string.Empty;

    [Parameter]
    public Func<string, Task<List<ItemViewModel>>>? SearchFunction { get; set; }

    [Parameter]
    public EventCallback<ItemViewModel> OnItemSelected { get; set; }

    private string UserInput
    {
        get => _userInput;
        set
        {
            _userInput = value;
            if (!string.IsNullOrWhiteSpace(_userInput) && SearchFunction != null)
            {
                if (_selectedItem?.Name != _userInput)
                {
                    ViewItemAsync();
                }
            }
            else
            {
                ClearCurrentItem();
            }
        }
    }

    private async Task ViewItemAsync()
    {
        if (SearchFunction != null)
        {
            _searchResults = await SearchFunction(_userInput);
            _selectedItem = null;
            StateHasChanged();
        }
    }

    private void OnSelectItem(ItemViewModel? item)
    {
        ClearCurrentItem();

        if (item is null) return;
        _selectedItem = item;
        _userInput = item.Name;
        OnItemSelected.InvokeAsync(item);
    }

    private void ClearCurrentItem()
    {
        _searchResults = null;
        _currentItemIndex = -1;
        _currentItem = null;
    }

    private void OnKeyUp(KeyboardEventArgs @event)
    {
        if (_searchResults is null || _searchResults.Count == 0)
        {
            return;
        }

        switch (@event.Code)
        {
            case "ArrowUp" when _currentItemIndex > 0:
                _currentItem = _searchResults[--_currentItemIndex];
                return;
            case "ArrowUp" when _currentItemIndex <= 0:
                _currentItemIndex = -1;
                _currentItem = null;
                return;
            case "ArrowDown" when _currentItemIndex < _searchResults.Count - 1:
                _currentItem = _searchResults[++_currentItemIndex];
                return;
            case "Enter" or "NumpadEnter" when _currentItem is not null:
                OnSelectItem(_currentItem);
                return;
        }
    }


    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}